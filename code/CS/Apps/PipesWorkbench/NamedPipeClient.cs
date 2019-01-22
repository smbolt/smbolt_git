using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using Org.GS;

namespace Org.PipesWorkbench
{
  public class NamedPipeClient
  {
    private int _pipeClientLimit = 4;

    private NamedPipeClientStream _pipeClient;


    public NamedPipeClient()
    {
      _pipeClient = new NamedPipeClientStream(".", "REMOTE_HOST_01", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);
      _pipeClient.Connect();
    }

    public string SendMessage(string message)
    {
      string responseFromServer = "No response";

      StreamString ss = new StreamString(_pipeClient);

      // Validate the server's signature string
      string fromServer = ss.ReadString();

      if (fromServer == "I am the one true server!")
      {
        // The client security token is sent with the first write.
        // Send the name of the file whose contents are returned
        // by the server.

        ss.WriteString(message);

        responseFromServer = ss.ReadString();
      }
      else
      {
        return "Server could not be verified.";
      }

      return responseFromServer;
    }

    public void Close()
    {
      _pipeClient.Close();
      Thread.Sleep(4000);
    }
  }
}
