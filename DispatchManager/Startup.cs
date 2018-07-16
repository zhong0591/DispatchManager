using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DispatchManager.Startup))]
namespace DispatchManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
