using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MMS.FMS.Startup))]
namespace MMS.FMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
