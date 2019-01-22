using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
    public class Interval
    {
        private int _intervalID;
        public int IntervalID
        {
            get { return _intervalID; }
            set { _intervalID = value; }
        }

        private string _intervalDesc;
        public string IntervalDesc
        {
            get { return _intervalDesc; }
            set { _intervalDesc = value; }
        }

        public Interval()
        {
            _intervalID = 0;
            _intervalDesc = String.Empty;
        }
    }
}
