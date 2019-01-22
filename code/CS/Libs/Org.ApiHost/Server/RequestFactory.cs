using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.ApiHost
{
    public class RequestFactory
    {
        public static RequestHandlerBase Get(IDictionary<string, object> env, IEnumerable<Route> routes)
        {
            var httpMethod = ((string)env["owin.RequestMethod"]).ToUpper();

            switch (httpMethod)
            {
                case "GET": return new GetHandler(env, routes);
                case "POST": throw new NotImplementedException("POST handler");
                default: throw new NotImplementedException("No handler found for: " + httpMethod);
            }
        }
    }
}
