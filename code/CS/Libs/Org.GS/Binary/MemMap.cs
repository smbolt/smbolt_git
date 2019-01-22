using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Binary
{
  public class MemMap
  {
    public string Name { get; protected set; }
    public byte[] MemArray { get; protected set; }
    public UInt32 Offset { get; protected set; }
    public UInt32 SizeBytes { get; protected set; }
    
    public MemMap Parent { get; protected set; }
    public Dictionary<string, MemMap> Children { get; private set; }


    public MemMap(MemMap parent, string name, UInt32 offset, UInt32 sizeBytes)
    {
      try
      {
        this.Name = name;
        this.Parent = parent;
        this.Offset = offset;
        this.SizeBytes = sizeBytes;
        this.Children = new Dictionary<string, MemMap>();

        if (this.Parent == null)
        {
          this.MemArray = new byte[sizeBytes];
          this.MemArray.ZeroInit();
        }
        else
        {
          this.MemArray = new byte[this.SizeBytes];

          if (this.Parent.MemArray == null)
            throw new Exception("The Parent MemArray property is null.");

          if (this.Offset > this.Parent.MemArray.Length - 1)
            throw new Exception("The Offset value (" + this.Offset.ToString("###,###,##0") + ") is beyond the end of the Parent.MemArray length (" +
                                this.Parent.MemArray.Length.ToString("###,###,##0") + ").");

          int endPosInParent = Convert.ToInt32(this.Offset + this.SizeBytes);
          if (endPosInParent > this.Parent.MemArray.Length - 1)
            throw new Exception("The end position of the memory array for this object (" + endPosInParent.ToString("###,###,##0") + ") is beyond " +
                                "the end of the Parent.MemArray length (" + this.Parent.MemArray.Length.ToString("###,###,##0") + ".");

          Buffer.BlockCopy(this.Parent.MemArray, this.Offset.ToInt32(), this.MemArray, 0, this.SizeBytes.ToInt32()); 
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the MemMap constructor.", ex);
      }
    }
  }
}
