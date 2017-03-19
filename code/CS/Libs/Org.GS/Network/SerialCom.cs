using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.IO.Ports;
using System.Management;
using System.ComponentModel;
using Org.GS;

namespace Org.GS.Network
{
  public class SerialCom : IDisposable
  {
    public Action<SerialComEventArgs> SerialComNotification; 

    private SerialPortParms _serialPortParms; 
    private SerialPort _serialPort;
    private ManagementEventWatcher _deviceWatcher;
    private Dictionary<string, SerialPort> _serialPorts;

    private SerialComInfoSet _serialComInfoSet; 

    public bool PortIsOpen { get { return Get_PortIsOpen(); } }

    public bool InDiagnosticsMode
    {
      get 
      {
        if (_serialPortParms == null)
          return false;
        return _serialPortParms.InDiagnosticsMode; 
      }
      set 
      { 
        if (_serialPortParms != null)
          _serialPortParms.InDiagnosticsMode = value; 
      }
    }

    private StringBuilder _diagnosticsReport;

    public string DiagnosticsReport { get { return _diagnosticsReport.ToString(); } }

    public SerialCom()
    {
      _diagnosticsReport = new StringBuilder();

      _serialPort = new SerialPort();
      _serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
      _serialPort.PinChanged += new SerialPinChangedEventHandler(serialPort_PinChanged);
      _serialPort.NewLine = "\n";

      _serialComInfoSet = new SerialComInfoSet();
    }

    public void OpenPort(SerialPortParms serialPortParms)
    {
      _serialPortParms = serialPortParms;
      _serialPort.BaudRate = _serialPortParms.BaudRate;
      _serialPort.DataBits = _serialPortParms.DataBits;
      _serialPort.StopBits = _serialPortParms.StopBits;
      _serialPort.Parity = _serialPortParms.Parity;
      _serialPort.PortName = _serialPortParms.PortName;

      try
      {
        _serialPort.Open();

        SerialComEventArgs eventArgs = new SerialComEventArgs();
        eventArgs.SerialComEventType = SerialComEventType.PortStatusNotification;
        eventArgs.SerialComPortStatus = _serialPort.IsOpen ? SerialComPortStatus.Opened : SerialComPortStatus.Closed; 
        eventArgs.Message = "COM port '" + _serialPortParms.PortName + "' was successfully opened using Baud Rate=" + _serialPortParms.BaudRate.ToString() +
          " DataBits=" + _serialPortParms.DataBits.ToString() + " StopBits=" + _serialPortParms.StopBits.ToString() + " Parity=" + _serialPortParms.Parity.ToString() + ".";
        SendNotification(eventArgs); 
      }
      catch (Exception ex)
      {
        throw new Exception("An 'Exception' occurred attempting to open the COM Port '" + _serialPortParms.PortName + "' using Baud Rate=" + _serialPortParms.BaudRate.ToString() +
          " DataBits=" + _serialPortParms.DataBits.ToString() + " StopBits=" + _serialPortParms.StopBits.ToString() + " Parity=" + _serialPortParms.Parity.ToString() +
          "." + g.crlf + "See InnerException for details.", ex);
      }
            
      UpdatePinState();
      //chkDTR.Checked = _serialPort.DtrEnable;
      //chkRTS.Checked = _serialPort.RtsEnable;

      if (_serialPort.IsOpen)
      {
        //txtSendData.Focus();

        //if (chkClearOnOpen.Checked) 
        //    ClearTerminal();
      }
    }

