using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.WSO;
using Org.WSO.Transactions;
using Org.GS.Configuration;
using Org.GS;

namespace Org.AX
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class AxAction
  {
    [XMap(IsKey = true)]
    public string Name { get; set; }

    [XMap(IsRequired = true)]
    public AxFunction AxFunction { get; set; }    

    [XMap(DefaultValue = "True")]
    public bool IsActive { get; set; }

    [XMap]
    public string Src { get; set; }

    [XMap]
    public string Tgt { get; set; }

    public AxActionClass AxActionClass { get { return Get_AxActionClass(); } }

    public bool IsDryRun { get; private set; }
    public string Report { get { return Get_Report(); } }

    public AxAction()
    {
      this.Name = String.Empty;
      this.AxFunction = AxFunction.NotSet;
      this.IsActive = true;
      this.IsDryRun = false;
    }

    public TaskResult Run(bool isDryRun)
    {
      try
      {
        this.IsDryRun = isDryRun;

        switch (this.AxActionClass)
        {
          case AxActionClass.FileSystemAction:
            return this.RunFileSystemAction();

          case AxActionClass.ServiceManagementAction:
            return this.RunServiceManagementAction();
        }

        throw new Exception("Functions for AxActionClass '" + this.AxActionClass.ToString() + " are not yet implemented.");
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to execute AxAction '" + this.Report + "'.", ex); 
      }
    }

    private TaskResult RunFileSystemAction()
    {
      try
      {
        using (var engine = new FSEngine(this.IsDryRun))
        {
          engine.CreateReport = true;

          switch (this.AxFunction)
          {
            case AxFunction.DeleteFiles:
              return engine.DeleteFilesInFolder(this.Tgt);

            case AxFunction.DeleteRecursive:
              return engine.DeleteRecursive(this.Tgt);
            
            case AxFunction.CopyFiles:
              return engine.CopyFiles(this.Src, this.Tgt);

          }
        }

        return new TaskResult("RunAxAction_" + this.AxFunction.ToString()).Failed("RunFileSystemAction is not yet implemented.");
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to run the AxAction '" + this.Report + "'.", ex);
      }
    }

    private TaskResult RunServiceManagementAction()
    {
      try
      {


        return new TaskResult("RunAxAction_" + this.AxFunction.ToString()).Failed("RunServiceManagementAction is not yet implemented.");
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to run the AxAction '" + this.Report + "'.", ex);
      }
    }


    private AxActionClass Get_AxActionClass()
    {
      switch (this.AxFunction)
      {
        case AxFunction.CopyFiles:
        case AxFunction.DeleteFiles:
        case AxFunction.DeleteRecursive:
          return AxActionClass.FileSystemAction;



      }


      throw new Exception("Cannot determine AxActionClass from AxFunction '" + this.AxFunction.ToString() + "'.");
    }

    private string Get_Report()
    {
      return "AxAction report not yet implemented."; 
    }

  }
}
