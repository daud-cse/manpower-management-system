using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MMS.API.Startup))]
namespace MMS.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
