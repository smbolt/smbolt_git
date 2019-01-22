using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.Terminal.Screen;
using Org.GS;

namespace Org.Terminal.BMS
{
  public class BmsStatementSet : SortedList<int, BmsStatement>
  {
    public BmsMapErrorSet BmsMapErrorSet { get; set; }
    private int NextStatementNumber { get { return Get_NextStatementNumber(); } }

    public void Load(string bmsString, BmsMapErrorSet bmsMapErrorSet)
    {
      try
      {
        this.BmsMapErrorSet = bmsMapErrorSet;

        this.Clear();        

        string[] rawLines = bmsString.Replace("\r", String.Empty).Split(Constants.NewLineDelimiter, StringSplitOptions.RemoveEmptyEntries);

        BmsStatement stmt = null;
        int lineNumber = 0; 

        foreach (var line in rawLines)
        {
          lineNumber++;
          var bmsLine = new BmsLine();
          bmsLine.LineNumber = lineNumber;

          string lineWork = line;

          if (lineWork.StartsWith("*"))
          {
            bmsLine.LineText = lineWork;
            stmt = new BmsStatement();
            stmt.BmsStatementType = BmsStatementType.Comment;
            stmt.BmsLineSet.Add(this.NextStatementNumber, bmsLine);
            this.Add(this.Count + 1, stmt);
            stmt = new BmsStatement();
            continue;
          }

          bool lineIsContinued = lineWork.Length > 71 && lineWork[71].ToString().IsNotBlank();

          bmsLine.LineText = lineWork;

          stmt.BmsLineSet.Add(stmt.BmsLineSet.Count + 1, bmsLine);

          if (!lineIsContinued)
          {
            this.Add(this.Count + 1, stmt);
            stmt = new BmsStatement();
            continue;
          }
        }

        if (stmt.BmsLineSet.Count > 0)
          this.Add(this.Count + 1, stmt);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a BmsStatementSet from a BMS map string.", ex); 
      }
    }

    public void CompileStatements()
    {
      try
      {
        IdentifyStatementTypes();

        foreach (var bmsStmt in this.Values)
        {
          if (bmsStmt.BmsStatementType == BmsStatementType.Comment)
            continue;

          bmsStmt.ExtractParentheticals();
          bmsStmt.ExtractParameters();
          
          switch (bmsStmt.BmsStatementType)
          {
            case BmsStatementType.PRINT:
              bmsStmt.Compile_PRINT();
              break;

            case BmsStatementType.DFHMSD:
              bmsStmt.Compile_DFHMSD();
              break;

            case BmsStatementType.DFHMDI:
              bmsStmt.Compile_DFHMDI();
              break;

            case BmsStatementType.DFHMDF:
              bmsStmt.Compile_DFHMDF();
              break;

            default:
              bmsStmt.Compile_Other();
              break;
          }
        }


        // validate the statements as a set

        int stmtCount = 0;
        List<string> mapNames = new List<string>();
        List<string> fieldNames = new List<string>();
        string mapName = String.Empty;

        foreach (var bmsStmt in this.Values)
        {
          if (bmsStmt.BmsStatementType == BmsStatementType.Comment || bmsStmt.BmsStatementType == BmsStatementType.PRINT)
            continue;

          if (stmtCount == 0)
          {
            if (bmsStmt.BmsStatementType != BmsStatementType.DFHMSD)
            {
              // add an error and discontinue compile
              // first statement must be DFHMSD

            }
          }

          stmtCount++;

          switch (bmsStmt.BmsStatementType)
          {
            case BmsStatementType.DFHMDI:
              var dfhmdi = bmsStmt.Bms_BASE as Bms_DFHMDI;
              mapName = dfhmdi.Name;
              if (mapNames.Contains(mapName))
              {
                // add error
                // continue
              }
              else
              {
                mapNames.Add(mapName);
              }
              break;

            case BmsStatementType.DFHMDF:
              var dfhmdf = bmsStmt.Bms_BASE as Bms_DFHMDF;
              if (dfhmdf.Name.IsNotBlank())
              {
                string fieldName = mapName + "_" + dfhmdf.Name;
                if (fieldNames.Contains(fieldName))
                {
                  // add error about duplicate field name
                  // continue
                }
              }
              break;
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to compile the BmsStatementSet.", ex); 
      }
    }

    private void IdentifyStatementTypes()
    {
      try
      {
        foreach (var bmsStmt in this.Values)
        {
          if (bmsStmt.BmsStatementType != BmsStatementType.Comment)
          {
            var firstLine = bmsStmt.BmsLineSet.Values.FirstOrDefault();

            if (firstLine == null)
            {
              bmsStmt.BmsStatementType = BmsStatementType.Unidentified;
              // add error to collection
              continue;
            }

            string firstLineText = firstLine.LineText;
            string[] tokens = firstLineText.Trim().Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length < 1)
            {
              bmsStmt.BmsStatementType = BmsStatementType.Unidentified;
              // add error to collection
              continue;
            }

            string firstToken = tokens[0];

            if (firstToken.ToUpper() == "PRINT")
            {
              bmsStmt.BmsStatementType = BmsStatementType.PRINT;
              continue;
            }

            if (firstToken.ToUpper() == "END")
            {
              bmsStmt.BmsStatementType = BmsStatementType.PRINT;
              continue;
            }

            // initialize to Unidentified type
            bmsStmt.BmsStatementType = BmsStatementType.Unidentified;

            // must have
            if (tokens.Length < 2)
            {
              // error ?
              // continue?
            }

            string secondToken = tokens[1];

            bool isFirstTokenBmsMacro = false;

            if (firstToken.IsBmsMacro())
            {
              bmsStmt.BmsStatementType = firstToken.ToBmsStatementType();
              isFirstTokenBmsMacro = true;
            }

            if (!isFirstTokenBmsMacro && secondToken.IsBmsMacro())
              bmsStmt.BmsStatementType = secondToken.ToBmsStatementType();

            if (!isFirstTokenBmsMacro)
              bmsStmt.FieldName = firstToken;

            if (bmsStmt.BmsStatementType == BmsStatementType.Unidentified)
            {
              // add error
              continue;
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to identify BMS statement types.", ex); 
      }
    }

    private int Get_NextStatementNumber()
    {
      return this.Count + 1;
    }
  }
}
