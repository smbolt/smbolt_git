using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
    public class MetricObservationSetCollection : System.Collections.Generic.SortedList<int, MetricObservationSet>
    {
        private float _highMetricValue;
        public float HighMetricValue
        {
            get { return _highMetricValue; }
            set { _highMetricValue = value; }
        }

        private float _lowMetricValue;
        public float LowMetricValue
        {
            get { return _lowMetricValue; }
            set { _lowMetricValue = value; }
        }

        private string _yAxisLabel;
        public string YAxisLabel
        {
            get { return _yAxisLabel; }
            set { _yAxisLabel = value; }
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

        private int _refreshInterval;
        public int RefreshInterval
        {
            get { return _refreshInterval; }
            set { _refreshInterval = value; }
        }

        private bool IsRolledUp = false;


        public MetricObservationSetCollection()
        {
            _lowMetricValue = 0;
            _highMetricValue = 0;
            _yAxisLabel = String.Empty;
            _dataPoints = 0;
            _useMostCurrentMetrics = false;
            _fromDateTime = DateTime.MinValue;
            _toDateTime = DateTime.MinValue;
            _refreshInterval = 0;
        }

        public void Rollup()
        {
            if (IsRolledUp)
                return;

            if (this.Count < 1)
                return;

            // must find a valid interval
            int intervalID = GetIntervalID();
            if (intervalID != 3)
                return;

            // get a time span equal to the interval being used
            TimeSpan intervalTimeSpan = GetIntervalTimeSpan(intervalID);

            DateTime earliestDateTime = GetEarliestDate();
            DateTime latestDateTime = GetLatestDate();
            earliestDateTime = DateTime.Parse(earliestDateTime.ToString("MM/dd/yyyy HH") + ":00:00").AddHours(1);
            latestDateTime = DateTime.Parse(latestDateTime.ToString("MM/dd/yyyy HH") + ":00:00");
            DateTime workDateTime = earliestDateTime;

            int intervals = 0;
            while (workDateTime < latestDateTime)
            {
                intervals++;
                workDateTime = workDateTime.AddHours(1);
            }

            if (intervals == 0)
                return;

            IsRolledUp = true;

            float[,] values = new float[this.Count,intervals];
            DateTime[,] intervalTime = new DateTime[this.Count, intervals];

            workDateTime = earliestDateTime;
            for (int i = 0; i < this.Count; i++)
            {
                for (int j = 0; j < intervals; j++)
                {
                    intervalTime[i,j] = workDateTime;
                    values[i,j] = 0F;
                    workDateTime = workDateTime.AddHours(1);
                }
                workDateTime = earliestDateTime;
            }

            for (int i = 0; i < this.Count; i++)
            {
                for (int j = 0; j < this.Values[i].Count; j++)
                {
                    int obsvDate = Convert.ToInt32(this.Values[i].Values[j].MetricCapturedDateTime.ToString("yyyyMMddHH"));
                    for (int k = 0; k < intervals; k++)
                    {
                        if (obsvDate == Convert.ToInt32(intervalTime[i,k].ToString("yyyyMMddHH")))
                            values[i,k] += this.Values[i].Values[j].MetricValue;
                    }
                }
            }

            int setCount = this.Count;

            MetricObservationSet[] mos = new MetricObservationSet[setCount];

            for (int i = 0; i < setCount; i++)
            {
                mos[i] = new MetricObservationSet();
                mos[i].LegendLabel = this.Values[i].LegendLabel;
                for (int j = 0; j < intervals; j++)
                {
                    MetricObservation mo = CloneMetricObservation2(this.Values[i].Values[0]);
                    mo.MetricCapturedDateTime = intervalTime[i, j];
                    mo.MetricValue = values[i, j];
                    mo.IntervalID = 7;
                    mos[i].Add(mos[i].Count, mo);
                }
            }

            this.Clear();
            for (int i = 0; i < setCount; i++)
                this.Add(this.Count, mos[i]);
        }


        public void AlignMetrics()
        {
            // the MetricObservationSetCollection must contain at least one MetricObservationSet
            if (this.Count < 1)
                return;

            DateTime latestDateTime = GetLatestDate();
            int dataPoints = 0;

            // must find a valid interval
            int intervalID = GetIntervalID();
            if (intervalID < 1 || intervalID > 8)
                return;

            // get a time span equal to the interval being used
            TimeSpan intervalTimeSpan = GetIntervalTimeSpan(intervalID);

            DateTime earliestDateTime  = DateTime.MinValue;
            DateTime metricDateTime = DateTime.MinValue;

            if (this.UseMostCurrentMetric)
            {   // compute the first metric DateTime by using the interval time span times the number of points
                earliestDateTime = latestDateTime.AddSeconds((double)(-1 * ((dataPoints - 1) * intervalTimeSpan.TotalSeconds)));
                dataPoints = this.DataPoints;
            }
            else
            {   // for date ranges, use the earliest date and the computed number of intervals
                dataPoints = NormalizeDateRange(intervalID);
                earliestDateTime = _fromDateTime;
            }

            Console.WriteLine("Earliest=" + earliestDateTime.ToString("yy-MM-dd HH:mm:ss.fff"), "  Data Points = " + dataPoints.ToString("0000"));

            //for each MetricObservationSet in this object (MetricObservationSetCollection)
            for (int i = 0; i < this.Count; i++)
            {
                // create a set of MetricObservations having the correct datetime values (aligned)
                metricDateTime = earliestDateTime;
                MetricObservationSet alignedMOS = new MetricObservationSet();
                for (int j = 0; j < dataPoints; j++)
                {
                    alignedMOS.Add(j, CloneMetricObservation(this[i].Values[0], metricDateTime));
                    metricDateTime = metricDateTime.Add(intervalTimeSpan);
                }

                // now put the metrics that match the aligned date times into the aligned metric set
                int oldPtr = 0;
                int newPtr = 0;
                while (oldPtr < this[i].Values.Count && newPtr < alignedMOS.Count)
                {
                    if (this[i].Values[oldPtr].MetricCapturedDateTime.CompareTo(alignedMOS[newPtr].MetricCapturedDateTime) == 0)
                    {
                        alignedMOS[newPtr].MetricObservedID = this[i].Values[oldPtr].MetricObservedID;
                        alignedMOS[newPtr].ReceivedFromObserverDateTime = this[i].Values[oldPtr].ReceivedFromObserverDateTime;
                        alignedMOS[newPtr].MetricValue = this[i].Values[oldPtr].MetricValue;
                        alignedMOS[newPtr].MetricDuration = this[i].Values[oldPtr].MetricDuration;
                        oldPtr++;
                        newPtr++;
                    }
                    else
                    {
                        if (this[i].Values[oldPtr].MetricCapturedDateTime.CompareTo(alignedMOS[newPtr].MetricCapturedDateTime) < 0)
                        {
                            oldPtr++;
                        }
                        else
                        {
                            newPtr++;
                        }
                    }
                }
                this[i] = alignedMOS;
            }
        }

        private MetricObservation CloneMetricObservation(MetricObservation oldMO, DateTime metricDateTime)
        {
            MetricObservation newMO = new MetricObservation();
            newMO.MetricObservedID = -1;
            newMO.ReceivedFromObserverDateTime = DateTime.MinValue;
            newMO.ObserverSystemID = oldMO.ObserverSystemID;
            newMO.ObserverApplicationID = oldMO.ObserverApplicationID;
            newMO.ObserverServerID = oldMO.ObserverServerID;
            newMO.TargetSystemID = oldMO.TargetSystemID;
            newMO.TargetApplicationID = oldMO.TargetApplicationID;
            newMO.EnvironmentID = oldMO.EnvironmentID;
            newMO.AggregateTypeID = oldMO.AggregateTypeID;
            newMO.MetricID = oldMO.MetricID;
            newMO.MetricStateID = oldMO.MetricStateID;
            newMO.MetricTypeID = oldMO.MetricTypeID;
            newMO.MetricValueTypeID = oldMO.MetricValueTypeID;
            newMO.IntervalID = oldMO.IntervalID;
            newMO.MetricValue = 0F;
            newMO.MetricCapturedDateTime = metricDateTime;
            newMO.MetricDuration = TimeSpan.MinValue;
            return newMO;
        }



        private MetricObservation CloneMetricObservation2(MetricObservation oldMO)
        {
            MetricObservation newMO = new MetricObservation();
            newMO.MetricObservedID = -1;
            newMO.ReceivedFromObserverDateTime = DateTime.MinValue;
            newMO.ObserverSystemID = oldMO.ObserverSystemID;
            newMO.ObserverApplicationID = oldMO.ObserverApplicationID;
            newMO.ObserverServerID = oldMO.ObserverServerID;
            newMO.TargetSystemID = oldMO.TargetSystemID;
            newMO.TargetApplicationID = oldMO.TargetApplicationID;
            newMO.EnvironmentID = oldMO.EnvironmentID;
            newMO.AggregateTypeID = oldMO.AggregateTypeID;
            newMO.MetricID = oldMO.MetricID;
            newMO.MetricStateID = oldMO.MetricStateID;
            newMO.MetricTypeID = oldMO.MetricTypeID;
            newMO.MetricValueTypeID = oldMO.MetricValueTypeID;
            newMO.IntervalID = oldMO.IntervalID;
            newMO.MetricValue = 0F;
            newMO.MetricCapturedDateTime = oldMO.MetricCapturedDateTime;
            newMO.MetricDuration = TimeSpan.MinValue;
            return newMO;
        }


        private DateTime GetEarliestDate()
        {
            DateTime dtEarliest = DateTime.MaxValue;
            for (int i = 0; i < this.Count; i++)
            {
                // find the MetricObservationSet that contains the latest datetime value
                if (this[i].Values[0].MetricStateID != 2 && this[i].Values[0].MetricStateID != 3)
                    if (dtEarliest.CompareTo(this[i].Values[0].MetricCapturedDateTime) > 0)
                        dtEarliest = this[i].Values[0].MetricCapturedDateTime;
            }
            return dtEarliest;
        }
        private DateTime GetLatestDate()
        {
            DateTime dtLatest = DateTime.MinValue;
            for (int i = 0; i < this.Count; i++)
            {
                // find the MetricObservationSet that contains the latest datetime value
                //if (this[i].Values[0].MetricStateID != 2 && this[i].Values[0].MetricStateID != 3)
                    if (dtLatest.CompareTo(this[i].Values[this[i].Values.Count - 1].MetricCapturedDateTime) < 0)
                        dtLatest = this[i].Values[this[i].Values.Count - 1].MetricCapturedDateTime;
            }
            return dtLatest;
        }

        private int GetIntervalID()
        {
            int intervalID = 0;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Values.Count > 0 && this[i].Values[0].MetricStateID != 2 && this[i].Values[0].MetricStateID != 3)
                    intervalID = this[i].Values[0].IntervalID;
            }
            return intervalID;
        }

        private TimeSpan GetIntervalTimeSpan(int intervalID)
        {
            TimeSpan ts = new TimeSpan(0);

            switch (intervalID)
            {
                case 1:
                    ts = new TimeSpan(0, 0, 0, 1, 0); // 1 second
                    break;

                case 2:
                    ts = new TimeSpan(0, 0, 1, 0, 0); // 1 minute
                    break;

                case 3:
                    ts = new TimeSpan(0, 0, 5, 0, 0); // 5 minutes
                    break;

                case 4:
                    ts = new TimeSpan(0, 0, 10, 0, 0); // 10 minutes
                    break;

                case 5:
                    ts = new TimeSpan(0, 0, 15, 0, 0); // 15 minutes
                    break;

                case 6:
                    ts = new TimeSpan(0, 0, 30, 0, 0); // 30 minutes
                    break;

                case 7:
                    ts = new TimeSpan(0, 0, 60, 0, 0); // 60 minutes
                    break;

                case 8:
                    ts = new TimeSpan(1, 0, 0, 0, 0); // 1 day
                    break;
            }

            return ts;
        }

        private int NormalizeDateRange(int intervalID)
        {
            int intervals = 0;
            TimeSpan ts = TimeSpan.MinValue;

            switch (intervalID)
            {
                case 1:
                    _fromDateTime = new DateTime(_fromDateTime.Year, _fromDateTime.Month, _fromDateTime.Day,
                        _fromDateTime.Hour, _fromDateTime.Minute, _fromDateTime.Second, 0);
                    _toDateTime = new DateTime(_toDateTime.Year, _toDateTime.Month, _toDateTime.Day,
                        _toDateTime.Hour, _toDateTime.Minute, _toDateTime.Second, 0);
                    ts = _toDateTime.Subtract(_fromDateTime);
                    intervals = Convert.ToInt32(ts.TotalSeconds + 1);
                    break;

                case 2:
                    _fromDateTime = new DateTime(_fromDateTime.Year, _fromDateTime.Month, _fromDateTime.Day,
                        _fromDateTime.Hour, _fromDateTime.Minute, 0, 0);
                    _toDateTime = new DateTime(_toDateTime.Year, _toDateTime.Month, _toDateTime.Day,
                        _toDateTime.Hour, _toDateTime.Minute, 0, 0);
                    ts = _toDateTime.Subtract(_fromDateTime);
                    intervals = Convert.ToInt32(ts.TotalMinutes) + 1;
                    break;

                case 3:
                    _fromDateTime = NormalizeDateToInterval(_fromDateTime, 5, false);
                    _toDateTime = NormalizeDateToInterval(_toDateTime, 5, true);
                    ts = _toDateTime.Subtract(_fromDateTime);
                    intervals = (Convert.ToInt32(ts.TotalMinutes) / 5) + 1;
                    break;

                case 4:
                    _fromDateTime = NormalizeDateToInterval(_fromDateTime, 10, false);
                    _toDateTime = NormalizeDateToInterval(_toDateTime, 10, true);
                    ts = _toDateTime.Subtract(_fromDateTime);
                    intervals = (Convert.ToInt32(ts.TotalMinutes) / 10) + 1;
                    break;

                case 5:
                    _fromDateTime = NormalizeDateToInterval(_fromDateTime, 15, false);
                    _toDateTime = NormalizeDateToInterval(_toDateTime, 15, true);
                    ts = _toDateTime.Subtract(_fromDateTime);
                    intervals = (Convert.ToInt32(ts.TotalMinutes) / 15) + 1;
                    break;

                case 6:
                    _fromDateTime = NormalizeDateToInterval(_fromDateTime, 30, false);
                    _toDateTime = NormalizeDateToInterval(_toDateTime, 30, true);
                    ts = _toDateTime.Subtract(_fromDateTime);
                    intervals = (Convert.ToInt32(ts.TotalMinutes) / 30) + 1;
                    break;

                case 7:
                    _fromDateTime = new DateTime(_fromDateTime.Year, _fromDateTime.Month, _fromDateTime.Day,
                        _fromDateTime.Hour, 0, 0, 0);
                    _toDateTime = new DateTime(_toDateTime.Year, _toDateTime.Month, _toDateTime.Day,
                        _toDateTime.Hour, 0, 0, 0);
                    ts = _toDateTime.Subtract(_fromDateTime);
                    intervals = Convert.ToInt32(ts.TotalHours) + 1;
                    break;

                case 8:
                    _fromDateTime = new DateTime(_fromDateTime.Year, _fromDateTime.Month, _fromDateTime.Day);
                    _toDateTime = new DateTime(_toDateTime.Year, _toDateTime.Month, _toDateTime.Day);
                    ts = _toDateTime.Subtract(_fromDateTime);
                    break;
            }

            return intervals;
        }

        private DateTime NormalizeDateToInterval(DateTime date, int interval, bool roundUp)
        {
            int minutes = 0;
            bool addHour = false;

            if (roundUp)
                minutes = date.Minute - (date.Minute % interval) + interval;
            else
                minutes = date.Minute - (date.Minute % interval);

            if (minutes == 60)
            {
                minutes = 0;
                addHour = true;
            }

            DateTime newDate = new DateTime(date.Year, date.Month, date.Day, date.Hour, minutes, 0, 0);
            if (addHour)
                newDate = newDate.AddHours(1);

            return newDate;
        }

    }
}
