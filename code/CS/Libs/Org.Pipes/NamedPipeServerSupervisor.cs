using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Pipes
{
  public class NamedPipeServerSupervisor
  {
    public NamedPipeServerSet NamedPipeServerSet {
      get;
      private set;
    }
    public string PipeList64Path {
      get;
      private set;
    }


    public NamedPipeServerSupervisor(string pipeList64Path = "")
    {
      this.NamedPipeServerSet = new NamedPipeServerSet();

    }

    public void CreateNamedPipeServer(string namedPipeServerName, string namedPipeName, int maxInstances = -1)
    {
      try
      {
        if (this.NamedPipeServerSet.ContainsKey(namedPipeServerName))
          throw new Exception("The NamedPipeServerSet of the NamedPipeSupervisor already contains a NamedPipeServer with " +
                              "the NamedPipeServerName '" + namedPipeServerName + "'.");

        var namedPipeServer = new NamedPipeServer(namedPipeServerName, namedPipeName, maxInstances);
        this.NamedPipeServerSet.Add(namedPipeServer.NamedPipeServerName, namedPipeServer);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a NamedPipeServer with name '" + namedPipeServerName + "'.", ex);
      }
    }

    public void CreateNamedPipe(string mainNamedPipeServerName, string mainNamedPipeName, int maxMainPipeInstances = -1)
    {
      try
      {



        //NamedPipeServerStream pipeServer = new NamedPipeServerStream("REMOTE_HOST_01", PipeDirection.InOut, _threadLimit);

      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a NamedPipe named '" + mainNamedPipeName + "' on NamedPipeServer named '" +
                            mainNamedPipeServerName + "'.", ex);
      }
    }

    public void BeginListeningOnNamedPipe(string namedPipeServerName)
    {
      try
      {
        if (this.NamedPipeServerSet == null)
          throw new Exception("The NamedPipeServerSet is null.");

        if (!this.NamedPipeServerSet.ContainsKey(namedPipeServerName))
          throw new Exception("The NamedPipeServerSet does not contain a NamedPipeServer named '" + namedPipeServerName + "'.");

        var namedPipeServer = this.NamedPipeServerSet[namedPipeServerName];

        namedPipeServer.BeginListening();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to begin listening to the NamedPipe hosted by the NamedPipeServer named '" +
                            namedPipeServerName + "'.", ex);
      }
    }
  }
}
