using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Org.ApiHost.Controllers
{
    public class AccountController : ApiController
    {
        [Authorize]
        [Route("api/account")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Secure", "Account" };
        }

        [Route("api/account/{id}")]
        public IEnumerable<string> Get(int id)
        {
            return new string[] { "NotSecure", "Account" + id.ToString() };
        }
    }
}
