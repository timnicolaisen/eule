using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppEule.Startup))]
namespace AppEule
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
