using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestMembership.Startup))]
namespace TestMembership
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
