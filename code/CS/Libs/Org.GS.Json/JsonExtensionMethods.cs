using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Org.Json;
using Org.GS;

namespace Org.GS.Json
{
  public static class JsonExtensionMethods
  {
    //public static CommandSet JsonToCommandSet(this string s)
    //{
    //  try
    //  {
    //    if (s == null)
    //      return null;

    //    return (CommandSet) s.JsonDeserialize<CommandSet>();
    //  }
    //  catch(Exception ex)
    //  {
    //    throw new Exception("An exception occurred while attempting to convert a JSON string to a CommandSet object." +
    //                        "The first (up to) 50 characters of the JSON string is '" + (s == null ? "[null string]" : s.First50()) + "'.", ex);
    //  }
    //}
  }
}
