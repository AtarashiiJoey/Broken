using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Colmart.Startup))]
namespace Colmart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
