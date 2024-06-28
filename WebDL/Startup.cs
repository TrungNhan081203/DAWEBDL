using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebDL.Startup))]
namespace WebDL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
