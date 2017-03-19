using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Org.GS;

namespace Org.RPT
{
  [XMap(XType = XType.Element)]
  public class ReportDef : ReportObject
  {
    [XMap(XType = XType.Element, CollectionElements = "ReportObject", WrapperElement = "ReportObjectSet")]
    public ReportObjectSet ReportObjectSet { get; set; }

    [XMap(IsKey = true)]
    public string Name { get; set; }

    [XMap(DefaultValue = "850")]
    public float PageWidth { get; set; }

    [XMap(DefaultValue = "1100")]
    public float PageHeight { get; set; }

    [XMap(DefaultValue = "500")]
    public float TopMargin { get; set; }

    [XMap(DefaultValue = "500")]
    public float BottomMargin { get; set; }

    [XMap(DefaultValue = "500")]
    public float LeftMargin { get; set; }

    [XMap(DefaultValue = "500")]
    public float RightMargin { get; set; }

    [XMap(DefaultValue = "False")]

    public bool DiagMode { get; set; }

    public float NextY { get; set; }
    public int NumberOfPages { get; set; }

    public ReportObjectSet PerPage { get; set; }
    public ReportObjectSet PrintSet { get; set; }

    public StringBuilder Trace { get; set; }
    public Font DiagFont { get; set; }

    public ReportDef()
    {
      this.ReportObjectType = ReportObjectType.ReportDef;
      this.Name = "ReportRoot";
      this.PageWidth = 850F;
      this.PageWidth = 1100F;
      this.TopMargin = 50.0F;
      this.BottomMargin = 50.0F;
      this.LeftMargin = 50.0F;
      this.RightMargin = 50.0F;
      this.DiagMode = false;
      this.NextY = 0.0F;
      this.NumberOfPages = 1;
      this.Trace = new StringBuilder();
      this.PerPage = new ReportObjectSet();
      this.PrintSet = new ReportObjectSet();
      this.DiagFont = new Font("Lucida Console", 7.0F); 
    }

    public void LayoutReport(Graphics gr, ReportData _reportData, float scale)
    {
      this.NextY = this.TopMargin;
      this.CurrY = this.TopMargin;
      this.LeftEdge = this.LeftMargin;
      this.CurrX = this.LeftMargin;

      try
      {
        this.ReportObjectSet.LoadData(_reportData);

        foreach (ReportObject rx in this.ReportObjectSet.Values)
        {
          rx.LayoutObject(gr, scale);
        }
      }
      catch (Exception ex)
      {
        string message = ex.Message;
      }
    }

    public void PrintReport(Graphics gr, ReportData _reportData, float scale)
    {
      try
      {
        // Draw the per page objects
        foreach (ReportObject rx in this.PerPage.Values)
        {
          rx.DrawObject(gr, scale);
        }

        // Draw the laid out objects
        foreach (ReportObject rx in this.PrintSet.Values)
        {
          rx.DrawObject(gr, scale);
        }
      }
      catch (Exception ex)
      {
        string message = ex.Message;
      }
    }


    public void SetReferences()
    {
      foreach (ReportObject ro in this.ReportObjectSet.Values)
      {
        ro.Parent = this;
        ro.Level = 1;
        ro.RDef = this;
        ro.SetReferences();
      }
    }

    public void DumpTrace()
    {
      string fullPath = g.LogPath + @"\ReportTrace.txt";
      if (Directory.Exists(Path.GetDirectoryName(g.LogPath)))
      {
          string trace = this.Trace.ToString();
          File.WriteAllText(fullPath, trace);
      }
    }
  }
}
