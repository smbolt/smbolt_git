using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DocGen.DocSpec
{
    public class PropertyTypeInfo
    {
        public bool IsNullable { get; set; }
        public bool IsEnumValue { get; set; }
        public Type NativeType { get; set; }

        public PropertyTypeInfo(bool isNullable, bool isEnumValue, Type nativeType)
        {
            this.IsNullable = isNullable;
            this.IsEnumValue = isEnumValue;
            this.NativeType = nativeType;
        }

        public bool IsSameAs(PropertyTypeInfo pti)
        {
            if (this.IsNullable == pti.IsNullable && this.IsEnumValue == pti.IsEnumValue && this.NativeType.FullName == pti.NativeType.FullName)
                return true;

            return false;
        }
    }
}
