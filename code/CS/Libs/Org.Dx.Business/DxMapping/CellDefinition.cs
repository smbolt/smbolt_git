using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{ 
  public class CellDefinition 
  {
    public int Column { get; private set; }
    public int Row { get; private set; }
    public int OffSet { get; private set; }
    public RelOp RelOp { get; private set; }    
    public string CompareValue { get; private set; }
    public bool IsCellLocated { get; private set; }

    public CellDefinition(string definition)
    {
      this.IsCellLocated = false;

      if (definition.IsBlank())
        throw new Exception("While constructing a Cell Definition, the definition string was blank or null.");
      
      try
      {
        string[] tokens = definition.SplitRelation();

        if (tokens.Length != 3)
          throw new Exception("The cell definition string is invalid + '" + definition + "'.");

        this.RelOp = tokens[1].ToRelOp();

        if (this.RelOp == RelOp.NotSet)
          throw new Exception("The relational operator is invalid '" + tokens[1] + "'.");

        if (tokens[0] == "*")
        {
          this.Column = -1;
        }
        else
        {
          if (tokens[0].IsNotInteger())
            throw new Exception("The column index '" + tokens[0] + "' of the relational definition is not an integer.");
          else
            this.Column = tokens[0].ToInt32();
        }

        this.CompareValue = tokens[2];

        string offsetText = this.CompareValue.GetTextBetweenBrackets();
        this.CompareValue = this.CompareValue.Split(Constants.OpenBracket, StringSplitOptions.RemoveEmptyEntries).First();

        if (this.CompareValue.IsBlank())
          throw new Exception("The relational definition compare value is blank.");

        if (offsetText.IsNotBlank() && offsetText.IsNotInteger())
          throw new Exception("The offset specified in the relational definition '" + offsetText + "' is not an integer.");

        if (offsetText.IsInteger())
          this.OffSet = offsetText.ToInt32();
        else
          this.OffSet = 0;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to process a CellDefinition.", ex);
      }
    }

    public void SetCell(DxRowSet rs, string offset = null)
    {
      if (rs == null  || rs.Rows.Count == 0)
        throw new Exception("The DxRowSet passed to this method was null or contained zero rows.");      
      
      try
      {
        foreach (var row in rs.Rows)
        {
          if (this.Column == -1)
          {
            foreach (var cell in row.Value.Cells)
            {
              if (cell.Value.IsCellEmpty)
                continue;

              switch (this.RelOp)
              {
                case RelOp.Contains:
                  if (cell.Value.ValueOrDefault.ToString().ToLower().Contains(this.CompareValue.ToLower()))
                  {
                    if (offset == "c")
                      this.Column = cell.Key + this.OffSet;
                    else
                      this.Column = cell.Key;

                    if (offset == "r")
                      this.Row = row.Key + this.OffSet;
                    else
                      this.Row = row.Key;

                    this.IsCellLocated = true;
                    break;
                  }
                  break;

                case RelOp.Equals:
                  if (cell.Value.ValueOrDefault.ToString().ToLower() == this.CompareValue.ToLower())
                  {
                    if (offset == "c")
                      this.Column = cell.Key + this.OffSet;
                    else
                      this.Column = cell.Key;

                    if (offset == "r")
                      this.Row = row.Key + this.OffSet;
                    else
                      this.Row = row.Key;

                    this.IsCellLocated = true;
                    break;
                  }
                  break;

                default:
                  throw new Exception("The Relational Operator '" + this.RelOp.ToString() + "' has not yet been implemented.");
              }
            }

            if (this.IsCellLocated)
              break;
          }
          else
          {
            if (this.Column < row.Value.Cells.Count)
            {
              var kvpCell = row.Value.Cells.ElementAt(this.Column);
              var cell = kvpCell.Value;

              if (!cell.IsCellEmpty)
              {
                if (cell.ValueOrDefault.ToString().ToLower().Contains(this.CompareValue.ToLower()))
                {
                  if (offset == "r")
                    this.Row = row.Key + this.OffSet;
                  else
                    this.Row = row.Key;

                  this.IsCellLocated = true;
                  break;
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to Set a Column or Row Definition.", ex);
      }
    }
  }
}
