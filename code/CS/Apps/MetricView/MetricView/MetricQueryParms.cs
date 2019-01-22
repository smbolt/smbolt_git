using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
    public class MetricQueryParms
    {
        private bool _isEnvironmentIDSpecified;
        public bool IsEnvironmentIDSpecified
        {
            get { return _isEnvironmentIDSpecified; }
            set { _isEnvironmentIDSpecified = value; }
        }

        private int _environmentID;
        public int EnvironmentID
        {
            get { return _environmentID; }
            set { _environmentID = value; }
        }

        private bool _isTargetSystemIDSpecified;
        public bool IsTargetSystemIDSpecified
        {
            get { return _isTargetSystemIDSpecified; }
            set { _isTargetSystemIDSpecified = value; }
        }

        private int _targetSystemID;
        public int TargetSystemID
        {
            get { return _targetSystemID; }
            set { _targetSystemID = value; }
        }

        private bool _isTargetApplicationIDSpecified;
        public bool IsTargetApplicationIDSpecified
        {
            get { return _isTargetApplicationIDSpecified; }
            set { _isTargetApplicationIDSpecified = value; }
        }

        private int _targetApplicationID;
        public int TargetApplicationID
        {
            get { return _targetApplicationID; }
            set { _targetApplicationID = value; }
        }

        private string _serverList;
        public string ServerList
        {
            get { return _serverList; }
            set { _serverList = value; }
        }

        private bool _isServerListSpecified;
        public bool IsServerListSpecified
        {
            get { return _isServerListSpecified; }
            set { _isServerListSpecified = value; }
        }

        private bool _useMostCurrentMetrics;
        public bool UseMostCurrentMetric
        {
            get { return _useMostCurrentMetrics; }
            set { _useMostCurrentMetrics = value; }
        }

        private int _dataPoints;
        public int DataPoints
        {
            get { return _dataPoints; }
            set { _dataPoints = value; }
        }

        private bool _isLimitedByDates;
        public bool IsLimitedByDates
        {
            get { return _isLimitedByDates; }
            set { _isLimitedByDates = value; }
        }

        private DateTime _fromDateTime;
        public DateTime FromDateTime
        {
            get { return _fromDateTime; }
            set { _fromDateTime = value; }
        }

        private DateTime _toDateTime;
        public DateTime ToDateTime
        {
            get { return _toDateTime; }
            set { _toDateTime = value; }
        }


        public MetricQueryParms()
        {
            _isEnvironmentIDSpecified = false;
            _environmentID = 0;
            _isTargetSystemIDSpecified = false;
            _targetSystemID = 0;
            _isTargetApplicationIDSpecified = false;
            _targetApplicationID = 0;
            _serverList = String.Empty;
            _isServerListSpecified = false;
            _useMostCurrentMetrics = false;
            _dataPoints = 0;
            _isLimitedByDates = false;
            _fromDateTime = DateTime.MinValue;
            _toDateTime = DateTime.MinValue;
        }



    }
}
