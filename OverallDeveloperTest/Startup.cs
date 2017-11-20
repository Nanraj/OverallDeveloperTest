using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OverallDeveloperTest.Startup))]
namespace OverallDeveloperTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
