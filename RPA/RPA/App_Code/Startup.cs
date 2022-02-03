using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RPA.Startup))]
namespace RPA
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
