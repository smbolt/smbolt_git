using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS
{
  public class DiagnosticsManager
  {
    private DiagnosticsMode _runUnitDiagnosticsMode;
    private DiagnosticsMode _majorProcessDiagnosticsMode;
    private DiagnosticsMode _minorProcessDiagnosticsMode;

    public DiagnosticsMode DiagnosticsMode
    {
      get {
        return Get_DiagnosticsMode();
      }
    }

    public DiagnosticsManager()
    {
      _runUnitDiagnosticsMode = DiagnosticsMode.None;
      _majorProcessDiagnosticsMode = DiagnosticsMode.None;
      _minorProcessDiagnosticsMode = DiagnosticsMode.None;
    }

    private DiagnosticsMode Get_DiagnosticsMode()
    {
      int diagnosticsMode = Convert.ToInt32(DiagnosticsMode.None);

      int runUnitValue = Convert.ToInt32(_runUnitDiagnosticsMode);
      int majorProcessValue = Convert.ToInt32(_majorProcessDiagnosticsMode);
      int minorProcessValue = Convert.ToInt32(_minorProcessDiagnosticsMode);

      if (runUnitValue > diagnosticsMode)
        diagnosticsMode = runUnitValue;

      if (majorProcessValue > diagnosticsMode)
        diagnosticsMode = majorProcessValue;

      if (minorProcessValue > diagnosticsMode)
        diagnosticsMode = minorProcessValue;

      DiagnosticsMode value = (DiagnosticsMode)diagnosticsMode;

      return value;
    }

    public void SetRunUnitDiagnosticsMode(DiagnosticsMode diagnosticsMode)
    {
      _runUnitDiagnosticsMode = diagnosticsMode;
    }

    public void SetMajorProcessDiagnosticsMode(DiagnosticsMode diagnosticsMode)
    {
      _majorProcessDiagnosticsMode = diagnosticsMode;
    }

    public void SetMinorProcessDiagnosticsMode(DiagnosticsMode diagnosticsMode)
    {
      _minorProcessDiagnosticsMode = diagnosticsMode;
    }

  }
}