    public void SendData(string dataToSend)
    {
      if (_serialPortParms == null)
      {
        throw new Exception("SerialPortParms is null."); 
      }

      try
      {
        if (_serialPortParms.InDiagnosticsMode)
        {
          _diagnosticsReport = new StringBuilder();
          _diagnosticsReport.Append(g.crlf + "*** DATA SENT ***" + g.crlf2);
          byte[] bytesSent = Encoding.ASCII.GetBytes(dataToSend);
          string dump = bytesSent.ToBinHexDump();
          _diagnosticsReport.Append(dump);
        }

        _serialPort.Write(dataToSend);

        SerialComEventArgs eventArgs = new SerialComEventArgs();
        eventArgs.SerialComEventType = SerialComEventType.DataSentSuccessfully;
        eventArgs.TextData = dataToSend;
        eventArgs.SerialComPortStatus = _serialPort.IsOpen ? SerialComPortStatus.Opened : SerialComPortStatus.Closed;
        eventArgs.Message = "Text data was successfully sent on COM port '" + _serialPortParms.PortName + "'.";

        if (_serialPortParms.InDiagnosticsMode)
          _diagnosticsReport.Append(g.crlf2 + eventArgs.Message + g.crlf); 

        SendNotification(eventArgs); 
      }
      catch (Exception ex)
      {
        throw new Exception("An 'Exception' occurred attempting to send text data to COM Port '" + _serialPortParms.PortName + "' using Baud Rate=" + _serialPortParms.BaudRate.ToString() +
          " DataBits=" + _serialPortParms.DataBits.ToString() + " StopBits=" + _serialPortParms.StopBits.ToString() + " Parity=" + _serialPortParms.Parity.ToString() +
          "." + g.crlf + "See InnerException for details.", ex);
      }
    }

    public void SendData(byte[] dataToSend)
    {
      if (_serialPortParms == null)
      {
        throw new Exception("SerialPortParms is null.");
      }

      try
      {
        if (_serialPortParms.InDiagnosticsMode)
        {
          _diagnosticsReport = new StringBuilder();
          _diagnosticsReport.Append(g.crlf + "*** DATA SENT ***" + g.crlf2); 
          string dump = dataToSend.ToBinHexDump();
          _diagnosticsReport.Append(dump); 
        }

        _serialPort.Write(dataToSend, 0, dataToSend.Length);

        SerialComEventArgs eventArgs = new SerialComEventArgs();
        eventArgs.SerialComEventType = SerialComEventType.DataSentSuccessfully;
        eventArgs.TextData = dataToSend.ToHex();
        eventArgs.SerialComPortStatus = _serialPort.IsOpen ? SerialComPortStatus.Opened : SerialComPortStatus.Closed;
        eventArgs.Message = "Binary data was successfully sent on COM port '" + _serialPortParms.PortName + "'.";

        if (_serialPortParms.InDiagnosticsMode)
          _diagnosticsReport.Append(g.crlf2 + eventArgs.Message + g.crlf); 

        SendNotification(eventArgs); 
      }
      catch (Exception ex)
      {
        throw new Exception("An 'Exception' occurred attempting to send binary data to COM Port '" + _serialPortParms.PortName + "' using Baud Rate=" + _serialPortParms.BaudRate.ToString() +
          " DataBits=" + _serialPortParms.DataBits.ToString() + " StopBits=" + _serialPortParms.StopBits.ToString() + " Parity=" + _serialPortParms.Parity.ToString() +
          "." + g.crlf + "See InnerException for details.", ex);
      }
    }

    public void ClosePort(bool isTerminating)
    {
      try
      {
        bool portClosed = false;

        if (_serialPort.IsOpen)
        {
          portClosed = true;
          _serialPort.Close();
        }

        if (isTerminating)
        {
          _serialPort.Dispose();
          return;
        }

        string portName = "????";
        if (_serialPortParms != null)
          if (_serialPortParms.PortName.IsNotBlank())
            portName = _serialPortParms.PortName;

        SerialComEventArgs eventArgs = new SerialComEventArgs();
        eventArgs.SerialComEventType = SerialComEventType.PortStatusNotification;
        eventArgs.SerialComPortStatus = _serialPort.IsOpen ? SerialComPortStatus.Opened : SerialComPortStatus.Closed;
        if (portClosed)
          eventArgs.Message = "COM port '" + portName + "' was successfully closed.";
        else
          eventArgs.Message = "COM port '" + portName + "' was already closed."; 
        SendNotification(eventArgs); 
      }
      catch (Exception ex)
      {
        throw new Exception("An 'Exception' occurred attempting to close COM Port '" + _serialPortParms.PortName + "' using Baud Rate=" + _serialPortParms.BaudRate.ToString() +
          " DataBits=" + _serialPortParms.DataBits.ToString() + " StopBits=" + _serialPortParms.StopBits.ToString() + " Parity=" + _serialPortParms.Parity.ToString() +
          "." + g.crlf + "See InnerException for details.", ex);
      }
    }

