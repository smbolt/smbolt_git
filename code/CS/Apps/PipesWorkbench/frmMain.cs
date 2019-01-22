using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.Pipes;
using Org.GS;

namespace Org.PipesWorkbench
{
  public partial class frmMain : Form
  {
    private int _threadLimit = 4;

    private List<string> _namedPipeServers;
    private List<string> _namedPipes;
    private Dictionary<string, List<string>> _pipesOnServers;

    private NamedPipeServerSupervisor _supervisor;
    private string _mainNamedPipeServerName;
    private string _mainNamedPipeName;
    private int _maxMainPipeInstances;

    private NamedPipeClient _namedPipeClient;
    private string _pipeList64Path;
    private string _namedPipePattern;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }
    
    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "CreateNamedPipeServer":
          CreateNamedPipeServer();
          break;

        case "CreateNamedPipe":
          CreateNamedPipe();
          break;

        case "BeginNamedPipeListening":
          BeginNamedPipeListening();
          break;

        case "CreateNamedPipeClient":
          CreateNamedPipeClient();
          break;

        case "SendMessage":
          SendMessage();
          break;

        case "CloseNamedPipeClient":
          CloseNamedPipeClient();
          break;

        case "GetExistingNamedPipes":
          GetExistingNamedPipes();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void CreateNamedPipeServer()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        _supervisor.CreateNamedPipeServer(_mainNamedPipeServerName, _mainNamedPipeName, _maxMainPipeInstances);
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred in the NamedPipeServerSupervisor while attempting to create a NamedPipeServer named '" +
                        _mainNamedPipeServerName + "'." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - NamedPipeServer Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      this.Cursor = Cursors.Default;
    }

    private void CreateNamedPipe()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        _supervisor.CreateNamedPipe(_mainNamedPipeServerName, _mainNamedPipeName, _maxMainPipeInstances);
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred in the NamedPipeServerSupervisor while attempting to create a NamedPipe named '" +
                        _mainNamedPipeName + "'." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - NamedPipe Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      this.Cursor = Cursors.Default;
    }

    private void BeginNamedPipeListening()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        _supervisor.BeginListeningOnNamedPipe(_mainNamedPipeServerName);
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred in the NamedPipeServerSupervisor while attempting to begin listening on the NamedPipe " +
                        "hosted by the NamedPipeServer named '" + _mainNamedPipeServerName + "'." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - NamedPipe BeginListening Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      this.Cursor = Cursors.Default;
    }

    private void RunServer()
    {
      try
      {
        int i;
        Thread[] servers = new Thread[_threadLimit];

        WriteToDisplay("Named pipe server stream with impersonation example." + g.crlf + "Waiting for client to connect...");

        for (i = 0; i < _threadLimit; i++)
        {
          servers[i] = new Thread(ServerThread);
          servers[i].Start();
        }

        Thread.Sleep(250);

        while (i > 0)
        {
          for (int j = 0; j < _threadLimit; j++)
          {
            if (servers[j] != null)
            {
              if (servers[j].Join(250))
              {
                WriteToDisplay("Server thread finished - thread id " + servers[j].ManagedThreadId.ToString() + ".");
                servers[j] = null;
                i--;    // decrement the thread watch count
              }
            }
          }
        }

        WriteToDisplay("Server threads exhausted, exiting...");
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while running the named pipe server.", ex); 
      }
    }


    private void ServerThread(object data)
    {
      NamedPipeServerStream pipeServer = new NamedPipeServerStream("REMOTE_HOST_01", PipeDirection.InOut, _threadLimit);

      int threadId = Thread.CurrentThread.ManagedThreadId;

      // Wait for a client to connect
      pipeServer.WaitForConnection();

      WriteToDisplay("Client connected on thread " + threadId.ToString() + ".");
      try
      {
        // Read the request from the client. Once the client has
        // written to the pipe its security token will be available.

        StreamString ss = new StreamString(pipeServer);

        // Verify our identity to the connected client using a
        // string that the client anticipates.

        ss.WriteString("I am the one true server!");
        string filename = ss.ReadString();

        // Read in the contents of the file while impersonating the client.
        ReadFileToStream fileReader = new ReadFileToStream(ss, filename);

        // Display the name of the user we are impersonating.
        WriteToDisplay("Reading file: " + filename + " on thread " + threadId.ToString() + " as user  '" + pipeServer.GetImpersonationUserName() + ".");
        pipeServer.RunAsClient(fileReader.Start);
      }
      // Catch the IOException that is raised if the pipe is broken
      // or disconnected.
      catch (IOException ex)
      {
        WriteToDisplay("An exception occurred in the pipe server." + ex.ToReport()); 
      }

      pipeServer.Close();
    }

    private void CreateNamedPipeClient()
    {
      _namedPipeClient = new NamedPipeClient();
    }

    private void SendMessage()
    {
      string message = @"C:\_work\path.txt";

      string response = _namedPipeClient.SendMessage(message);
      WriteToDisplay("Response: " + response); 
    }

    private void CloseNamedPipeClient()
    {
      _namedPipeClient.Close();
    }

    private void GetExistingNamedPipes()
    {
      try
      {
        lblExistingNamedPipesValue.Text = String.Empty;
        Application.DoEvents();
        Thread.Sleep(100);

        using (var namedPipeUtility = new NamedPipeUtility(_pipeList64Path))
        {
          var pipeSet = namedPipeUtility.GetNamedPipeSet(_namedPipePattern);
          var sb = new StringBuilder();

          if (pipeSet.Count == 0)
          {
            lblExistingNamedPipesValue.Text = "None";
            return; 
          }

          foreach (var pipe in pipeSet.Values)
          {
            if (sb.Length > 0)
              sb.Append(g.crlf);
            sb.Append(pipe.PipeName + " (" + pipe.Instances.ToString() + "/" + pipe.MaxInstances.ToString() + ")");
          }

          string displayValue = sb.ToString();
          lblExistingNamedPipesValue.Text = displayValue;
        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting get a list of the existing named pipes." + g.crlf + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Getting List of Named Pipes", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      this.Cursor = Cursors.Default;
    }

    private void WriteToDisplay(string message)
    {
      try
      {
        this.Invoke((Action)((() =>
        {
          txtServersReport.Text += message + g.crlf;
        })));
      }
      catch (Exception ex)
      {
        this.Invoke((Action)((() =>
        {
          MessageBox.Show("An exception occurred while attempting to write to the display." + g.crlf + ex.ToReport(),
                          g.AppInfo.AppName + " - Exception Writing to Display", MessageBoxButtons.OK, MessageBoxIcon.Error); 


        })));
      }
    }

    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the application object 'a'." + g.crlf2 + ex.ToReport(),
                        "Pipes Workbench - Application Object 'a' Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try
      {
        _pipeList64Path = g.CI("PipeList64Path");
        _namedPipeServers = g.GetList("NamedPipeServers");
        _namedPipes = g.GetList("NamedPipes");

        _pipesOnServers = new Dictionary<string, List<string>>();

        foreach (var namedPipeServer in _namedPipeServers)
        {
          if (_pipesOnServers.ContainsKey(namedPipeServer))
            throw new Exception("A duplicate entry '" + namedPipeServer + "' was found in the NamedPipeServers configuration list.");
          _pipesOnServers.Add(namedPipeServer, new List<string>());
        }

        foreach (var namedPipe in _namedPipes)
        {
          string[] tokens = namedPipe.Split(Constants.ColonDelimiter, StringSplitOptions.RemoveEmptyEntries);
          if (tokens.Length != 2)
            throw new Exception("The format of the value '" + namedPipe + "' in the NamedPipes configuration list is invalid.");

          string serverName = tokens[0];
          string pipeSpec = tokens[1];

          if (!_pipesOnServers.ContainsKey(serverName))
            throw new Exception("The NamedPipeServer '" + serverName + "' is not in the NamedPipeServers configuration list - " +
                                "therefore the value in the NamedPipes configuration list '" + namedPipe + "' is invalid.");

          int pos = pipeSpec.IndexOf('[');
          if (pos == -1)
            throw new Exception("The format of the NamedPipe '" + namedPipe + "' in the NamedPipe configuration list is invalid. " +
                                "The pipe name should include the max instances specification in brackets i.e. PipeName[4].");

          string pipeName = pipeSpec.Substring(0, pos);
          string maxInstanceSpec = pipeSpec.Substring(pos).Replace("[", String.Empty).Replace("]", String.Empty); 
          if (maxInstanceSpec.IsNotNumeric())
            throw new Exception("The format of the NamedPipe '" + namedPipe + "' in the NamedPipe configuration list is invalid. " +
                                "The pipe name should include the max instances specification in brackets i.e. PipeName[4]. " + 
                                "The max instances value '" + maxInstanceSpec + "' is not numeric.");

          _pipesOnServers[serverName].Add(pipeSpec);
        }

        // add servers and pipes to new combo boxes... 


        _mainNamedPipeServerName = g.CI("MainNamedPipeServerName"); 
        _mainNamedPipeName = g.CI("MainNamedPipeName");
        _namedPipePattern = g.CI("NamedPipePattern");
        _maxMainPipeInstances = g.CI("MaxMainPipeInstances").ToInt32OrDefault(-1);

        _supervisor = new NamedPipeServerSupervisor(_pipeList64Path);

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 + ex.ToReport(),
                        "Pipes Workbench - Application Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
