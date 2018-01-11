using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(N_Medical.Startup))]
namespace N_Medical
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
