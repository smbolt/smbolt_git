using System;
using System.Collections.Generic;
using System.Text;

namespace NameTags
{
    [Serializable]
    public class PersonSet : SortedList<string, Person>
    {
    }
}
