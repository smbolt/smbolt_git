using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  public enum QueryFunction
  {
    Select = 0
  }

  public class Query
  {
    public QueryFunction QueryFunction {
      get;
      set;
    }
    public List<TypeSpec> TypeSpecs {
      get;
      set;
    }


    public Query(string queryString)
    {
      if (queryString.CharCount('[') != 1 || queryString.CharCount(']') != 1)
        throw new Exception("Document query '" + queryString + "' is not valid.  Must include one and only one of each of the characters '[' and ']'.");

      List<string> tokens = queryString.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries).ToList();

      if (tokens.Count != 2)
        throw new Exception("Document query '" + queryString + "' is not valid.  Must have the form of  'verb[specification]'.");

      this.QueryFunction = GetQueryFunction(tokens[0]);
      this.TypeSpecs = BuildTypeSpecs(tokens[1]);
    }

    private QueryFunction GetQueryFunction(string s)
    {
      switch (s)
      {
        case "get":
          return DocSpec.QueryFunction.Select;
      }

      throw new Exception("Document query function (verb) '" + s + "' is not valid.");
    }

    public List<TypeSpec> BuildTypeSpecs(string typeSpec)
    {
      List<TypeSpec> specs = new List<TypeSpec>();

      // For now we are just populating the first TypeSpec, we may add more later.

      TypeSpec ts = new TypeSpec(typeSpec);
      specs.Add(ts);

      return specs;
    }

  }
}
