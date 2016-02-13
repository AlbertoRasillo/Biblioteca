using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(App1Web.Startup))]
namespace App1Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
