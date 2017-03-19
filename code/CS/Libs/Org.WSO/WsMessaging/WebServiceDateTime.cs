using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.WSO
{
  public class WebServiceDateTime
  {
    private bool _isUsed;
    public bool IsUsed
    {
      get { return _isUsed; }
      set { _isUsed = value; }
    }

    private DateTime _dateTime;
    public DateTime DateTime
    {
      get { return _dateTime; }
      set { _dateTime = value; }
    }

    private TimeZoneInfo _timeZoneInfo;
    public TimeZoneInfo TimeZoneInfo
    {
      get { return _timeZoneInfo; }
      set { _timeZoneInfo = value; }
    }

    public WebServiceDateTime()
    {
      _isUsed = false;
      _dateTime = DateTime.MinValue;
      _timeZoneInfo = TimeZoneInfo.Utc;
    }

    public WebServiceDateTime(DateTime dateTime, TimeZoneInfo timeZoneInfo)
    {
      _isUsed = true;
      _dateTime = dateTime;
      _timeZoneInfo = timeZoneInfo;
    }

    public bool IsMinValue()
    {
      if (_dateTime == DateTime.MinValue)
        return true;

      return false;
    }

    public bool IsMaxValue()
    {
      if (_dateTime == DateTime.MaxValue)
        return true;

      return false;
    }
  }
}
