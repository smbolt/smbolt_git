using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{
  public class Algorithm
  {
    private string _rawSpec;
    public AlgorithmType AlgorithmType { get; set; }
    public string AlgorithmSpec { get; set; }
    public string Report { get { return Get_Report(); } }

    public Algorithm(string rawSpec)
    {
      _rawSpec = rawSpec.Trim();
      this.AlgorithmType = AlgorithmType.NotSet;
      this.AlgorithmSpec = String.Empty;
      Parse();
    }

    private void Parse()
    {
      if (_rawSpec.IsBlank())
        throw new Exception("The /algorithm specification must contain a valid algorithm specification after a colon, i.e. '/algorithm:[spec]'.");

      int pos = _rawSpec.IndexOf("(");

      string algorithmType = String.Empty;
      string algorithmRawSpec = String.Empty;

      if (pos == -1)
      {
        algorithmType = _rawSpec;
      }
      else
      {
        algorithmType = _rawSpec.Substring(0, pos);
        algorithmRawSpec = _rawSpec.Substring(pos);
      }
      

      this.AlgorithmType = g.ToEnum<AlgorithmType>(algorithmType, AlgorithmType.NotSet);

      switch (this.AlgorithmType)
      {
        case AlgorithmType.RightMostNonBlankCell:
        case AlgorithmType.LeftMostNonBlankCell:
        case AlgorithmType.TopMostNonBlankCell:
        case AlgorithmType.BottomMostNonBlankCell:
          if (algorithmRawSpec.IsNotBlank())
            throw new Exception("The AlgorithmType '" + this.AlgorithmType.ToString() + "' should not have a parenthetical expression as part of its " +
                                "specification - found '" + algorithmRawSpec + "'.");
          break;

        case AlgorithmType.NotSet:
          throw new Exception("An invalid AlgorithmType value was encountered '" + _rawSpec + "'.");

      }
    }

    private string Get_Report()
    {
      return _rawSpec;
    }
  }
}
