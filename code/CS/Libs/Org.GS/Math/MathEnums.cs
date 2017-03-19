using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Org.GS.Math
{

  [ObfuscationAttribute(Exclude = true)]
  public enum EquationResultType
  {
    Integer,
    Float
  }

  [ObfuscationAttribute(Exclude = true)]
  public enum SubExpressionType
  {
    NotSet,
    FullEquation,
    Parenthetical,
    HighOrder,
    LowOrder
  }

  [ObfuscationAttribute(Exclude = true)]
  public enum MathOperation
  {
    NotSet,
    Add,
    Subtract,
    Multiply,
    Divide,
    Exponent
  }
}
