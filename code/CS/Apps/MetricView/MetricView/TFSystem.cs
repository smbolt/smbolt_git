using System;
using System.Collections.Generic;
using System.Text;

namespace Teleflora.Operations.MetricView
{
    public class TFSystem
    {
        private int _systemID;
        public int SystemID
        {
            get { return _systemID; }
            set { _systemID = value; }
        }

        private string _systemDesc;
        public string SystemDesc
        {
            get { return _systemDesc; }
            set { _systemDesc = value; }
        }

        public TFSystem()
        {
            _systemID = 0;
            _systemDesc = String.Empty;
        }

    }
}
