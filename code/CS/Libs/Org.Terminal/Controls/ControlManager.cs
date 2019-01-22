using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Terminal.Controls
{
  public class ControlManager
  {
    private MFContainer _mfContainer;

    private MFBase _activeControl;
    public MFBase ActiveControl {
      get {
        return Get_ActiveControl();
      }
    }

    private Dictionary<string, MFBase> _controls;
    private SortedList<int, MFBase> _orderedControls;
    private SortedList<string, MFBase> _vFlexControls;
    public ControlLine VFlexLine {
      get {
        return Get_VFlexLine();
      }
    }

    public bool ContainsVFlexControl {
      get {
        return _vFlexControls == null ? false : _vFlexControls.Count > 0;
      }
    }

    public ControlLineSet ControlLineSet {
      get;
      private set;
    }


    public ControlManager(MFContainer mfContainer)
    {
      _mfContainer = mfContainer;
      _controls = new Dictionary<string, MFBase>();
      _orderedControls = new SortedList<int, MFBase>();
      _vFlexControls = new SortedList<string, MFBase>();
      this.ControlLineSet = new ControlLineSet();
    }

    public void AddControl(MFBase c)
    {
      if (_controls == null)
        return;

      if (_controls.ContainsKey(c.Name))
        throw new Exception("The control named '" + c.Name + "' already exists in the control container.");

      _controls.Add(c.Name, c);

      if (c.TabStop)
      {
        if (_orderedControls.ContainsKey(c.TabIndex))
        {
          var existingControl = _orderedControls[c.TabIndex];
          throw new Exception("The control named '" + c.Name + "' has a duplicate TabIndex value of " + c.TabIndex.ToString() +
                              ", the already loaded control named '" + existingControl.Name + "' has the same TabIndex.");
        }

        _orderedControls.Add(c.TabIndex, c);
      }

      if (c.IsVFlexControl)
      {
        if (_vFlexControls.ContainsKey(c.Name))
        {
          var existingControl = _vFlexControls[c.Name];
          throw new Exception("The control named '" + c.Name + "' has the same name as a already existing control.");
        }
        _vFlexControls.Add(c.Name, c);
      }
    }

    private ControlLine Get_VFlexLine()
    {
      if (_vFlexControls == null || _vFlexControls.Count == 0)
        return null;

      ControlLine controlLine = null;

      if (_vFlexControls.Count == 1)
      {
        controlLine = new ControlLine();
        controlLine.Add(_vFlexControls.Values[0]);
        return controlLine;
      }

      controlLine = new ControlLine();

      foreach (var vFlexControl in _vFlexControls.Values)
      {
        if (controlLine.Count > 0 && vFlexControl.OrigLine != controlLine[0].OrigLine)
        {
          throw new Exception("VFLEX controls must only exist on one screen line.  Control " + vFlexControl.Name + " is on line " +
                              vFlexControl.OrigLine.ToString() + " and " + controlLine[0].Name + " is on line " +
                              controlLine[0].OrigLine.ToString() + ".");
        }

        controlLine.Add(vFlexControl);
      }

      if (controlLine.Count == 0)
        return null;

      return controlLine;
    }

    public void ClearControls()
    {
      if (this._controls != null)
        this._controls.Clear();

      if (this._orderedControls != null)
        this._orderedControls.Clear();
    }

    public MFBase GetNextControl(MFBase currControl, string controlType = "")
    {
      if (currControl == null)
        return null;

      int currTabIndex = currControl.TabIndex;

      // loop through tab ordered controls greater than current tab index
      for (int i = 0; i < _orderedControls.Count; i++)
      {
        if (_orderedControls.Keys[i] > currTabIndex)
        {
          switch (controlType)
          {
            case "@LC":
              if (_orderedControls.Values[i].Name.StartsWith("@LC"))
                return _orderedControls.Values[i];
              break;

            case "@TL":
              if (_orderedControls.Values[i].Name.StartsWith("@TL"))
                return _orderedControls.Values[i];
              break;

            default:
              return _orderedControls.Values[i];
          }
        }
      }

      // LATER, WE WILL WANT TO SCROLL ONE LINE DOWN ONCE WE REACH THE BOTTOM OF THE SCREEN
      // WHEN PROCESSING FROM LINE COMMAND OR TEXT LINE ELEMENTS (OR OTHER SCROLLABLE ORDERED ELEMENTS)

      // if next control is not found, loop back to top of tab ordered list
      // and find control with tab index less than current tab index

      for (int i = 0; i < _orderedControls.Count; i++)
      {
        if (_orderedControls.Keys[i] < currTabIndex)
        {
          switch (controlType)
          {
            case "@LC":
              if (_orderedControls.Values[i].Name.StartsWith("@LC"))
                return _orderedControls.Values[i];
              break;

            case "@TL":
              if (_orderedControls.Values[i].Name.StartsWith("@TL"))
                return _orderedControls.Values[i];
              break;

            default:
              return _orderedControls.Values[i];
          }
        }
      }

      return null;
    }

    public MFBase GetPrevControl(MFBase currControl, string controlType = "")
    {
      if (currControl == null)
        return null;

      int currTabIndex = currControl.TabIndex;
      int currIndex = -1;

      // locate the current position in the ordered controls
      for (int i = 0; i < _orderedControls.Count; i++)
      {
        if (_orderedControls.Keys[i] == currTabIndex)
        {
          currIndex = i;
          break;
        }
      }

      if (currIndex == -1)
        return null;

      // LATER WE'LL WANT TO SCROLL THE DOCUMENT UP IF WE CAN - UNTIL WE REACH THE TOP

      // search backward for the next control of the given type if specified
      if (currIndex > 0)
      {
        for (int i = currIndex - 1; i > -1; i--)
        {
          switch (controlType)
          {
            case "@LC":
              if (_orderedControls.Values[i].Name.StartsWith("@LC"))
                return _orderedControls.Values[i];
              break;

            case "@TL":
              if (_orderedControls.Values[i].Name.StartsWith("@TL"))
                return _orderedControls.Values[i];
              break;

            default:
              return _orderedControls.Values[i];
          }
        }
      }

      // if not found "above" the current index, then start at the bottom and search
      // upward until the next control (of the specified type if applicable) is found
      // limiting the search up from the bottom to items greater than the current index
      for (int i = _orderedControls.Count - 1; i > currIndex; i--)
      {
        switch (controlType)
        {
          case "@LC":
            if (_orderedControls.Values[i].Name.StartsWith("@LC"))
              return _orderedControls.Values[i];
            break;

          case "@TL":
            if (_orderedControls.Values[i].Name.StartsWith("@TL"))
              return _orderedControls.Values[i];
            break;

          default:
            return _orderedControls.Values[i];
        }
      }

      return null;
    }

    public void MoveLinesDown(int firstLineToMove, int numberOfLinesDown)
    {
      try
      {

        if (!this.ControlLineSet.Keys.Contains(firstLineToMove))
          throw new Exception("An exception occurred while attempting to move lines down to make room for additional VFLEX lines " +
                              "which are being inserted. The first line to move down which is line " + firstLineToMove.ToString() +
                              " is not incluced in the ControlLineSet.");

        var lineKeysToMove = new List<int>();
        var linesBeingMoved = new SortedList<int, ControlLine>();

        for (int i = 0; i < this.ControlLineSet.Count; i++)
        {
          if (this.ControlLineSet.Keys[i] >= firstLineToMove)
          {
            lineKeysToMove.Add(this.ControlLineSet.Keys[i]);
          }
        }

        foreach (int lineKeyToMove in lineKeysToMove)
        {
          int newKey = lineKeyToMove + numberOfLinesDown;
          var movingControlLine = this.ControlLineSet[lineKeyToMove];
          this.ControlLineSet.Remove(lineKeyToMove);
          linesBeingMoved.Add(newKey, movingControlLine);
        }

        foreach (var lineBeingMoved in linesBeingMoved)
        {
          int newLineNumber = lineBeingMoved.Key;
          var movingControlLine = lineBeingMoved.Value;
          foreach (TextBlock tb in movingControlLine)
          {
            tb.SetLineNumber(newLineNumber);
          }
          this.ControlLineSet.Add(newLineNumber, movingControlLine);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to move ControlLines down to make room for " +
                            "new ControlLines being created with new VFLEX-created controls.", ex);
      }
    }

    public void RemoveControl(MFBase c)
    {
      if (_controls == null)
        throw new Exception("The controls collection in the control container is null.");

      if (!_controls.ContainsKey(c.Name))
        throw new Exception("The control to be removed named '" + c.Name + "' does not exist in the control container.");

      _controls.Remove(c.Name);

      if (_orderedControls.ContainsKey(c.TabIndex))
        _orderedControls.Remove(c.TabIndex);

      if (_vFlexControls.ContainsKey(c.Name))
        _vFlexControls.Remove(c.Name);
    }

    public void ClearControlLineSet()
    {
      this.ControlLineSet = new ControlLineSet();
    }

    private MFBase Get_ActiveControl()
    {
      if (_activeControl == null)
        return null;

      return _activeControl;
    }

    public bool AdjustSize()
    {
      try
      {




        // if no changes have been made
        return false;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to adjust the control layout to the updated screen size.", ex);
      }
    }
  }
}
