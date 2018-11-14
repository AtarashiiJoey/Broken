using ColmartCMS.Controllers;
using Microsoft.Owin;
using Owin;
using System;
using System.Diagnostics;

[assembly: OwinStartup(typeof(Colmart.Startup))]
namespace Colmart
{
    public partial class Startup
    {
        #region Polling timer setup
        private System.Timers.Timer _timer;
        private DateTime _startTime;
        #endregion

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            #region Timer startup, run, and check
            _startTime = DateTime.Now;
            var timeMinutes = 15;
            var timeSeconds = 0;
            _timer = new System.Timers.Timer(1000 * ((timeMinutes * 60) + (timeSeconds))); // 5.5 minutes
            _timer.Elapsed += PollDataFromColmart;
            _timer.Enabled = true;
            Debug.WriteLine("Timer has started");
            #endregion
        }
        #region Polling action from timer
        public async void PollDataFromColmart(object sender, System.Timers.ElapsedEventArgs e)
        {
            var timeSinceStart = DateTime.Now - _startTime;

            var pc = new ProductsController();
            await pc.ProductsImport();

            Debug.Write($"New poll: " +
                        $"\r\nDate:{DateTime.Today.ToLongDateString()}" +
                        $"\r\nTime:{DateTime.Now.ToLocalTime()}" +
                        $"\r\nTotal up-time:{(int)Math.Floor(timeSinceStart.TotalSeconds)}\r\n");
        }
        #endregion
    }
}
