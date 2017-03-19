using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class EncryptedFilePair
  {
    public string EncryptedFilePath { get; set; }
    public string UnencryptedFilePath { get; set; }
    public string RawText { get; set; }
    public string OriginalFormattedText { get; set; }
    public string CurrentFormattedText { get; set; }
    public bool IsUpdated { get { return Get_IsUpdated(); } }

    public EncryptedFilePair()
    {
      this.EncryptedFilePath = String.Empty;
      this.UnencryptedFilePath = String.Empty;
      this.RawText = String.Empty;
      this.OriginalFormattedText = String.Empty;
      this.CurrentFormattedText = String.Empty;
    }
    
    private bool Get_IsUpdated()
    {
      if (this.OriginalFormattedText.IsBlank() || this.CurrentFormattedText.IsBlank())
        return false;


      return this.OriginalFormattedText != this.CurrentFormattedText;
    }
  }
}
