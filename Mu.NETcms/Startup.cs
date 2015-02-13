using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mu.NETcms.Startup))]
namespace Mu.NETcms
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