    private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      if (!_serialPort.IsOpen) 
        return;

      _diagnosticsReport = new StringBuilder();

      SerialComEventArgs eventArgs = new SerialComEventArgs();
      eventArgs.SerialComEventType = SerialComEventType.DataReceivedSuccessfully;
      eventArgs.SerialComPortStatus = _serialPort.IsOpen ? SerialComPortStatus.Opened : SerialComPortStatus.Closed;
      eventArgs.Message = _serialPortParms.DataMode.ToString() + " data was successfully received on COM port '" + _serialPortParms.PortName + "'.";

      DateTime beginDt = DateTime.Now;
      bool completeMessageReceived = false;
      long millisecondLimit = 500; 
      long totalMilliseconds = 0;
      int readCount = 0; 

      switch (_serialPortParms.DataMode)
      {
        case DataMode.Text:
          StringBuilder sb = new StringBuilder(); 

          while(!completeMessageReceived)
          {
            sb.Append(_serialPort.ReadExisting());
            readCount++; 
            string textData = sb.ToString();
            System.Threading.Thread.Sleep(5); 

            if (textData.Contains(_serialPort.NewLine))
              completeMessageReceived = true;
            else
            {
              totalMilliseconds = (long) (DateTime.Now - beginDt).TotalMilliseconds;
              if (totalMilliseconds > millisecondLimit)
                completeMessageReceived = true;
            }
          }

          eventArgs.TextData = sb.ToString();
          eventArgs.Message = "Text data received from scanner '" + eventArgs.TextData.Trim() +
                              "' in " + totalMilliseconds.ToString() + " milliseconds and " + readCount.ToString() + " buffer reads.";  
          _diagnosticsReport.Append(g.crlf + "*** DATA RECEIVED ***" + g.crlf2);
          byte[] bytesSent = Encoding.ASCII.GetBytes(eventArgs.TextData);
          string dump = bytesSent.ToBinHexDump();
          _diagnosticsReport.Append(dump);
          break;

          default:
            int bufferSize = _serialPort.BytesToRead;
            eventArgs.BinaryData = new byte[bufferSize];
            _serialPort.Read(eventArgs.BinaryData, 0, bufferSize);
            _diagnosticsReport.Append(g.crlf + "*** DATA RECEIVED ***" + g.crlf2);
            byte[] bytes = eventArgs.BinaryData;
            _diagnosticsReport.Append(bytes.ToBinHexDump());
            break;
      }

      SendNotification(eventArgs); 
    }

    public void SetDataMode(DataMode dataMode)
    {
      if (_serialPortParms == null)
        return; 

      _serialPortParms.DataMode = dataMode; 
    }
        
    private void serialPort_PinChanged(object sender, SerialPinChangedEventArgs e)
    {
      UpdatePinState();
    }

    private void UpdatePinState()
    {
      // need to send an event with an object to update state

      //this.Invoke(new ThreadStart(() =>
      //{
      //    // Show the state of the pins
      //    chkCD.Checked = comport.CDHolding;
      //    chkCTS.Checked = comport.CtsHolding;
      //    chkDSR.Checked = comport.DsrHolding;
      //}));
    }

    public List<string> GetComPortList()
    {
      List<string> comPortNames = SerialPort.GetPortNames().ToList();
      comPortNames.Sort();
      return comPortNames;
    }

    private void SendNotification(SerialComEventArgs e)
    {
      if (this.SerialComNotification == null)
        return;

      this.SerialComNotification(e); 
    }

    public void StartListeningForComPortChanges()
    {
      _deviceWatcher = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2 OR EventType = 3"));
      _serialPorts = new Dictionary<string, SerialPort>();
      _deviceWatcher.EventArrived += _deviceWatcher_EventArrived;
      _deviceWatcher.Start();
    }

    public void StopListeningForComPortChanges()
    {
      if (_deviceWatcher == null)
        return;

      _deviceWatcher.Stop();
      _deviceWatcher.Dispose();
      _deviceWatcher = null; 
    }

