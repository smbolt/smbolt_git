using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public enum NodeType
  {
    NotSet,
    Elemental,
    Group
  }

  public enum DxObjectType
  {
    NotSet,
    DxCell,
    DxCellSet,
    DxColumn,
    DxColumnSet,
    DxRowSet
  }

  public enum MatchStatus
  {
    NotCompared,
    MatchAcrossEnvironments,
    MismatchAcrossEnvironments,
    MissingInThisEnvironment,
    MissingInOppositeEnvrionment,
    NotSet,
  }
}
