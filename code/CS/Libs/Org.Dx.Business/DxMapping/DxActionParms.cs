using Org.GS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class DxActionParms
  {
    public DxActionType DxActionType { get; set; }
    public string ParmString { get; private set; }
    public string FilterName { get; private set; }
    public List<string> RegionNames { get; private set; }

    public DxActionParms(DxActionType dxActionType, string parmString)
    {
      this.DxActionType = dxActionType;
      this.ParmString = parmString;

      ParseParms();
    }

    private void ParseParms()
    {
      try
      {
        switch (this.DxActionType)
        {
          case DxActionType.FilterSheetsAction:
            this.FilterName = this.ParmString.Trim();
            break;

          case DxActionType.CreateRegionsAction:
            this.RegionNames = this.ParmString.Trim().Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries).ToList();
            break;

          case DxActionType.DataMappingAction:
              break;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to parse the DxAction ParmString '" + 
                            this.ParmString + "' for DxActionType '" + this.DxActionType.ToString() + "'.", ex);
      }
    }
  }
}
