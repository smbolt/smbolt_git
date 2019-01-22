using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS.Network
{
  public enum SerialComEventType
  {
    NotSet,
    PortStatusNotification,
    ComDeviceChange,
    DataReceivedSuccessfully,
    DataSentSuccessfully,
  }

  public enum SerialComPortStatus
  {
    NotSet,
    Opened,
    Closed
  }

  public class SerialComEventArgs
  {
    public SerialComEventType SerialComEventType {
      get;
      set;
    }
    public SerialComPortStatus SerialComPortStatus {
      get;
      set;
    }
    public string TextData {
      get;
      set;
    }
    public byte[] BinaryData {
      get;
      set;
    }
    public string Message {
      get;
      set;
    }

    public SerialComEventArgs()
    {
      this.SerialComEventType = SerialComEventType.NotSet;
      this.SerialComPortStatus = SerialComPortStatus.NotSet;
      this.TextData = String.Empty;
      this.BinaryData = new byte[0];
      this.Message = String.Empty;
    }
  }
}
