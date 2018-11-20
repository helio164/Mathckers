using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mathckers_.Startup))]
namespace Mathckers_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
