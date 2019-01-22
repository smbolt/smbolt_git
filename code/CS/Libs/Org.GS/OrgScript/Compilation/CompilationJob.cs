using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.OrgScript.Compilation
{
  // This class represents a compilation job.
  public class CompilationJob
  {
    public string RawCode { get; private set; }
    public CompilerConfig CompilerConfig { get; private set; }
    public Compiler Compiler { get; private set; }
    public SyntaxTree SyntaxTree { get; private set; }
    public string Report { get { return Get_Report(); } }


    public CompilationJob(string rawCode, CompilerConfig compilerConfig = null)
    {
      this.RawCode = rawCode;
      this.CompilerConfig = compilerConfig;
      if (compilerConfig == null)
        compilerConfig = GetDefaultCompilerConfig();

      this.Compiler = new Compiler(rawCode, compilerConfig);
      this.SyntaxTree = new SyntaxTree();
      
    }

    public void StepForward()
    {
      try
      {
        this.Compiler.RunNextStep();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to step forward in the compilation job debug session.", ex);
      }
    }


    private CompilerConfig GetDefaultCompilerConfig()
    {
      var compilerConfig = new CompilerConfig();
      compilerConfig.TargetRawLength = 500;

      return compilerConfig;
    }

    private string Get_Report()
    {
      return this.Compiler.Report;
    }
  }
}
