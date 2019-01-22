using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using Org.GS;

namespace Org.RPT
{
  [XMap(XType = XType.Element, CollectionElements = "ReportObject", SequenceDuplicates = true, KeyName = "Name")]
  public class ReportObjectSet : Dictionary<string, ReportObject>
  {
    [XMap(IsKey = true)]
    public string Name {
      get;
      set;
    }


    public ReportObject Parent {
      get;
      set;
    }
    public ReportDef RDef {
      get;
      set;
    }
    public int Level {
      get;
      set;
    }

    public ReportObjectSet()
    {
      this.Name = String.Empty;
      this.Parent = null;
      this.RDef = null;
      this.Level = 0;
    }

    public void LoadData(ReportData _reportData)
    {
      foreach (ReportObject ro in this.Values)
      {
        if (ro.ReportObjectType == ReportObjectType.ReportText)
        {
          if (ro.ReportText != null)
          {
            string dataSource = ro.ReportText.DataSource;
            if (dataSource.IsNotBlank())
            {
              switch (dataSource)
              {
                case "$LONGDATE1$":
                  ro.ReportText.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
                  break;

                default:
                  if (_reportData.ContainsKey(dataSource))
                    ro.ReportText.Text = _reportData[dataSource];
                  break;

              }

            }
          }
        }

        if (ro.ReportObjectSet != null)
          ro.ReportObjectSet.LoadData(_reportData);
      }
    }

    public void SetReferences()
    {
      foreach (ReportObject ro in this.Values)
      {
        ro.Parent = this.Parent;
        ro.Level = this.Level;
        ro.RDef = this.RDef;
        ro.SetReferences();
      }
    }
  }
}
