using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbGen
{
    public class TestNullable
    {
        private long? _nullableLong;
        public long? NullableLong
        {
            get 
            {
                if (_nullableLong.HasValue)
                    return _nullableLong.Value;
                else
                    return null;
            }

            set
            {
                _nullableLong = value;
            }
        }

        private long _notNullableLong;
        public long NotNullableLong
        {
            get { return _notNullableLong; }
            set { _notNullableLong = value; }
        }

        public TestNullable()
        {


        }

    }
}
