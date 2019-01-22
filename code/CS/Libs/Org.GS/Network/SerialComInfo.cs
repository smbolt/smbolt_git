using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS.Network
{
  public class SerialComInfo
  {
    // MSSerial_PortName
    public string PortName {
      get;
      set;
    }
    public bool Active {
      get;
      set;
    }
    public string InstanceName {
      get;
      set;
    }

    // MSSerial_CommInfo
    public UInt32 BaudRate {
      get;
      set;
    }
    public UInt32 BitsPerByte {
      get;
      set;
    }
    public bool IsBusy {
      get;
      set;
    }
    public UInt32 MaximumBaudRate {
      get;
      set;
    }
    public UInt32 MaximumInputBufferSize {
      get;
      set;
    }
    public UInt32 MaximumOutputBufferSize {
      get;
      set;
    }
    public UInt32 Parity {
      get;
      set;
    }
    public bool ParityCheckEnable {
      get;
      set;
    }
    public bool SettableBaudRate {
      get;
      set;
    }
    public bool SettableDataBits {
      get;
      set;
    }
    public bool SettableFlowControl {
      get;
      set;
    }
    public bool SettableParity {
      get;
      set;
    }
    public bool SettableParityCheck {
      get;
      set;
    }
    public bool SettableStopBits {
      get;
      set;
    }
    public UInt32 StopBits {
      get;
      set;
    }
    public bool Support16BitMode {
      get;
      set;
    }
    public bool SupportDTRDSR {
      get;
      set;
    }
    public bool SupportIntervalTimeouts {
      get;
      set;
    }
    public bool SupportParityCheck {
      get;
      set;
    }
    public bool SupportRTSCTS {
      get;
      set;
    }
    public bool SupportXonXoff {
      get;
      set;
    }
    public UInt32 XoffCharacter {
      get;
      set;
    }
    public UInt32 XoffXmitThreshold {
      get;
      set;
    }
    public UInt32 XonCharacter {
      get;
      set;
    }
    public UInt32 XonXmitThreshold {
      get;
      set;
    }

    // MSSerial_CommProperties
    public UInt32 dwCurrentRxQueue {
      get;
      set;
    }
    public UInt32 dwCurrentTxQueue {
      get;
      set;
    }
    public UInt32 dwMaxBaud {
      get;
      set;
    }
    public UInt32 dwMaxRxQueue {
      get;
      set;
    }
    public UInt32 dwMaxTxQueue {
      get;
      set;
    }
    public UInt32 dwProvCapabilities {
      get;
      set;
    }
    public UInt32 dwProvCharSize {
      get;
      set;
    }
    public UInt32 dwProvSpec1 {
      get;
      set;
    }
    public UInt32 dwProvSpec2 {
      get;
      set;
    }
    public UInt32 dwProvSubType {
      get;
      set;
    }
    public UInt32 dwReserved1 {
      get;
      set;
    }
    public UInt32 dwServiceMask {
      get;
      set;
    }
    public UInt32 dwSettableBaud {
      get;
      set;
    }
    public UInt32 dwSettableParams {
      get;
      set;
    }
    public Byte wcProvChar {
      get;
      set;
    }
    public UInt16 wPacketLength {
      get;
      set;
    }
    public UInt16 wPacketVersion {
      get;
      set;
    }
    public UInt16 wSettableData {
      get;
      set;
    }
    public UInt16 wSettableStopParity {
      get;
      set;
    }

    // MSSerial_HardwareConfiguration
    public UInt64 BaseIOAddress {
      get;
      set;
    }
    public UInt32 InterruptType {
      get;
      set;
    }
    public UInt64 IrqAffinityMask {
      get;
      set;
    }
    public UInt32 IrqLevel {
      get;
      set;
    }
    public UInt32 IrqNumber {
      get;
      set;
    }
    public UInt32 IrqVector {
      get;
      set;
    }

    // MSSerial_PerformanceInformation
    public UInt32 BufferOverrunErrorCount {
      get;
      set;
    }
    public UInt32 FrameErrorCount {
      get;
      set;
    }
    public UInt32 ParityErrorCount {
      get;
      set;
    }
    public UInt32 ReceivedCount {
      get;
      set;
    }
    public UInt32 SerialOverrunErrorCount {
      get;
      set;
    }
    public UInt32 TransmittedCount {
      get;
      set;
    }

    // PnPEntity_Information
    public bool PnP_InfoFound {
      get;
      set;
    }
    public UInt16 PnP_Availability {
      get;
      set;
    }
    public string PnP_Caption {
      get;
      set;
    }
    public string PnP_ClassGuid  {
      get;
      set;
    }
    public string[] PnP_CompatibleID {
      get;
      set;
    }
    public UInt32 PnP_ConfigManagerErrorCode {
      get;
      set;
    }
    public bool PnP_ConfigManagerUserConfig {
      get;
      set;
    }
    public string PnP_CreationClassName {
      get;
      set;
    }
    public string PnP_Description {
      get;
      set;
    }
    public string PnP_DeviceID {
      get;
      set;
    }
    public bool PnP_ErrorCleared {
      get;
      set;
    }
    public string PnP_ErrorDescription {
      get;
      set;
    }
    public string[] PnP_HardwareID {
      get;
      set;
    }
    public DateTime? PnP_InstallDate {
      get;
      set;
    }
    public UInt32 PnP_LastErrorCode {
      get;
      set;
    }
    public string PnP_Manufacturer {
      get;
      set;
    }
    public string PnP_Name {
      get;
      set;
    }
    public string PnP_PNPDeviceID {
      get;
      set;
    }
    public UInt16[] PnP_PowerManagementCapabilities {
      get;
      set;
    }
    public bool PnP_PowerManagementSupported {
      get;
      set;
    }
    public string PnP_Service {
      get;
      set;
    }
    public string PnP_Status {
      get;
      set;
    }
    public UInt16 PnP_StatusInfo {
      get;
      set;
    }
    public string PnP_SystemCreationClassName {
      get;
      set;
    }
    public string PnP_SystemName {
      get;
      set;
    }

    public string FormattedDevice
    {
      get {
        return Get_FormattedDevice();
      }
    }

    public string DeviceIdentifier
    {
      get {
        return Get_DeviceIdentifier();
      }
    }

    public string Manufacturer
    {
      get {
        return this.PnP_Manufacturer;
      }
    }

    public string Product
    {
      get {
        return this.PnP_Name;
      }
    }

    public SerialComInfo()
    {
      this.Initialize();
    }

    private void Initialize()
    {
      this.PortName = String.Empty;
      this.Active = false;
      this.InstanceName = String.Empty;

      // MSSerial_CommInfo
      this.BaudRate = 0;
      this.BitsPerByte = 0;
      this.IsBusy = false;
      this.MaximumBaudRate = 0;
      this.MaximumInputBufferSize = 0;
      this.MaximumOutputBufferSize = 0;
      this.Parity = 0;
      this.ParityCheckEnable = false;
      this.SettableBaudRate = false;
      this.SettableDataBits = false;
      this.SettableFlowControl = false;
      this.SettableParity = false;
      this.SettableParityCheck = false;
      this.SettableStopBits = false;
      this.StopBits = 0;
      this.Support16BitMode = false;
      this.SupportDTRDSR = false;
      this.SupportIntervalTimeouts = false;
      this.SupportParityCheck = false;
      this.SupportRTSCTS = false;
      this.SupportXonXoff = false;
      this.XoffCharacter = 0;
      this.XoffXmitThreshold = 0;
      this.XonCharacter = 0;
      this.XonXmitThreshold = 0;

      // MSSerial_CommProperties
      this.dwCurrentRxQueue = 0;
      this.dwCurrentTxQueue = 0;
      this.dwMaxBaud = 0;
      this.dwMaxRxQueue = 0;
      this.dwMaxTxQueue = 0;
      this.dwProvCapabilities = 0;
      this.dwProvCharSize = 0;
      this.dwProvSpec1 = 0;
      this.dwProvSpec2 = 0;
      this.dwProvSubType = 0;
      this.dwReserved1 = 0;
      this.dwServiceMask = 0;
      this.dwSettableBaud = 0;
      this.dwSettableParams = 0;
      this.wcProvChar = 0;
      this.wPacketLength = 0;
      this.wPacketVersion = 0;
      this.wSettableData = 0;
      this.wSettableStopParity = 0;

      // MSSerial_HardwareConfiguration
      this.BaseIOAddress = 0;
      this.InterruptType = 0;
      this.IrqAffinityMask = 0;
      this.IrqLevel = 0;
      this.IrqNumber = 0;
      this.IrqVector = 0;

      // MSSerial_PerformanceInformation
      this.BufferOverrunErrorCount = 0;
      this.FrameErrorCount = 0;
      this.ParityErrorCount = 0;
      this.ReceivedCount = 0;
      this.SerialOverrunErrorCount = 0;
      this.TransmittedCount = 0;

      // PnPEntity_Information
      this.PnP_InfoFound = false;
      this.PnP_Availability = 0;
      this.PnP_Caption = String.Empty;
      this.PnP_ClassGuid = String.Empty;
      this.PnP_CompatibleID = null;
      this.PnP_ConfigManagerErrorCode = 0;
      this.PnP_ConfigManagerUserConfig = false;
      this.PnP_CreationClassName = String.Empty;
      this.PnP_Description = String.Empty;
      this.PnP_DeviceID = String.Empty;
      this.PnP_ErrorCleared = false;
      this.PnP_ErrorDescription = String.Empty;
      this.PnP_HardwareID = null;
      this.PnP_InstallDate = null;
      this.PnP_LastErrorCode = 0;
      this.PnP_Manufacturer = String.Empty;
      this.PnP_Name = String.Empty;
      this.PnP_PNPDeviceID = String.Empty;
      this.PnP_PowerManagementCapabilities = null;
      this.PnP_PowerManagementSupported = false;
      this.PnP_Service = String.Empty;
      this.PnP_Status = String.Empty;
      this.PnP_StatusInfo = 0;
      this.PnP_SystemCreationClassName = String.Empty;
      this.PnP_SystemName = String.Empty;
    }

    private string Get_FormattedDevice()
    {
      string formattedDevice = String.Empty;

      string portName = this.PortName;
      string instanceName = this.InstanceName;
      string manufacturer = this.PnP_Manufacturer;
      string productName = this.PnP_Name;

      if (productName.StartsWith(manufacturer))
        manufacturer = String.Empty;

      if (productName.IsBlank() && manufacturer.IsBlank())
        formattedDevice = portName + " - [ " + this.InstanceName + " ]";
      else
        formattedDevice = portName + " - [ " + (manufacturer + " " + productName).Trim() + " ]";

      return formattedDevice;
    }

    public string GetDeviceReport()
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("Port Name      : " + this.PortName + g.crlf +
                "Manufacturer   : " + this.Manufacturer + g.crlf +
                "Product        : " + this.Product + g.crlf +
                "Identifier     : " + this.DeviceIdentifier + g.crlf +
                "Baud Rate      : " + this.BaudRate.ToString() + g.crlf +
                "Data Bits      : " + this.BitsPerByte.ToString() + g.crlf +
                "Stop Bits      : " + this.StopBits.ToString() + g.crlf +
                "Parity         : " + this.Parity.ToString() + g.crlf +
                "Status         : " + this.PnP_Status);


      string report = sb.ToString();
      return report;
    }

    private string Get_DeviceIdentifier()
    {
      string instanceName = this.InstanceName;

      List<string> tokens = instanceName.Split(Constants.BSlashDelimiter, StringSplitOptions.RemoveEmptyEntries).ToList();

      if (tokens.Count == 0)
        return String.Empty;

      string deviceIdToken = String.Empty;
      foreach (string token in tokens)
      {
        if (token.ToUpper().StartsWith("V") && token.Contains('&') && token.CountOfChar('_') == 2)
        {
          return token;
        }
      }

      return String.Empty;
    }
  }
}
