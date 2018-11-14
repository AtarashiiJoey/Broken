using ColmartCMS.Controllers;
using Microsoft.Owin;
using Owin;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;

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
            var timeMinutes = 0;
            var timeSeconds = 30;
            _timer = new System.Timers.Timer(1000 * ((timeMinutes * 60) + (timeSeconds))); // 5.5 minutes
            _timer.Elapsed += timer_Elapsed;
            _timer.Enabled = true;
            Debug.WriteLine("Timer has started");
            #endregion
        }

        public void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var timeSinceStart = DateTime.Now - _startTime;
           
            Debug.Write($"New poll: " +
                        $"\r\nDate:{DateTime.Today.ToLongDateString()}" +
                        $"\r\nTime:{DateTime.Now.ToLocalTime()}" +
                        $"\r\nTotal uptime:{(int)Math.Floor(timeSinceStart.TotalSeconds)}\r\n");
        }
    }
}
