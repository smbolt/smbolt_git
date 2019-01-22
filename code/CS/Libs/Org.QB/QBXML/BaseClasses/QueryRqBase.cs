using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.QB.QBXML
{
  public class QueryRqBase : QbXmlBase
  {
    public QueryMeta QueryMeta {
      get;
      set;
    }

    [XMap (XType = XType.Element, Name = "ListID")]
    public ListIdList ListIdList {
      get;
      set;
    }

    [XMap (XType = XType.Element, Name = "FullName")]
    public FullNameList FullNameList {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public int? MaxReturned {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public ActiveStatus? ActiveStatus {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public DateTime? FromModifiedDate {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public DateTime? ToModifiedDate {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public NameFilter NameFilter {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public NameRangeFilter NameRangeFilter {
      get;
      set;
    }

    [XMap(XType = XType.Element, Name = "IncludeRetElement")]
    public IncludeRetElementList IncludeRetElementList {
      get;
      set;
    }

    [XMap(XType = XType.Element, Name = "OwnerID")]
    public OwnerIdList OwnerIdList {
      get;
      set;
    }

    public QueryRqBase()
    {
      this.QueryMeta = null;
      this.ListIdList = null;
      this.FullNameList = null;
      this.MaxReturned = null;
      this.ActiveStatus = null;
      this.FromModifiedDate = null;
      this.ToModifiedDate = null;
      this.NameFilter = null;
      this.NameRangeFilter = null;
      this.IncludeRetElementList = null;
      this.OwnerIdList = null;
    }
  }
}
