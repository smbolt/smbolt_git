using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.WSO
{
  public enum HeaderType
  {
    NotSet,
    Standard
  }

  public enum MessagingParticipant
  {
    NotSet,
    Sender,
    Receiver
  }

  public enum ServiceProcessStatus
  {
    NotSet,
    NotUsed,
    Unsuccessful,
    Successful
  }
}
