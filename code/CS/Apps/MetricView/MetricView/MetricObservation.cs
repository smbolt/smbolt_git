using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
    public class MetricObservation
    {
        private int _metricObservedID;
        public int MetricObservedID
        {
            get { return _metricObservedID; }
            set { _metricObservedID = value; }
        }

        private DateTime _receivedFromObserverDateTime;
        public DateTime ReceivedFromObserverDateTime
        {
            get { return _receivedFromObserverDateTime; }
            set { _receivedFromObserverDateTime = value; }
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

        private int _environmentID;
        public int EnvironmentID
        {
            get { return _environmentID; }
            set { _environmentID = value; }
        }

        private int _aggregateTypeID;
        public int AggregateTypeID
        {
            get { return _aggregateTypeID; }
            set { _aggregateTypeID = value; }
        }

        private int _metricID;
        public int MetricID
        {
            get { return _metricID; }
            set { _metricID = value; }
        }

        private int _metricStateID;
        public int MetricStateID
        {
            get { return _metricStateID; }
            set { _metricStateID = value; }
        }

        private int _metricTypeID;
        public int MetricTypeID
        {
            get { return _metricTypeID; }
            set { _metricTypeID = value; }
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

        private float _metricValue;
        public float MetricValue
        {
            get { return _metricValue; }
            set { _metricValue = value; }
        }

        private DateTime _metricCapturedDateTime;
        public DateTime MetricCapturedDateTime
        {
            get { return _metricCapturedDateTime; }
            set { _metricCapturedDateTime = value; }
        }

        private TimeSpan _metricDuration;
        public TimeSpan MetricDuration
        {
            get { return _metricDuration; }
            set { _metricDuration = value; }
        }

        public MetricObservation()
        {
            _metricObservedID = 0;
            _receivedFromObserverDateTime = DateTime.MinValue;
            _observerSystemID = 0;
            _observerApplicationID = 0;
            _observerServerID = 0;
            _targetSystemID = 0;
            _targetApplicationID = 0;
            _environmentID = 0;
            _aggregateTypeID = 0;
            _metricID = 0;
            _metricStateID = 0;
            _metricTypeID = 0;
            _metricValueTypeID = 0;
            _intervalID = 0;
            _metricValue = 0F;
            _metricCapturedDateTime = DateTime.MinValue;
            _metricDuration = TimeSpan.MinValue;
        }
    }
}
