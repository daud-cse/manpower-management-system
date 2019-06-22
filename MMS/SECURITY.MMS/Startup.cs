using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MMS.SECURITY.Startup))]
namespace MMS.SECURITY
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
