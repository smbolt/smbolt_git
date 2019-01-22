using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Performance
{
  [XMap(XType=XType.Element)]
  public class PerfConfig
  {
    [XMap(XType=XType.Element, CollectionElements="PerfProfile")]
    public PerfProfileSet PerfProfileSet {
      get;
      set;
    }

    private string _originalFileImage;
    public string OriginalFileImage
    {
      get {
        return _originalFileImage;
      }
    }

    private string _currentFileImage;
    public string CurrentFileImage
    {
      get
      {
        CaptureCurrentFileImage();
        return _currentFileImage;
      }
    }

    public bool IsUpdated
    {
      get {
        return Get_IsUpdated();
      }
    }

    public PerfConfig()
    {
      this.PerfProfileSet = new PerfProfileSet();
    }

    public void CaptureOriginalFileImage()
    {
      ObjectFactory2 f = new ObjectFactory2();
      this._originalFileImage = f.Serialize(this).ToString();
    }

    public void CaptureCurrentFileImage()
    {
      ObjectFactory2 f = new ObjectFactory2();
      this._currentFileImage = f.Serialize(this).ToString();
    }

    private bool Get_IsUpdated()
    {
      CaptureCurrentFileImage();
      bool imagesIdentical = this._currentFileImage == this._originalFileImage;
      return !imagesIdentical;
    }
  }
}
