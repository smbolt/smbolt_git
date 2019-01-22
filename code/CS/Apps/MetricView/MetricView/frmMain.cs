using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace Teleflora.Operations.MetricView
{
    public partial class frmMain : Form
    {
        private MetricViewConfiguration config;
        private MetricDataObjects dataObjects;
        private frmSplash fSplash;
        private char[] controlDelimiter = { ',' };
        private string[] args;
        string LastAction = String.Empty;
        private bool IsFirstShowing = true;
        private bool IsFileSavedOnExit = false;
        private bool IsNewUnsavedProfile = false;
        private string configFileName = String.Empty;
        private string formTitle = String.Empty;
        private string titleFileName = String.Empty;

        public frmMain()
        {
            InitializeComponent();

            InitializeApplication();
        }

        private void InitializeApplication()
        {
            formTitle = GeneralUtility.GetApplicationBuildString();
            this.Text = formTitle;
            this.titleFileName = GeneralUtility.GetApplicationBuildString();
            IsFirstShowing = true;
            InitializeControls();
        }

        public void SetArgs(string[] Args)
        {
            args = Args;
        }

        private void LoadGraphsFromConfig()
        {
            bool IsSplashVisible = false;
            int totalGraphs = 0;
            int graphCount = 0;

            if (fSplash != null)
                IsSplashVisible = true;

            totalGraphs = config.MetricGraphs.Count;
            string[] oldNames = new string[totalGraphs];
            string[] newNames = new string[totalGraphs];

            foreach (KeyValuePair<string, MetricGraphConfiguration> listEntry in config.MetricGraphs)
            {
                string name = listEntry.Key;
                oldNames[graphCount] = name;
                if (name.Substring(0, 2) != "~[")                
                    newNames[graphCount] = "~[" + graphCount.ToString("0000") + "]"
                        + name;
                else
                    newNames[graphCount] = String.Empty;
                graphCount++;
            }

            for(int i = 0; i < config.MetricGraphs.Count; i++)
            {
                if (newNames[i] != String.Empty)
                {
                    MetricGraphConfiguration cfg = config.MetricGraphs[oldNames[i]];
                    config.MetricGraphs.Remove(oldNames[i]);
                    cfg.GraphName = newNames[i];
                    config.MetricGraphs.Add(cfg.GraphName, cfg);
                }
            }

            graphCount = 0;

            foreach(KeyValuePair<string, MetricGraphConfiguration> listEntry in config.MetricGraphs)
            {
                string name = GeneralUtility.StripSeqFromName(listEntry.Value.GraphName);
                if (IsSplashVisible)
                {
                    graphCount++;
                    fSplash.SetMessage("Loading Graph " + graphCount.ToString().Trim() + " of " +
                        totalGraphs.ToString().Trim() + "\r\n" + name);
                    Application.DoEvents();
                }

                MetricGraphConfiguration graphConfig = listEntry.Value;

                MetricGraph graph = new MetricGraph(this.dataObjects);
                graph.GraphName = graphConfig.GraphName;
                graph.Name = graph.GraphName;
                graph.MetricGraphConfig = graphConfig;
                graph.MenuAction += this.GraphMenuAction;
                graph.SaveImageToFile += this.SaveImageToFile;
                graph.GraphValue += this.GraphValue;
                graph.UpdateConfigLocAndSize += this.UpdateConfigLocAndSize;
                pnlContent.Controls.Add(graph);
                graph.ResizeGraph(true);
            }
        }

        private void mnuOptionsAddGraph_Click(object sender, EventArgs e)
        {
            MetricGraph graph = new MetricGraph(dataObjects);
            graph.Visible = false;
            graph.Left = 5;
            graph.Top = 5;
            graph.BorderStyle = BorderStyle.FixedSingle;
            graph.GraphName = "MetricGraph" + config.MetricGraphs.NextNumber;
            graph.Name = graph.GraphName;
            graph.Size = new Size(700, 300);
            //// rather than doing the above

            graph.MenuAction += this.GraphMenuAction;
            graph.SaveImageToFile += this.SaveImageToFile;
            graph.GraphValue += this.GraphValue;
            graph.UpdateConfigLocAndSize += this.UpdateConfigLocAndSize;

            pnlContent.Controls.Add(graph);
            graph.BringToFront();
            graph.FrontAndCenterNew();

            MetricGraphConfiguration metricGraphConfig = graph.MetricGraphConfig;
            metricGraphConfig.IsActive = true;
            metricGraphConfig.GraphName = graph.GraphName;
            metricGraphConfig.GraphSize = graph.Size;
            metricGraphConfig.GraphLocation = new Point(graph.Left, graph.Top);
            metricGraphConfig.IsSelected = false;
            graph.Reconfigure();
            graph.Visible = true;

            config.MetricGraphs.Add(graph.GraphName, metricGraphConfig);

            for (int x = 1; x < pnlContent.Controls.Count; x++)
            {
                Control c = (Control)pnlContent.Controls[x];
                Console.WriteLine(c.Name);
            }

        }

        private void GraphValue(string graph, string metric, string time, string value)
        {
            //lblStatus.Text = sender + " - " + XLabel + " - " + value;
            sbLblGraphData.Text = graph;
            sbLblMetricData.Text = metric;
            sbLblTimeData.Text = time;
            sbLblValueData.Text = value;
            Application.DoEvents();
        }


        private void GraphMenuAction(string graphName, string menuAction)
        {
            for (int x = 0; x < pnlContent.Controls.Count; x++)
            {
                MetricGraph c = (MetricGraph)pnlContent.Controls[x];

                if (menuAction.CompareTo("SELECT") == 0)
                {
                    foreach (KeyValuePair<string, MetricGraphConfiguration> listEntry in config.MetricGraphs)
                    {
                        string configGraphName = listEntry.Key;
                        MetricGraphConfiguration graphConfig = listEntry.Value;
                        graphConfig.IsSelected = false;
                        MetricGraph graph = (MetricGraph)pnlContent.Controls[configGraphName];
                        graph.SetSelectionAndActivation();
                    }
                    config.MetricGraphs[c.GraphName].IsSelected = true;
                    c.SetSelectionAndActivation();
                    return;
                }

                if (menuAction.CompareTo("SAVE_TO_FILE")== 0)
                {
                    SaveGraphToFile(graphName);
                    return;
                }

                if (menuAction.CompareTo("SQL_CONNECTION_LOST") == 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    foreach (KeyValuePair<string, MetricGraphConfiguration> listEntry in config.MetricGraphs)
                    {
                        string configGraphName = listEntry.Key;
                        MetricGraphConfiguration graphConfig = listEntry.Value;
                        graphConfig.IsActive = false;
                        MetricGraph graph = (MetricGraph)pnlContent.Controls[configGraphName];
                        graph.Reconfigure();
                    }
                    this.Cursor = Cursors.Arrow;
                    MessageBox.Show("Database Connection Failure\r\n\r\nAll graphs have been inactivated",
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (c.Name.CompareTo(graphName) == 0)
                {
                    switch (menuAction)
                    {
                        case "REMOVE_GRAPH":
                            if (MessageBox.Show("Are you sure you want to permanently remove the graph?",
                                "Permanently remove this graph?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                return;
                            pnlContent.Controls.RemoveAt(x);
                            config.MetricGraphs.Remove(c.Name);
                            if (pnlContent.Controls.Count == 0)
                                config.MetricGraphs.NextNumber = -1;
                            c.Dispose();
                            break;

                        case "ACTIVATE_GRAPH":
                            config.MetricGraphs[graphName].IsActive = true;
                            c.SetSelectionAndActivation();
                            break;

                        case "DEACTIVATE_GRAPH":
                            config.MetricGraphs[graphName].IsActive = false;
                            c.SetSelectionAndActivation();
                            break;

                        case "SELECT":
                            config.MetricGraphs[graphName].IsSelected = true;
                            c.SetSelectionAndActivation();
                            break;

                        case "PROPERTIES":
                            MetricGraphConfiguration cfg = config.MetricGraphs[graphName];
                            string saveName = cfg.GraphName;
                            c.IsSuspended = true;
                            frmGraphProperties fGraphProperties = 
                                new frmGraphProperties(config.MetricGraphs[graphName], dataObjects);
                            fGraphProperties.ShowDialog();
                            c.Refresh();
                            Application.DoEvents();
                            c.IsSuspended = false;
                            c.Reconfigure();
                            if (cfg.GraphName.CompareTo(saveName) != 0)
                            {
                                c.Name = cfg.GraphName;
                                config.MetricGraphs.Remove(saveName);
                                config.MetricGraphs.Add(cfg.GraphName, cfg);
                            }
                            c.RefreshGraph(true);
                            break;

                        case "REFRESH_GRAPH":
                            c.RefreshGraph(true);
                            break;

                        case "CAPTURE_IMAGE":
                            c.SendChartImageToClipboard();
                            break;

                        case "MAXIMIZE":
                            c.MaximizeGraph();
                            break;

                        case "FRONT_AND_CENTER":
                            c.FrontAndCenter();
                            break;

                        case "RESTORE_POSITION":
                            c.RestorePosition();
                            break;
                    }

                }

            }
        }

        private void UpdateConfigLocAndSize(MetricGraphConfiguration metricGraphConfig)
        {
            config.MetricGraphs[metricGraphConfig.GraphName] = metricGraphConfig;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsFileSavedOnExit)
                return;

            IsFileSavedOnExit = true;
            if (configFileName.CompareTo(String.Empty) != 0)
            {
                switch (MessageBox.Show("Do you want to save the current profile?", "Exiting MetricView...",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        ConfigHelper.PutConfiguration(config, configFileName);
                        IsNewUnsavedProfile = false;
                        mnuFileSave.Enabled = true;
                        pnlContent.Visible = false;
                        Application.DoEvents();
                        pnlContent.Controls.Clear();
                        config.MetricGraphs.Clear();
                        this.Text = formTitle;
                        configFileName = String.Empty;
                        Application.DoEvents();
                        break;
                }
            }

            if (CheckTerminateApplication())
                this.Close();


            if (configFileName.CompareTo(String.Empty) != 0)
            {
                ConfigHelper.PutConfiguration(config, configFileName);
            }
                
            dataObjects.CloseConnection();
        }

        private void pnlContent_MouseDown(object sender, MouseEventArgs e)
        {
            DeselectAllGraphs();
        }

        private void DeselectAllGraphs()
        {
            for (int x = 0; x < pnlContent.Controls.Count; x++)
            {
                MetricGraph c = (MetricGraph)pnlContent.Controls[x];
                config.MetricGraphs[c.GraphName].IsSelected = false;
                c.SetSelectionAndActivation();
            }
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            DeselectAllGraphs();

            if (configFileName.CompareTo(String.Empty) != 0)
            {
                ConfigHelper.PutConfiguration(config, configFileName);
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            if (IsFirstShowing)
            {
                this.Cursor = Cursors.AppStarting;
                fSplash = new frmSplash();
                fSplash.TopMost = false;
                fSplash.Owner = this;
                fSplash.SetBuildString(GeneralUtility.GetApplicationBuildString());
                fSplash.SetMessage("Initializing...");
                fSplash.Show();
                Application.DoEvents();
                System.Threading.Thread.Sleep(200);
                IsFirstShowing = false;

                fSplash.SetMessage("Initializing data objects");
                Application.DoEvents();
                dataObjects = new MetricDataObjects();

                fSplash.SetMessage("Loading Environments");
                Application.DoEvents();
                dataObjects.GetEnvironments();
                System.Threading.Thread.Sleep(100);

                fSplash.SetMessage("Loading Systems");
                Application.DoEvents();
                dataObjects.GetSystems();
                System.Threading.Thread.Sleep(100);

                fSplash.SetMessage("Loading Applications");
                Application.DoEvents();
                dataObjects.GetApplications();
                System.Threading.Thread.Sleep(100);

                fSplash.SetMessage("Loading MetricTypes");
                Application.DoEvents();
                dataObjects.GetMetricTypes();
                System.Threading.Thread.Sleep(100);

                fSplash.SetMessage("Loading MetricStates");
                Application.DoEvents();
                dataObjects.GetMetricStates();
                System.Threading.Thread.Sleep(100);

                fSplash.SetMessage("Loading AggregateTypes");
                Application.DoEvents();
                dataObjects.GetAggregateTypes();
                System.Threading.Thread.Sleep(100);

                fSplash.SetMessage("Loading MetricValueTypes");
                Application.DoEvents();
                dataObjects.GetMetricValueTypes();
                System.Threading.Thread.Sleep(100);

                fSplash.SetMessage("Loading Servers");
                Application.DoEvents();
                dataObjects.GetServers();
                System.Threading.Thread.Sleep(100);

                fSplash.SetMessage("Loading Intervals");
                Application.DoEvents();
                dataObjects.GetIntervals();
                System.Threading.Thread.Sleep(100);

                fSplash.SetMessage("Loading Metrics");
                Application.DoEvents();
                dataObjects.GetMetrics();
                System.Threading.Thread.Sleep(100);

                fSplash.SetMessage("Loading available metrics - please wait");
                Application.DoEvents();
                MetricQueryParms parms = new MetricQueryParms();
                parms.IsEnvironmentIDSpecified = true;
                parms.EnvironmentID = 1;
                parms.IsTargetSystemIDSpecified = false;
                parms.IsTargetApplicationIDSpecified = false;
                dataObjects.GetAvailableMetrics(parms);

                bool IsFileNameArgSupplied = false;

                if (args.Length > 0)
                {
                    System.Threading.Thread.Sleep(200);
                    fSplash.SetMessage("Command line arguments = " + args[0]);
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(200);
                    configFileName = @"c:\Program Files\MetricView1.0\" + args[0];
                    if (File.Exists(configFileName))
                    {
                        IsFileNameArgSupplied = true;
                        fSplash.SetMessage("Initializing configuration");
                        Application.DoEvents();
                        config = ConfigHelper.GetConfiguration(configFileName);
                        titleFileName = System.IO.Path.GetFileNameWithoutExtension(configFileName);
                        this.Text = formTitle + " - " + titleFileName;
                    }
                    else
                        configFileName = String.Empty;
                }
                else
                {
                    System.Threading.Thread.Sleep(200);
                    fSplash.SetMessage("No command line arguments supplied");
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(200);
                }

                if (!IsFileNameArgSupplied)
                {
                    dlgOpenFile.Title = "Open MetricView Profile";
                    dlgOpenFile.Filter = @"MetricView Profiles (*.mvp)|*.mvp";
                    dlgOpenFile.InitialDirectory = @"c:\Program Files\MetricView1.0";

                    if (this.dlgOpenFile.ShowDialog() == DialogResult.OK)
                    {
                        configFileName = dlgOpenFile.FileName;
                        fSplash.SetMessage("Initializing configuration");
                        Application.DoEvents();
                        config = ConfigHelper.GetConfiguration(configFileName);
                        titleFileName = System.IO.Path.GetFileNameWithoutExtension(configFileName);
                        this.Text = formTitle + " - " + titleFileName;
                    }
                }

                if (configFileName.CompareTo(String.Empty) != 0)
                {
                    long startTicks = DateTime.Now.Ticks;
                    fSplash.SetMessage("Loading graphs - please wait");
                    Application.DoEvents();
                    LoadGraphsFromConfig();
                    long durationTicks = DateTime.Now.Ticks - startTicks;
                    TimeSpan durationSpan = new TimeSpan(durationTicks);
                    lblStatus.Text = "Time to load graphs = " + durationSpan.TotalSeconds.ToString().Trim() + " seconds.";
                    pnlContent.Visible = true;
                }
                else
                {
                    config = new MetricViewConfiguration();
                    pnlContent.Visible = false;
                    mnuOptionsAddGraph.Enabled = false;
                }

                fSplash.Close();
                this.Cursor = Cursors.Arrow;
                this.BringToFront();

                timerControl.Interval = 2000;
                timerControl.Enabled = true;
            }
           

        }

        private void mnuFileSaveProfileAs_Click(object sender, EventArgs e)
        {
            DeselectAllGraphs();

            if (configFileName.CompareTo(String.Empty) == 0)
                return;

            dlgSaveFile.Title = "Save MetricView Profile as";
            dlgSaveFile.Filter = @"MetricView Profiles (*.mvp)|*.mvp";
            dlgSaveFile.InitialDirectory = @"c:\Program Files\MetricView1.0";
            dlgSaveFile.FileName = String.Empty;
            if (this.dlgSaveFile.ShowDialog() == DialogResult.OK)
            {
                configFileName = dlgSaveFile.FileName;
                titleFileName = System.IO.Path.GetFileNameWithoutExtension(configFileName);
                this.Text = formTitle + " - " + titleFileName;
                ConfigHelper.PutConfiguration(config, configFileName);
                IsNewUnsavedProfile = false;
                mnuFileSave.Enabled = true;
            }
        }

        private void mnuFileOpenProfile_Click(object sender, EventArgs e)
        {
            DeselectAllGraphs();

            mnuOptionsAddGraph.Enabled = true;
            string holdConfigFileName = configFileName;
            if(configFileName.CompareTo(String.Empty) != 0)
            {
                switch(MessageBox.Show("Do you want to save the current profile?", "Preparing to open new profile...", 
                    MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question))
                {
                    case DialogResult.Cancel:
                        return;

                    case DialogResult.No:
                        pnlContent.Visible = false;
                        Application.DoEvents();
                        pnlContent.Controls.Clear();
                        config.MetricGraphs.Clear();
                        this.Text = formTitle;
                        configFileName = String.Empty;
                        Application.DoEvents();
                        break;

                    case DialogResult.Yes:
                        ConfigHelper.PutConfiguration(config, configFileName);
                        pnlContent.Visible = false;
                        Application.DoEvents();
                        pnlContent.Controls.Clear();
                        config.MetricGraphs.Clear();
                        this.Text = formTitle;
                        configFileName = String.Empty;
                        Application.DoEvents();
                        break;
                }
            }
            
            IsNewUnsavedProfile = false;
            mnuFileSave.Enabled = true;

            dlgOpenFile.Title = "Open MetricView Profile";
            dlgOpenFile.Filter = @"MetricView Profiles (*.mvp)|*.mvp";
            dlgOpenFile.InitialDirectory = @"c:\Program Files\MetricView1.0";

            if (this.dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                if (holdConfigFileName.CompareTo(dlgOpenFile.FileName) == 0)
                {
                    MessageBox.Show("You cannot re-open the same profile\r\n" +
                    "Use the File-Open menu to attempt again with orignal file closed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Cursor = Cursors.Arrow;
                    pnlContent.Visible = true;
                    return;
                }
                this.Cursor = Cursors.AppStarting;
                fSplash = new frmSplash();
                fSplash.Show();
                fSplash.SetMessage("Loading graphs - please wait");
                fSplash.SetBuildString(GeneralUtility.GetApplicationBuildString());
                Application.DoEvents();
                LoadGraphsFromConfig();

                configFileName = dlgOpenFile.FileName;
                config = ConfigHelper.GetConfiguration(configFileName);
                titleFileName = System.IO.Path.GetFileNameWithoutExtension(configFileName);
                this.Text = formTitle + " - " + titleFileName;
                LoadGraphsFromConfig();
                fSplash.Close();
                mnuOptionsAddGraph.Enabled = true;
                pnlContent.Visible = true;
            }
            else
            {
                mnuOptionsAddGraph.Enabled = false;
                pnlContent.Visible = false;
            }

            this.Cursor = Cursors.Arrow;
        }

        private void mnuFileCloseProfile_Click(object sender, EventArgs e)
        {
            DeselectAllGraphs();

            if (configFileName.CompareTo(String.Empty) != 0)
            {
                switch (MessageBox.Show("Do you want to save the current profile?", "Closing current profile...",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Cancel:
                        return;

                    case DialogResult.No:
                        pnlContent.Visible = false;
                        Application.DoEvents();
                        pnlContent.Controls.Clear();
                        config.MetricGraphs.Clear();
                        this.Text = formTitle;
                        configFileName = String.Empty;
                        Application.DoEvents();
                        IsNewUnsavedProfile = false;
                        mnuFileSave.Enabled = true;
                        break;

                    case DialogResult.Yes:
                        ConfigHelper.PutConfiguration(config, configFileName);
                        IsNewUnsavedProfile = false;
                        mnuFileSave.Enabled = true;
                        pnlContent.Visible = false;
                        Application.DoEvents();
                        pnlContent.Controls.Clear();
                        config.MetricGraphs.Clear();
                        this.Text = formTitle;
                        configFileName = String.Empty;
                        Application.DoEvents();
                        break;
                }
            }

            mnuOptionsAddGraph.Enabled = false;
        }

        private void mnuFileNewProfile_Click(object sender, EventArgs e)
        {
            DeselectAllGraphs();

            if (configFileName.CompareTo(String.Empty) != 0)
            {
                switch (MessageBox.Show("Do you want to save the current profile?", "Preparing to open new profile...",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Cancel:
                        return;

                    case DialogResult.No:
                        pnlContent.Visible = false;
                        Application.DoEvents();
                        pnlContent.Controls.Clear();
                        config.MetricGraphs.Clear();
                        this.Text = formTitle;
                        configFileName = String.Empty;
                        Application.DoEvents();
                        break;

                    case DialogResult.Yes:
                        if (IsNewUnsavedProfile)
                        {
                            dlgSaveFile.Title = "Save MetricView Profile as";
                            dlgSaveFile.Filter = @"MetricView Profiles (*.mvp)|*.mvp";
                            dlgSaveFile.InitialDirectory = @"c:\Program Files\MetricView1.0";
                            dlgSaveFile.FileName = String.Empty;
                            if (this.dlgSaveFile.ShowDialog() == DialogResult.OK)
                            {
                                configFileName = dlgSaveFile.FileName;
                                ConfigHelper.PutConfiguration(config, configFileName);
                            }
                        }
                        else
                        {
                            ConfigHelper.PutConfiguration(config, configFileName);
                        }
                        pnlContent.Controls.Clear();
                        config.MetricGraphs.Clear();
                        this.Text = formTitle;
                        configFileName = String.Empty;
                        Application.DoEvents();
                        break;
                }
            }

            pnlContent.Controls.Clear();
            config.MetricGraphs.Clear();
            pnlContent.Visible = true;
            Application.DoEvents();
            configFileName = "New Profile";
            titleFileName = configFileName;
            this.Text = formTitle + " - " + titleFileName;
            IsNewUnsavedProfile = true;
            mnuFileSave.Enabled = false; 
            mnuOptionsAddGraph.Enabled = true;
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            DeselectAllGraphs();

            IsFileSavedOnExit = true;
            if (configFileName.CompareTo(String.Empty) != 0)
            {
                switch (MessageBox.Show("Do you want to save the current profile?", "Exiting MetricView...",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        ConfigHelper.PutConfiguration(config, configFileName);
                        IsNewUnsavedProfile = false;
                        mnuFileSave.Enabled = true;
                        pnlContent.Visible = false;
                        Application.DoEvents();
                        pnlContent.Controls.Clear();
                        config.MetricGraphs.Clear();
                        this.Text = formTitle;
                        configFileName = String.Empty;
                        Application.DoEvents();
                        break;
                }
            }

            if (CheckTerminateApplication())
                this.Close();
        }

        private void mnuOptionsActivateAll_Click(object sender, EventArgs e)
        {
            ActivateAll();
        }

        private void ActivateAll()
        {
            this.Cursor = Cursors.WaitCursor;
            foreach (KeyValuePair<string, MetricGraphConfiguration> listEntry in config.MetricGraphs)
            {
                string graphName = listEntry.Key;
                MetricGraphConfiguration graphConfig = listEntry.Value;
                graphConfig.IsActive = true;
                MetricGraph graph = (MetricGraph) pnlContent.Controls[graphName];
                graph.Reconfigure();
            }
            this.Cursor = Cursors.Arrow;
        }

        private void mnuOptionsDeactivateAll_Click(object sender, EventArgs e)
        {
            DeactivateAll();
        }

        private void DeactivateAll()
        {
            this.Cursor = Cursors.WaitCursor;
            foreach (KeyValuePair<string, MetricGraphConfiguration> listEntry in config.MetricGraphs)
            {
                string graphName = listEntry.Key;
                MetricGraphConfiguration graphConfig = listEntry.Value;
                graphConfig.IsActive = false;
                MetricGraph graph = (MetricGraph) pnlContent.Controls[graphName];
                graph.Reconfigure();
            }
            this.Cursor = Cursors.Arrow;
        }



        private void InitializeControls()
        {
            pnlControl.Visible = false;
            pnlContent.Visible = false;
        }

        private bool CheckTerminateApplication()
        {
            return true;
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    pnlControl.Visible = !pnlControl.Visible;
                    break;

                case Keys.F6:
                    if (e.Control)
                    {
                        frmMetrics fMetrics = new frmMetrics(dataObjects);
                        fMetrics.ShowDialog();
                    }
                    break;

                //case Keys.F10:
                //    for (int x = 0; x < config.MetricGraphs.Count; x++)
                //    {
                //        if (config.MetricGraphs.Values[x].IsSelected)
                //        {
                //            MetricGraph metricGraph = (MetricGraph)pnlContent.Controls[config.MetricGraphs.Keys[x]];
                //            metricGraph.IsRollup = !metricGraph.IsRollup;
                //            lblStatus.Text = metricGraph.IsRollup.ToString();
                //            metricGraph.RefreshGraph(false);
                //        }
                //    }
                //    break;

                case Keys.Right:
                    for (int x = 0; x < pnlContent.Controls.Count; x++)
                    {
                        MetricGraph c = (MetricGraph)pnlContent.Controls[x];
                        if (config.MetricGraphs[c.GraphName].IsSelected)
                        {
                            if (e.Control)
                            {
                                config.MetricGraphs[c.GraphName].GraphSize = new Size(
                                    config.MetricGraphs[c.GraphName].GraphSize.Width + 1,
                                    config.MetricGraphs[c.GraphName].GraphSize.Height);
                                c.Nudge("INCREASE_WIDTH");
                            }
                            else
                            {
                                config.MetricGraphs[c.GraphName].GraphLocation = new Point(
                                    config.MetricGraphs[c.GraphName].GraphLocation.X + 1,
                                    config.MetricGraphs[c.GraphName].GraphLocation.Y);
                                c.Nudge("RIGHT");
                            }
                        }
                    }
                    break;

                case Keys.Left:
                    for (int x = 0; x < pnlContent.Controls.Count; x++)
                    {
                        MetricGraph c = (MetricGraph)pnlContent.Controls[x];
                        if (config.MetricGraphs[c.GraphName].IsSelected)
                        {
                            if (e.Control)
                            {
                                config.MetricGraphs[c.GraphName].GraphSize = new Size(
                                    config.MetricGraphs[c.GraphName].GraphSize.Width - 1,
                                    config.MetricGraphs[c.GraphName].GraphSize.Height);
                                c.Nudge("DECREASE_WIDTH");
                            }
                            else
                            {
                                config.MetricGraphs[c.GraphName].GraphLocation = new Point(
                                    config.MetricGraphs[c.GraphName].GraphLocation.X - 1,
                                    config.MetricGraphs[c.GraphName].GraphLocation.Y);
                                c.Nudge("LEFT");
                            }
                        }
                    }
                    break;

                case Keys.Down:
                    for (int x = 0; x < pnlContent.Controls.Count; x++)
                    {
                        MetricGraph c = (MetricGraph)pnlContent.Controls[x];
                        if (config.MetricGraphs[c.GraphName].IsSelected)
                        {
                            if (e.Control)
                            {
                                config.MetricGraphs[c.GraphName].GraphSize = new Size(
                                    config.MetricGraphs[c.GraphName].GraphSize.Width,
                                    config.MetricGraphs[c.GraphName].GraphSize.Height + 1);
                                c.Nudge("INCREASE_HEIGHT");
                            }
                            else
                            {
                                config.MetricGraphs[c.GraphName].GraphLocation = new Point(
                                    config.MetricGraphs[c.GraphName].GraphLocation.X,
                                    config.MetricGraphs[c.GraphName].GraphLocation.Y + 1);
                                c.Nudge("DOWN");
                            }
                        }
                    }
                    break;

                case Keys.Up:
                    for (int x = 0; x < pnlContent.Controls.Count; x++)
                    {
                        MetricGraph c = (MetricGraph)pnlContent.Controls[x];
                        if (config.MetricGraphs[c.GraphName].IsSelected)
                        {
                            if (e.Control)
                            {
                                config.MetricGraphs[c.GraphName].GraphSize = new Size(
                                    config.MetricGraphs[c.GraphName].GraphSize.Width,
                                    config.MetricGraphs[c.GraphName].GraphSize.Height - 1);
                                c.Nudge("DECREASE_HEIGHT");
                            }
                            else
                            {
                                config.MetricGraphs[c.GraphName].GraphLocation = new Point(
                                    config.MetricGraphs[c.GraphName].GraphLocation.X,
                                    config.MetricGraphs[c.GraphName].GraphLocation.Y - 1);
                                c.Nudge("UP");
                            }
                        }
                    }
                    break;

                case Keys.Escape:
                    DeselectAllGraphs();
                    break;

                case Keys.Delete:
                    for (int x = 0; x < pnlContent.Controls.Count; x++)
                    {
                        MetricGraph c = (MetricGraph)pnlContent.Controls[x];
                        if (config.MetricGraphs[c.GraphName].IsSelected)
                        {
                            if (MessageBox.Show("Are you sure you want to permanently remove the graph?",
                                "Permanently remove this graph?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                return;

                            pnlContent.Controls.RemoveAt(x);
                            config.MetricGraphs.Remove(c.Name);
                            if (pnlContent.Controls.Count == 0)
                                config.MetricGraphs.NextNumber = -1;
                            c.Dispose();
                            break;
                        }
                    }
                    break;

                case Keys.PageUp:
                    for (int x = 0; x < pnlContent.Controls.Count; x++)
                    {
                        MetricGraph c = (MetricGraph)pnlContent.Controls[x];
                        if (config.MetricGraphs[c.GraphName].IsSelected)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            int newDataPoints = 100;
                            if (e.Control)
                            {

                            }
                            else
                            {
                                switch (config.MetricGraphs[c.GraphName].IncludedMetrics.DataPoints)
                                {
                                    case 10: newDataPoints = 25; break;
                                    case 25: newDataPoints = 50; break;
                                    case 50: newDataPoints = 100; break;
                                    case 100: newDataPoints = 200; break;
                                    case 200: newDataPoints = 300; break;
                                    case 300: newDataPoints = 500; break;
                                    case 500: newDataPoints = 1000; break;
                                    case 1000: newDataPoints = 2000; break;
                                    case 2000: newDataPoints = 3000; break;
                                    case 3000: newDataPoints = 3000; break;
                                }
                            }
                            config.MetricGraphs[c.GraphName].IncludedMetrics.DataPoints = newDataPoints;
                            c.RefreshGraph(true);
                            this.Cursor = Cursors.Arrow;
                        }
                    }
                    break;


                case Keys.PageDown:
                    for (int x = 0; x < pnlContent.Controls.Count; x++)
                    {
                        MetricGraph c = (MetricGraph)pnlContent.Controls[x];
                        if (config.MetricGraphs[c.GraphName].IsSelected)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            int newDataPoints = 100;
                            if (e.Control)
                            {

                            }
                            else
                            {
                                switch (config.MetricGraphs[c.GraphName].IncludedMetrics.DataPoints)
                                {
                                    case 10: newDataPoints = 10; break;
                                    case 25: newDataPoints = 10; break;
                                    case 50: newDataPoints = 25; break;
                                    case 100: newDataPoints = 50; break;
                                    case 200: newDataPoints = 100; break;
                                    case 300: newDataPoints = 200; break;
                                    case 500: newDataPoints = 300; break;
                                    case 1000: newDataPoints = 500; break;
                                    case 2000: newDataPoints = 1000; break;
                                    case 3000: newDataPoints = 2000; break;
                                }
                            }
                            config.MetricGraphs[c.GraphName].IncludedMetrics.DataPoints = newDataPoints;
                            c.RefreshGraph(true);
                            this.Cursor = Cursors.Arrow;
                        }
                    }
                    break;

                case Keys.A:
                    for (int x = 0; x < pnlContent.Controls.Count; x++)
                    {
                        MetricGraph c = (MetricGraph)pnlContent.Controls[x];
                        if (config.MetricGraphs[c.GraphName].IsSelected)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            c.RefreshGraph(true);
                            this.Cursor = Cursors.Arrow;
                        }
                    }
                    break;

                case Keys.S:
                    int selectedIndex = -1;
                    int graphCount = pnlContent.Controls.Count;

                    for (int x = 0; x < config.MetricGraphs.Count; x++)
                    {
                        if (config.MetricGraphs.Values[x].IsSelected)
                            selectedIndex = x;
                    }

                    if (e.Control)
                    {
                        if (selectedIndex == -1)
                            selectedIndex = graphCount - 1;
                        else
                        {
                            if (selectedIndex == 0)
                                selectedIndex = graphCount - 1;
                            else
                                selectedIndex--;
                        }
                    }
                    else
                    {
                        if (selectedIndex == -1)
                            selectedIndex++;
                        else
                        {
                            if (selectedIndex == graphCount - 1)
                                selectedIndex = 0;
                            else
                                selectedIndex++;
                        }
                    }

                    for (int x = 0; x < config.MetricGraphs.Count; x++) 
                    {
                        if (config.MetricGraphs.Values[x].IsSelected)
                        {
                            config.MetricGraphs.Values[x].IsSelected = false;
                            MetricGraph metricGraph = (MetricGraph) pnlContent.Controls[config.MetricGraphs.Keys[x]];
                            metricGraph.SetSelectionAndActivation();
                        }
                        if (x == selectedIndex)
                        {
                            config.MetricGraphs.Values[x].IsSelected = true;
                            MetricGraph metricGraph = (MetricGraph)pnlContent.Controls[config.MetricGraphs.Keys[x]];
                            metricGraph.SetSelectionAndActivation();
                        }
                    }
                    break;
            }
        }

        private void mnuReportsAvailableMetrics_Click(object sender, EventArgs e)
        {
            frmMetricReports fMetricReports = new frmMetricReports(dataObjects);
            fMetricReports.ShowDialog();
        }

        private void mnuGraphs_DropDownOpening(object sender, EventArgs e)
        {
            if(Clipboard.ContainsData("GraphConfig"))
                mnuOptionsPasteGraphFromClipboard.Visible = true;
            else
                mnuOptionsPasteGraphFromClipboard.Visible = false;
        }

        private void mnuOptionsAutoArrange_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            int option = Convert.ToInt32(item.Tag);
            int graphCount = config.MetricGraphs.Count;

            int rows = 0;
            int cols = 0;

            switch (graphCount)
            {

                case 1:
                    MetricGraph graph = (MetricGraph)pnlContent.Controls[config.MetricGraphs.Values[0].GraphName];
                    switch (option)
                    {
                        case 1:
                            graph.FrontAndCenter();
                            graph.RefreshGraph(false);
                            break;
                        case 2:
                            graph.MaximizeGraph();
                            graph.RefreshGraph(false);
                            break;
                    }
                    break;

                case 2:
                case 3:
                    switch (option)
                    {
                        case 1:
                            rows = graphCount;
                            cols = 1;
                            break;
                        case 2:
                            rows = 1;
                            cols = graphCount;
                            break;
                    }
                    break;

                case 4:
                    switch (option)
                    {
                        case 1:
                            rows = graphCount;
                            cols = 1;
                            break;
                        case 2:
                            rows = 2;
                            cols = 2;
                            break;
                    }
                    break;

                case 5:
                    switch (option)
                    {
                        case 1:
                            rows = graphCount;
                            cols = 1;
                            break;
                        case 2:
                            rows = 2;
                            cols = 3;
                            break;
                        case 3:
                            rows = 3;
                            cols = 2;
                            break;
                    }
                    break;

                case 6:
                    switch (option)
                    {
                        case 1:
                            rows = 2;
                            cols = 3;
                            break;
                        case 2:
                            rows = 3;
                            cols = 2;
                            break;
                    }
                    break;
 

                case 7:
                case 8:
                    switch (option)
                    {
                        case 1:
                            rows = 3;
                            cols = 3;
                            break;
                        case 2:
                            rows = 4;
                            cols = 2;
                            break;
                    }
                    break;

                case 9:
                    rows = 3;
                    cols = 3;
                    break;

                case 10:
                case 11:
                case 12:
                    switch (option)
                    {
                        case 1:
                            rows = 3;
                            cols = 4;
                            break;
                        case 2:
                            rows = 4;
                            cols = 3;
                            break;
                    }
                    break;

                case 13:
                case 14:
                case 15:
                    rows = 5;
                    cols = 3;
                    break;

                default:
                    break;
            }

            if(graphCount < 2 || graphCount > 15)
                return;

            int graphNumber = 0;
            int graphWidth = (pnlContent.Width / cols) - (cols + 1);
            int graphHeight = (pnlContent.Height / rows) - rows;

            Point[] points = new Point[rows * cols];
            Size[] sizes = new Size[rows * cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    graphNumber = r * cols + c;
                    points[graphNumber].X = 1 + (c * graphWidth) + 1 * c;
                    points[graphNumber].Y = 1 + (r * graphHeight) + 1 * r;
                    sizes[graphNumber].Width = graphWidth;
                    sizes[graphNumber].Height = graphHeight;
                }
            }

            int i = 0;

            foreach (KeyValuePair<string, MetricGraphConfiguration> kvpGraphConfig in config.MetricGraphs)
            {
                kvpGraphConfig.Value.GraphLocation = points[i];
                kvpGraphConfig.Value.GraphSize = sizes[i];

                MetricGraph graph = (MetricGraph)pnlContent.Controls[kvpGraphConfig.Value.GraphName];
                graph.Arrange();
                graph.RefreshGraph(false);

                i++;
            }

        }

        private void mnuOptionsAutoArrange_DropDownOpening(object sender, EventArgs e)
        {
            int graphCount = config.MetricGraphs.Count;


            mnuOptionsAutoArrange.Visible = true;
            switch (graphCount)
            {
                case 1:
                    mnuOptionsAutoArrangeOption1.Text = "Full Screen";
                    mnuOptionsAutoArrangeOption1.Visible = true;
                    mnuOptionsAutoArrangeOption2.Text = "Centered";
                    mnuOptionsAutoArrangeOption2.Visible = true;   
                    mnuOptionsAutoArrangeOption3.Visible = false;
                    break;

                case 2:
                case 3:
                    mnuOptionsAutoArrangeOption1.Text = "Tile Horizontal";
                    mnuOptionsAutoArrangeOption1.Visible = true;
                    mnuOptionsAutoArrangeOption2.Text = "Tile Vertical";
                    mnuOptionsAutoArrangeOption2.Visible = true;
                    mnuOptionsAutoArrangeOption3.Visible = false;
                    break;

                case 4:
                    mnuOptionsAutoArrangeOption1.Text = "Tile Horizontal";
                    mnuOptionsAutoArrangeOption1.Visible = true;
                    mnuOptionsAutoArrangeOption2.Text = "2 rows of 2";
                    mnuOptionsAutoArrangeOption2.Visible = true;
                    mnuOptionsAutoArrangeOption3.Visible = false;
                    break;

                case 5:
                    mnuOptionsAutoArrangeOption1.Text = "Tile Horizontal";
                    mnuOptionsAutoArrangeOption1.Visible = true;
                    mnuOptionsAutoArrangeOption2.Text = "2 rows of 3";
                    mnuOptionsAutoArrangeOption2.Visible = true;
                    mnuOptionsAutoArrangeOption3.Text = "3 rows of 2";
                    mnuOptionsAutoArrangeOption3.Visible = true;
                    break;

                case 6:
                    mnuOptionsAutoArrangeOption1.Text = "2 rows of 3";
                    mnuOptionsAutoArrangeOption1.Visible = true;
                    mnuOptionsAutoArrangeOption2.Text = "3 rows of 2";
                    mnuOptionsAutoArrangeOption2.Visible = true;
                    mnuOptionsAutoArrangeOption3.Visible = false;
                    break;

                case 7:
                case 8:
                    mnuOptionsAutoArrangeOption1.Text = "3 rows of 3";
                    mnuOptionsAutoArrangeOption1.Visible = true;
                    mnuOptionsAutoArrangeOption2.Text = "4 rows of 2";
                    mnuOptionsAutoArrangeOption2.Visible = true;
                    mnuOptionsAutoArrangeOption3.Visible = false;
                    break;

                case 9:
                    mnuOptionsAutoArrangeOption1.Text = "3 rows of 3";
                    mnuOptionsAutoArrangeOption1.Visible = true;
                    mnuOptionsAutoArrangeOption2.Visible = false;
                    mnuOptionsAutoArrangeOption3.Visible = false;
                    break;

                case 10:
                case 11:
                case 12:
                    mnuOptionsAutoArrangeOption1.Text = "3 rows of 4";
                    mnuOptionsAutoArrangeOption1.Visible = true;
                    mnuOptionsAutoArrangeOption2.Text = "4 rows of 3";
                    mnuOptionsAutoArrangeOption2.Visible = true;
                    mnuOptionsAutoArrangeOption3.Visible = false;
                    break;

                case 13:
                case 14:
                case 15:
                    mnuOptionsAutoArrangeOption1.Text = "5 rows of 3";
                    mnuOptionsAutoArrangeOption1.Visible = true;
                    mnuOptionsAutoArrangeOption2.Visible = false;
                    mnuOptionsAutoArrangeOption3.Visible = false;
                    break;

                default:
                    mnuOptionsAutoArrange.Visible = false;
                    break;
            }



        }

        private void mnuOptionsReOrder_Click(object sender, EventArgs e)
        {
            bool IsReOrdered = false;

            frmReOrderGraphs fReOrderGraphs = new frmReOrderGraphs(config);
            if (fReOrderGraphs.ShowDialog() == DialogResult.OK)
            {
                for(int i = 0; i < config.MetricGraphs.Count; i++)
                {
                    string configGraphName = config.MetricGraphs.Keys[i];
                    MetricGraph controlMetricGraph = (MetricGraph) pnlContent.Controls[i];
                    if(configGraphName != controlMetricGraph.GraphName)
                    {
                        IsReOrdered = true;
                        controlMetricGraph.Name = configGraphName;
                        controlMetricGraph.GraphName = configGraphName;
                        controlMetricGraph.MetricGraphConfig = config.MetricGraphs.Values[i];
                        controlMetricGraph.RefreshGraph(true);
                    }
                }

                if (IsReOrdered)
                    MessageBox.Show("The sequence of the graphs in the configuration has been updated.\r\n\r\n" +
                        "Use the 'Auto Arrange' option from the 'Graphs' Menu\r\n" +
                        "to change the layout of the graphs on the screen", "Re-Ordering Complete", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);


            }
        }

        private void pnlContent_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < pnlContent.Controls.Count; i++)
            {
                MetricGraph metricGraph = (MetricGraph)pnlContent.Controls[i];
                sb.Append("Control-" + i.ToString() + " Name: " + pnlContent.Controls[i].Name +
                    " GraphName:" + metricGraph.GraphName + "\r\n");
            }

            sb.Append("\r\n");

            for (int i = 0; i < config.MetricGraphs.Count; i++)
            {
                sb.Append("Config-" + i.ToString() + " GraphName: " + config.MetricGraphs.Values[i].GraphName + "\r\n");

            }

            MessageBox.Show(sb.ToString(), "Diagnostics");

        }

        private void SaveImageToFile(string graphName, Image img)
        {
            dlgSaveFile.Title = "Save graph image to file...";
            dlgSaveFile.InitialDirectory = @"c:\Program Files\MetricView1.0";
            dlgSaveFile.Filter = @"JPG Image File (*.jpg)|*.jpg|BMP Image File (*.bmp)|*.bmp";
            dlgSaveFile.FileName = String.Empty;
            if (dlgSaveFile.ShowDialog() == DialogResult.OK)
            {
                string fileName = dlgSaveFile.FileName;
                if (dlgSaveFile.FilterIndex == 2)
                {
                    if (!fileName.Contains(".bmp"))
                        fileName = fileName + ".bmp";
                    img.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                }
                else
                {
                    if (!fileName.Contains(".jpg"))
                        fileName = fileName + ".jpg";
                    img.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
        }


        private void SaveGraphToFile(string graphName)
        {
            dlgSaveFile.Title = "Save graph to file...";
            dlgSaveFile.InitialDirectory = @"c:\Program Files\MetricView1.0";
            dlgSaveFile.Filter = @"MetricView Graph (*.mvg)|*.mvg";
            dlgSaveFile.FileName = GeneralUtility.StripSeqFromName(graphName) + ".mvg";
            if (dlgSaveFile.ShowDialog() == DialogResult.OK)
            {
                string fileName = dlgSaveFile.FileName;
                FileStream stream = new FileStream(fileName, FileMode.Create);
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, config.MetricGraphs[graphName]);
                stream.Close();
            }
        }

        private void mnuOptionsImportGraphFromFile_Click(object sender, EventArgs e)
        {
            dlgOpenFile.Title = "Save graph to file...";
            dlgOpenFile.InitialDirectory = @"c:\Program Files\MetricView1.0";
            dlgOpenFile.Filter = @"MetricView Graph (*.mvg)|*.mvg";

            if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                string fileName = dlgOpenFile.FileName;
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                MetricGraphConfiguration metricGraphConfig = (MetricGraphConfiguration)formatter.Deserialize(stream);
                stream.Close();

                string graphName = GeneralUtility.StripSeqFromName(metricGraphConfig.GraphName);
                int graphNumber = config.MetricGraphs.Count;
                graphName = "~[" + graphNumber.ToString("0000") + "]" + graphName;
                metricGraphConfig.GraphName = graphName;

                config.MetricGraphs.Add(metricGraphConfig.GraphName, metricGraphConfig);

                MetricGraph graph = new MetricGraph(this.dataObjects);
                graph.GraphValue += this.GraphValue;
                graph.GraphName = metricGraphConfig.GraphName;
                graph.Name = metricGraphConfig.GraphName;
                graph.MetricGraphConfig = metricGraphConfig;
                graph.MenuAction += this.GraphMenuAction;
                graph.SaveImageToFile += this.SaveImageToFile;
                graph.UpdateConfigLocAndSize += this.UpdateConfigLocAndSize;
                pnlContent.Controls.Add(graph);
                graph.FrontAndCenter();
                graph.ResizeGraph(true);
                this.Cursor = Cursors.Arrow;
            }
        }



        private void mnuOptionsPasteGraphFromClipboard_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            MetricGraphConfiguration metricGraphConfig = (MetricGraphConfiguration)Clipboard.GetData("GraphConfig");
            if (config.MetricGraphs.ContainsKey(metricGraphConfig.GraphName))
                metricGraphConfig.GraphName += ("[" + DateTime.Now.ToString("HHMMss") + "]");

            string graphName = GeneralUtility.StripSeqFromName(metricGraphConfig.GraphName);
            int graphNumber = config.MetricGraphs.Count;
            graphName = "~[" + graphNumber.ToString("0000") + "]" + graphName;
            metricGraphConfig.GraphName = graphName;
            config.MetricGraphs.Add(metricGraphConfig.GraphName, metricGraphConfig);

            MetricGraph graph = new MetricGraph(this.dataObjects);
            graph.GraphName = metricGraphConfig.GraphName;
            graph.Name = metricGraphConfig.GraphName;
            graph.MetricGraphConfig = metricGraphConfig;
            graph.MenuAction += this.GraphMenuAction;
            graph.SaveImageToFile += this.SaveImageToFile;
            graph.GraphValue += this.GraphValue;
            graph.UpdateConfigLocAndSize += this.UpdateConfigLocAndSize;
            pnlContent.Controls.Add(graph);
            graph.FrontAndCenter();
            graph.ResizeGraph(true);
            this.Cursor = Cursors.Arrow;
        }

        private void timerControl_Tick(object sender, EventArgs e)
        {
            CheckControl();
        }

        private void CheckControl()
        {
            if(!File.Exists(@"c:\Program Files\MetricView\MVControl.txt"))
                return;

            int thisProcessID = Process.GetCurrentProcess().Id;
            lblStatus.Text = "Process ID = " + thisProcessID.ToString();

            StreamReader sr = new StreamReader(@"c:\Program Files\MetricView\MVControl.txt");

            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine();
                string[] s = line.Split(controlDelimiter);
                if (s.Length == 2)
                {
                    int controlProcessID = Int32.Parse(s[0]);
                    if(controlProcessID == thisProcessID)
                    {
                        if (s[1] == LastAction)
                        {
                            sr.Close();
                            return;
                        }

                        switch (s[1])
                        {
                            case "CLOSE":
                                timerControl.Enabled = false;
                                DeselectAllGraphs();
                                if (configFileName.CompareTo(String.Empty) != 0)
                                {
                                    ConfigHelper.PutConfiguration(config, configFileName);
                                }
                                IsFileSavedOnExit = true;
                                this.Close();
                                break;

                            case "ACTIVATE":
                                ActivateAll();
                                break;

                            case "DEACTIVATE":
                                DeactivateAll();
                                break;
                        }
                        LastAction = s[1];
                    }
                }
            }
            sr.Close();
        }
    }
}