    private void _deviceWatcher_EventArrived(object sender, EventArrivedEventArgs e)
    {
      if (this.SerialComNotification == null)
        return;

      SerialComEventArgs eventArgs = new SerialComEventArgs();
      eventArgs.SerialComEventType = SerialComEventType.ComDeviceChange;

      if (_serialComInfoSet != null)
      {
        List<string> prevComDevices = new List<string>();
        foreach (string prevComDevice in _serialComInfoSet.Keys)
          prevComDevices.Add(prevComDevice);
        DiscoverComDevices();
        List<string> disappearedComDevices = new List<string>();
        List<string> appearedComDevices = new List<string>();

        foreach (string prevComDevice in prevComDevices)
        {
          if (!_serialComInfoSet.ContainsKey(prevComDevice))
            disappearedComDevices.Add(prevComDevice);
        }

        foreach (string currentComDevice in _serialComInfoSet.Keys)
        {
          if (!prevComDevices.Contains(currentComDevice))
            appearedComDevices.Add(currentComDevice);
        }

        StringBuilder sb = new StringBuilder();
        foreach (string disappearedComDevice in disappearedComDevices)
        {
          if (sb.Length > 0)
            sb.Append(g.crlf);
          sb.Append(disappearedComDevice + " is no longer available");
        }

        foreach (string appearedComDevice in appearedComDevices)
        {
          if (sb.Length > 0)
            sb.Append(g.crlf);
          sb.Append(appearedComDevice + " is now available");
        }

        if (sb.Length == 0)
          return;

        eventArgs.Message = "The following COM device changes have occurred:" + g.crlf + sb.ToString();
      }
      else
      {
          eventArgs.Message = "COM device changes detected (1).";
      }

      this.SerialComNotification(eventArgs); 
    }

    private bool Get_PortIsOpen()
    {
      if (_serialPort == null)
        return false;

      return _serialPort.IsOpen;
    }

