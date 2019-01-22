using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.OrgScript.Compilation
{
  public class Compiler
  {
    public CompilerConfig CompilerConfig { get; private set; }
    public CompilationPhase CompilationPhase { get; private set; }
    public Parser Parser { get; private set; }
    public CompilerNextStep CompilerNextStep { get; private set; }
    public string SyntaxNodeReport { get; set; }
    public string Report { get { return Get_Report(); } }

    public Compiler(string rawCode, CompilerConfig compilerConfig = null)
    {
      this.CompilationPhase = CompilationPhase.Loading;

      this.Parser = new Parser(rawCode, compilerConfig);
      this.CompilerConfig = compilerConfig;

      this.CompilationPhase = CompilationPhase.Loaded;
      this.CompilerNextStep = CompilerNextStep.ParseToSyntaxNodes;
    }

    public void RunNextStep()
    {
      try
      {
        switch (this.CompilerNextStep)
        {
          case CompilerNextStep.ParseToSyntaxNodes:
            this.Parser.RunNextStep();
            break;

        }

      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to run the compiler next step - next step is " + this.CompilerNextStep.ToString() + ".", ex);
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
      return "Compiler report not yet implemented.";
    }
  }
}
