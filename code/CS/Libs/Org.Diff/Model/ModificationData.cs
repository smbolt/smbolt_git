//using System.Text;
//using Org.GS;

//namespace Org.Diff.Model
//{
//  public class ModificationData
//  {
//    public int[] HashedPieces { get; set; }

//    public string RawData { get; }

//    public bool[] Modifications { get; set; }

//    public string[] Pieces { get; set; }

//    public ModificationData(string str)
//    {
//      RawData = str;
//    }

//    private string Get_Report()
//    {
//        var sb = new StringBuilder();

//        sb.Append("HashedPieces" + g.crlf);
//        if (this.HashedPieces == null)
//          sb.Append("  NULL" + g.crlf);
//        else
//          sb.Append(this.HashedPieces.ToGrid(10, 6) + g.crlf);

//        sb.Append(g.crlf2 + "Modifications" + g.crlf);
//        if (this.Modifications == null)
//          sb.Append("  NULL" + g.crlf);
//        else
//          sb.Append(this.Modifications.ToGrid(10, 6) + g.crlf);


//        string report = sb.ToString();
//        return report;
//      }
//    }
//}