    public SerialComInfoSet DiscoverComDevices()
    {
      SerialComInfoSet serialComInfoSet = new SerialComInfoSet();
      string wmiNamespace = @"root\WMI";

      ManagementObjectSearcher moSearcher = null;
      ManagementObjectCollection moSet = null;

      Dictionary<string, ManagementObject> pnpComDevices = new Dictionary<string, ManagementObject>();

      try
      {
        List<string> wmiQueries = new List<string>();
        wmiQueries.Add(@"SELECT * FROM MSSerial_CommInfo");
        wmiQueries.Add(@"SELECT * FROM MSSerial_PortName");
        wmiQueries.Add(@"SELECT * FROM MSSerial_CommProperties");
        wmiQueries.Add(@"SELECT * FROM MSSerial_HardwareConfiguration");
        wmiQueries.Add(@"SELECT * FROM MSSerial_PerformanceInformation");
        wmiQueries.Add(@"SELECT * FROM Win32_PnPEntity");

        List<string> deviceInstances = new List<string>();

        foreach (string query in wmiQueries)
        {
          string wmiObject = query.Replace(@"SELECT * FROM", String.Empty).Trim();
          string fullQuery = query;

          if (query.Contains("Win32_PnPEntity"))
            wmiNamespace = @"root\CIMV2";
          else
            wmiNamespace = @"root\WMI";

          moSearcher = new ManagementObjectSearcher(wmiNamespace, fullQuery);
          moSet = moSearcher.Get();

          foreach (ManagementObject device in moSet)
          {
            string instanceName = String.Empty;
            SerialComInfo serialComInfo = null;

            if (wmiNamespace.Contains(@"root\WMI"))
            {
              instanceName = device.Properties["InstanceName"].Value.ToString();

              if (!serialComInfoSet.ContainsKey(instanceName))
              {
                serialComInfo = new SerialComInfo();
                serialComInfo.InstanceName = instanceName;
                serialComInfoSet.Add(serialComInfo.InstanceName, serialComInfo);
              }
              serialComInfo = serialComInfoSet[instanceName];
            }
            else
            {
              string deviceName = device.Properties["Name"].Value.ToString();
              System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("COM[0-9]+", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
              System.Text.RegularExpressions.Match m = r.Match(deviceName);

              if (m.Success)
              {
                string comDeviceName = m.Value;
                if (!pnpComDevices.ContainsKey(comDeviceName))
                {
                    pnpComDevices.Add(comDeviceName, device);
                }
              }
            }

            if (wmiNamespace.Contains(@"root\WMI"))
            {
              System.Management.PropertyDataCollection props = device.Properties;
              foreach (PropertyData prop in props)
              {
                string propValue = String.Empty;
                if (prop.Value != null)
                  propValue = prop.Value.ToString().Trim();

                switch (prop.Name)
                {
                  case "Active":
                    serialComInfo.Active = propValue.ToBoolean();
                    break;

                  case "BaudRate":
                    serialComInfo.BaudRate = propValue.ToUInt32();
                    break;

                  case "BitsPerByte":
                    serialComInfo.BitsPerByte = propValue.ToUInt32();
                    break;

                  case "IsBusy":
                    serialComInfo.IsBusy = propValue.ToBoolean();
                    break;

                  case "MaximumBaudRate":
                    serialComInfo.MaximumBaudRate = propValue.ToUInt32();
                    break;

                  case "MaximumInputBufferSize":
                    serialComInfo.MaximumInputBufferSize = propValue.ToUInt32();
                    break;

                  case "MaximumOutputBufferSize":
                    serialComInfo.MaximumOutputBufferSize = propValue.ToUInt32();
                    break;

                  case "Parity":
                    serialComInfo.Parity = propValue.ToUInt32();
                    break;

                  case "ParityCheckEnable":
                    serialComInfo.ParityCheckEnable = propValue.ToBoolean();
                    break;

                  case "SettableBaudRate":
                    serialComInfo.SettableBaudRate = propValue.ToBoolean();
                    break;

                  case "SettableDataBits":
                    serialComInfo.SettableDataBits = propValue.ToBoolean();
                    break;

                  case "SettableFlowControl":
                    serialComInfo.SettableFlowControl = propValue.ToBoolean();
                    break;

                  case "SettableParity":
                    serialComInfo.SettableParity = propValue.ToBoolean();
                    break;

                  case "SettableParityCheck":
                    serialComInfo.SettableParityCheck = propValue.ToBoolean();
                    break;

                  case "SettableStopBits":
                    serialComInfo.SettableStopBits = propValue.ToBoolean();
                    break;

                  case "StopBits":
                    serialComInfo.StopBits = propValue.ToUInt32();
                    break;

                  case "Support16BitMode":
                    serialComInfo.Support16BitMode = propValue.ToBoolean();
                    break;

                  case "SupportDTRDSR":
                    serialComInfo.SupportDTRDSR = propValue.ToBoolean();
                    break;

                  case "SupportIntervalTimeouts":
                    serialComInfo.SupportIntervalTimeouts = propValue.ToBoolean();
                    break;

                  case "SupportParityCheck":
                    serialComInfo.SupportParityCheck = propValue.ToBoolean();
                    break;

                  case "SupportRTSCTS":
                    serialComInfo.SupportRTSCTS = propValue.ToBoolean();
                    break;

                  case "SupportXonXoff":
                    serialComInfo.SupportXonXoff = propValue.ToBoolean();
                    break;

                  case "XoffCharacter":
                    serialComInfo.XoffCharacter = propValue.ToUInt32();
                    break;

                  case "XoffXmitThreshold":
                    serialComInfo.XoffXmitThreshold = propValue.ToUInt32();
                    break;

                  case "XonCharacter":
                    serialComInfo.XonCharacter = propValue.ToUInt32();
                    break;

                  case "XonXmitThreshold":
                    serialComInfo.XonXmitThreshold = propValue.ToUInt32();
                    break;

                  case "PortName":
                    serialComInfo.PortName = propValue;
                    break;

                  case "dwCurrentRxQueue":
                    serialComInfo.dwCurrentRxQueue = propValue.ToUInt32();
                    break;

                  case "dwCurrentTxQueue":
                    serialComInfo.dwCurrentTxQueue = propValue.ToUInt32();
                    break;

                  case "dwMaxBaud":
                    serialComInfo.dwMaxBaud = propValue.ToUInt32();
                    break;

                  case "dwMaxRxQueue":
                    serialComInfo.dwMaxRxQueue = propValue.ToUInt32();
                    break;

                  case "dwMaxTxQueue":
                    serialComInfo.dwMaxTxQueue = propValue.ToUInt32();
                    break;

                  case "dwProvCapabilities":
                    serialComInfo.dwProvCapabilities = propValue.ToUInt32();
                    break;

                  case "dwProvCharSize":
                    serialComInfo.dwProvCharSize = propValue.ToUInt32();
                    break;

                  case "dwProvSpec1":
                    serialComInfo.dwProvSpec1 = propValue.ToUInt32();
                    break;

                  case "dwProvSpec2":
                    serialComInfo.dwProvSpec2 = propValue.ToUInt32();
                    break;

                  case "dwProvSubType":
                    serialComInfo.dwProvSubType = propValue.ToUInt32();
                      break;

                  case "dwReserved1":
                    serialComInfo.dwReserved1 = propValue.ToUInt32();
                    break;

                  case "dwServiceMask":
                    serialComInfo.dwServiceMask = propValue.ToUInt32();
                    break;

                  case "dwSettableBaud":
                    serialComInfo.dwSettableBaud = propValue.ToUInt32();
                    break;

                  case "dwSettableParams":
                    serialComInfo.dwSettableParams = propValue.ToUInt32();
                    break;

                  case "wcProvChar":
                    if (propValue.Length > 0)
                      serialComInfo.wcProvChar = Convert.ToByte(propValue[0]);
                    break;

                  case "wPacketLength":
                    serialComInfo.wPacketLength = propValue.ToUInt16();
                    break;

                  case "wPacketVersion":
                    serialComInfo.wPacketVersion = propValue.ToUInt16();
                    break;

                  case "wSettableData":
                    serialComInfo.wSettableData = propValue.ToUInt16();
                      break;

                  case "wSettableStopParity":
                    serialComInfo.wSettableStopParity = propValue.ToUInt16();
                    break;

                  case "BaseIOAddress":
                    serialComInfo.BaseIOAddress = propValue.ToUInt64();
                    break;

                  case "InterruptType":
                    serialComInfo.InterruptType = propValue.ToUInt32();
                    break;

                  case "IrqAffinityMask":
                    serialComInfo.IrqAffinityMask = propValue.ToUInt64();
                    break;

                  case "IrqLevel":
                    serialComInfo.IrqLevel = propValue.ToUInt32();
                    break;

                  case "IrqNumber":
                    serialComInfo.IrqNumber = propValue.ToUInt32();
                    break;

                  case "IrqVector":
                    serialComInfo.IrqVector = propValue.ToUInt32();
                    break;

                  case "BufferOverrunErrorCount":
                    serialComInfo.BufferOverrunErrorCount = propValue.ToUInt32();
                    break;

                  case "FrameErrorCount":
                    serialComInfo.FrameErrorCount = propValue.ToUInt32();
                    break;

                  case "ParityErrorCount":
                    serialComInfo.ParityErrorCount = propValue.ToUInt32();
                    break;

                  case "ReceivedCount":
                    serialComInfo.ReceivedCount = propValue.ToUInt32();
                    break;

                  case "SerialOverrunErrorCount":
                    serialComInfo.SerialOverrunErrorCount = propValue.ToUInt32();
                    break;

                  case "TransmittedCount":
                    serialComInfo.TransmittedCount = propValue.ToUInt32();
                    break;
                }
              }
            }
          }
        }

        SortedList<string, SerialComInfo> sortedList = new SortedList<string, SerialComInfo>();
        foreach (SerialComInfo sci in serialComInfoSet.Values)
        {
          string comPort = sci.PortName;
          int seq = 0;

          while (sortedList.ContainsKey(comPort))
          {
            comPort = comPort + "-" + seq.ToString("000");
            seq++;
          }

          sortedList.Add(comPort, sci);
        }

        SerialComInfoSet sortedSerialComInfoSet = new SerialComInfoSet();
        foreach (SerialComInfo sci in sortedList.Values)
        {
          if (!sortedSerialComInfoSet.ContainsKey(sci.PortName))
            sortedSerialComInfoSet.Add(sci.PortName, sci);
        }

        // add in the PnPEntity WMI information if found                
        if (pnpComDevices.Count > 0)
        {
          foreach (KeyValuePair<string, ManagementObject> kvpPnp in pnpComDevices)
          {
            string portName = kvpPnp.Key;
            ManagementObject device = kvpPnp.Value;

            if (sortedSerialComInfoSet.ContainsKey(portName))
            {
              SerialComInfo sci = sortedSerialComInfoSet[portName];

              PropertyDataCollection pnpEntityProps = device.Properties;

              foreach (PropertyData pnpEntityProp in pnpEntityProps)
              {
                sci.PnP_InfoFound = true;

                string pnpEntityPropValue = String.Empty;
                if (pnpEntityProp.Value != null)
                  pnpEntityPropValue = pnpEntityProp.Value.ToString().Trim();

                switch (pnpEntityProp.Name)
                {
                  case "Availability":
                    sci.PnP_Availability = pnpEntityPropValue.ToUInt16();
                    break;

                  case "Caption":
                    sci.PnP_Caption = pnpEntityPropValue;
                    break;

                  case "CompatibleID":
                    if (pnpEntityProp.Value != null)
                      sci.PnP_CompatibleID = (string[])pnpEntityProp.Value;
                    break;

                  case "ConfigManagerErrorCode":
                    sci.PnP_ConfigManagerErrorCode = pnpEntityPropValue.ToUInt32();
                    break;

                  case "ConfigManagerUserConfig":
                    sci.PnP_ConfigManagerUserConfig = pnpEntityPropValue.ToBoolean();
                    break;

                  case "CreationClassName":
                    sci.PnP_CreationClassName = pnpEntityPropValue;
                    break;

                  case "Description":
                    sci.PnP_Description = pnpEntityPropValue;
                    break;

                  case "DeviceID":
                    sci.PnP_DeviceID = pnpEntityPropValue;
                    break;

                  case "ErrorCleared":
                    sci.PnP_ErrorCleared = pnpEntityPropValue.ToBoolean();
                    break;

                  case "ErrorDescription":
                    sci.PnP_ErrorDescription = pnpEntityPropValue;
                    break;

                  case "HardwareID":
                    if (pnpEntityProp.Value != null)
                      sci.PnP_HardwareID = (string[])pnpEntityProp.Value;
                    break;

                  case "InstallDate":
                    if (pnpEntityProp.Value != null)
                      sci.PnP_InstallDate = (DateTime)pnpEntityProp.Value;
                    break;

                  case "LastErrorCode":
                    sci.PnP_LastErrorCode = pnpEntityPropValue.ToUInt32();
                    break;

                  case "Manufacturer":
                    sci.PnP_Manufacturer = pnpEntityPropValue;
                    break;

                  case "Name":
                    sci.PnP_Name = pnpEntityPropValue;
                    break;

                  case "PNPDeviceID":
                    sci.PnP_PNPDeviceID = pnpEntityPropValue;
                    break;

                  case "PowerManagementCapabilities":
                    if (pnpEntityProp.Value != null)
                      sci.PnP_PowerManagementCapabilities = (UInt16[])pnpEntityProp.Value;
                    break;

                  case "PowerManagementSupported":
                    sci.PnP_PowerManagementSupported = pnpEntityPropValue.ToBoolean();
                    break;

                  case "Service":
                    sci.PnP_Service = pnpEntityPropValue;
                    break;

                  case "Status":
                    sci.PnP_Status = pnpEntityPropValue;
                    break;

                  case "StatusInfo":
                    sci.PnP_StatusInfo = pnpEntityPropValue.ToUInt16();
                    break;

                  case "SystemCreationClassName":
                    sci.PnP_SystemCreationClassName = pnpEntityPropValue;
                    break;

                  case "SystemName":
                    sci.PnP_SystemName = pnpEntityPropValue;
                    break;
                }
              }
            }
          }
        }

        _serialComInfoSet = new SerialComInfoSet();
        foreach (SerialComInfo sci in sortedSerialComInfoSet.Values)
        {
          _serialComInfoSet.Add(sci.PortName, sci);
        }

        return sortedSerialComInfoSet;

      }
      catch (ManagementException mex)
      {
        if (mex.Message.Trim().ToLower() == "not supported")
          return new SerialComInfoSet();
        else
          throw new Exception("A System Management Exception occurred attempting to enumerate COM devices.", mex);
      }
      finally
      {
        if (moSearcher != null)
          moSearcher.Dispose();

        if (moSet != null)
          moSet.Dispose();
      }
    }

    public void Dispose()
    {
      if (_deviceWatcher != null)
        _deviceWatcher.Stop();
    }
  }
}
