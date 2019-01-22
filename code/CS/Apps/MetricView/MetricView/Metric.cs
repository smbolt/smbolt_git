using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
    public class Metric
    {
        private int _metricID;
        public int MetricID
        {
            get { return _metricID; }
            set { _metricID = value; }
        }

        private string _metricDesc;
        public string MetricDesc
        {
            get { return _metricDesc; }
            set { _metricDesc = value; }
        }

        public Metric()
        {
            _metricID = 0;
            _metricDesc = String.Empty;
        }
    }
}
