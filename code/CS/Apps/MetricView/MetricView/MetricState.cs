using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
    public class MetricState
    {
        private int _metricStateID;
        public int MetricStateID
        {
            get { return _metricStateID; }
            set { _metricStateID = value; }
        }

        private string _metricStateDesc;
        public string MetricStateDesc
        {
            get { return _metricStateDesc; }
            set { _metricStateDesc = value; }
        }

        public MetricState()
        {
            _metricStateID = 0;
            _metricStateDesc = String.Empty;
        }
    }
}
