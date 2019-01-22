using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
    [Serializable]
    public class SpecificMetric
    {
        private int _metricSource;
        public int MetricSource
        {
            get { return _metricSource; }
            set { _metricSource = value; }
        }

        private int _environmentID;
        public int EnvironmentID
        {
            get { return _environmentID; }
            set { _environmentID = value; }
        }

        private int _metricTypeID;
        public int MetricTypeID
        {
            get { return _metricTypeID; }
            set { _metricTypeID = value; }
        }

        private int _targetSystemID;
        public int TargetSystemID
        {
            get { return _targetSystemID; }
            set { _targetSystemID = value; }
        }

        private int _targetApplicationID;
        public int TargetApplicationID
        {
            get { return _targetApplicationID; }
            set { _targetApplicationID = value; }
        }

        private int _metricStateID;
        public int MetricStateID
        {
            get { return _metricStateID; }
            set { _metricStateID = value; }
        }

        private int _metricID;
        public int MetricID
        {
            get { return _metricID; }
            set { _metricID = value; }
        }

        private int _aggregateTypeID;
        public int AggregateTypeID
        {
            get { return _aggregateTypeID; }
            set { _aggregateTypeID = value; }
        }

        private int _metricValueTypeID;
        public int MetricValueTypeID
        {
            get { return _metricValueTypeID; }
            set { _metricValueTypeID = value; }
        }

        private int _intervalID;
        public int IntervalID
        {
            get { return _intervalID; }
            set { _intervalID = value; }
        }

        private int _observerSystemID;
        public int ObserverSystemID
        {
            get { return _observerSystemID; }
            set { _observerSystemID = value; }
        }

        private int _observerApplicationID;
        public int ObserverApplicationID
        {
            get { return _observerApplicationID; }
            set { _observerApplicationID = value; }
        }

        private int _observerServerID;
        public int ObserverServerID
        {
            get { return _observerServerID; }
            set { _observerServerID = value; }
        }

        private string _legendLabel;
        public string LegendLabel
        {
            get { return _legendLabel; }
            set { _legendLabel = value; }
        }

        private int _legendLabelNumber;
        public int LegendLabelNumber
        {
            get { return _legendLabelNumber; }
            set { _legendLabelNumber = value; }
        }

        private int _count;
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        // remove the below properties to remove temporarty code for forecasts.

        private bool _isMetricFromFile;
        public bool IsMetricFromFile
        {
            get { return _isMetricFromFile; }
            set { _isMetricFromFile = value; }
        }

        private string _metricFileName;
        public string MetricFileName
        {
            get { return _metricFileName; }
            set { _metricFileName = value; }
        }

        private bool _rollUpToHourly;
        public bool RollUpToHourly
        {
            get { return _rollUpToHourly; }
            set { _rollUpToHourly = value; }
        }

        private bool _useYOYData;
        public bool UseYOYData
        {
            get { return _useYOYData; }
            set { _useYOYData = value; }
        }

        private float _multiplier;
        public float Multiplier
        {
            get { return _multiplier; }
            set { _multiplier = value; }
        }


        public SpecificMetric()
        {
            _metricSource = 0; //  0=OpsMetric 1=esqlmgmt.teleflora.net
            _environmentID = 0;
            _metricTypeID = 0;
            _targetSystemID = 0;
            _targetApplicationID = 0;
            _metricStateID = 0;
            _metricID = 0;
            _aggregateTypeID = 0;
            _metricValueTypeID = 0;
            _intervalID = 0;
            _observerSystemID = 0;
            _observerApplicationID = 0;
            _observerServerID = 0;
            _legendLabel = String.Empty;
            _legendLabelNumber = 2;
            _count = 0;
            _isMetricFromFile = false;
            _metricFileName = String.Empty;
            _rollUpToHourly = false;
            _useYOYData = false;
            _multiplier = 0;
        }
    }
}
