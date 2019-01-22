using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Pipes
{
  public class NamedPipeServer
  {
    public string NamedPipeServerName {
      get;
      private set;
    }
    public string NamedPipeName {
      get;
      private set;
    }
    public NamedPipeServerStatus Status {
      get;
      private set;
    }

    private NamedPipeServerStream _serverStream;
    public int MaxInstances {
      get;
      private set;
    }

    public NamedPipeServer(string namedPipeServerName, string namedPipeName, int maxInstances = -1)
    {
      if (namedPipeServerName.IsBlank())
        throw new Exception("The NamedPipeServerName is blank or null.");
      this.NamedPipeServerName = namedPipeServerName;

      if (namedPipeName.IsBlank())
        throw new Exception("The NamedPipeName is blank or null.");
      this.NamedPipeName = namedPipeName;

      this.MaxInstances = maxInstances;
      this.Status = NamedPipeServerStatus.Created;
    }

    public void BeginListening()
    {
      try
      {
        _serverStream = new NamedPipeServerStream(this.NamedPipeName, PipeDirection.InOut, this.MaxInstances);
        _serverStream.BeginWaitForConnection(WaitForConnectionCallback, null);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to begin listening on the named pipe named '" + this.NamedPipeName + "' " +
                            "in NamedPipeServer named '" + this.NamedPipeServerName + "'.", ex);
      }
    }

    private void WaitForConnectionCallback(IAsyncResult result)
    {
      try
      {


      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred during the async callback function that is called when a client connects to the named pipe - " +
                            "in NamedPipeServer named '" + this.NamedPipeServerName + "' on NamedPipe named '" + this.NamedPipeName + "'.", ex);

      }
    }



  }
}
