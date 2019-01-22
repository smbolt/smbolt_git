using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.AppDomainManagement
{
  public class MarshalBase : MarshalByRefObject
  {
    private static TimeSpan _initialLeaseTime = TimeSpan.FromMinutes(75);
    public static TimeSpan InitialLeaseTime
    {
      get {
        return _initialLeaseTime;
      }
      set {
        _initialLeaseTime = value;
      }
    }

    private static TimeSpan _renewOnCallTime = TimeSpan.FromMinutes(75);
    public static TimeSpan RenewOnCallTime
    {
      get {
        return _renewOnCallTime;
      }
      set {
        _renewOnCallTime = value;
      }
    }

    private static TimeSpan _sponsorshipTimeOut = TimeSpan.FromMinutes(2);
    public static TimeSpan SponsorshipTimeout
    {
      get {
        return _sponsorshipTimeOut;
      }
      set {
        _sponsorshipTimeOut = value;
      }
    }

    private ILease _lease;
    public ILease Lease
    {
      get
      {
        if (_lease == null)
        {
          _lease = (ILease)InitializeLifetimeService();
        }

        return _lease;
      }
    }

    public MarshalBase()
    {
      _lease = (ILease) InitializeLifetimeService();
    }

    public override object InitializeLifetimeService()
    {
      ILease lease = (ILease) base.InitializeLifetimeService();
      switch (lease.CurrentState)
      {
        case LeaseState.Initial:
          lease.InitialLeaseTime = InitialLeaseTime;
          lease.RenewOnCallTime = RenewOnCallTime;
          lease.SponsorshipTimeout = SponsorshipTimeout;
          break;

        case LeaseState.Active:
          lease.Renew(RenewOnCallTime);
          break;
      }

      return lease;
    }

    public bool TestExistence()
    {
      return true;
    }
  }
}
