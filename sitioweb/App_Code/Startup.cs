using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sitioweb.Startup))]
namespace sitioweb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
