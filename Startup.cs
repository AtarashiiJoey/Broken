using Microsoft.Owin;
using MsgApp.Controllers;
using Owin;

[assembly: OwinStartup(typeof(Colmart.Startup))]
namespace Colmart
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            #region Test Area Star
            
            // test managers
            var sc = new ServiceController();
            sc.clsMessageServiceClient(15);
            // test method

            #endregion 

        }

    }
}
