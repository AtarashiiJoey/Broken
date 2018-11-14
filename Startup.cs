using ColmartCMS.Controllers;
using Microsoft.Owin;
using Owin;
using System;
using System.Diagnostics;
using System.IO;

[assembly: OwinStartup(typeof(Colmart.Startup))]
namespace Colmart
{
    public partial class Startup
    {
        #region Polling timer setup
        private System.Timers.Timer _timer;
        private DateTime _startTime;
        private string path = @"C:\Users\Dev-2018-Oct-PC\AppData\Roaming\Colmart\Log.txt";
        ProductsController pcCMS = new ProductsController();
        #endregion

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            #region Timer startup, run, and check
            _startTime = DateTime.Now;
            var timeMinutes = 5;
            var timeSeconds = 0;
            _timer = new System.Timers.Timer(1000 * ((timeMinutes * 60) + (timeSeconds))); // 5.5 minutes
            _timer.Elapsed += PollDataFromColmart;
            _timer.Enabled = true;
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();

                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine("Colmart Polling log:");
                }
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine($"{DateTime.Now} : Timer has started");
                }
            }
            Debug.WriteLine($"{DateTime.Now} : Timer has started");
            #endregion
        }
        #region Polling action from timer
        public async void PollDataFromColmart(object sender, System.Timers.ElapsedEventArgs e)
        {
            var timeSinceStart = DateTime.Now - _startTime;
            await pcCMS.ProductsImport();
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();

                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine("Colmart Polling log:");
                }
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine($"{DateTime.Now} : New poll - ");
                    tw.WriteLine($"{DateTime.Now} : Total up-time {(int)Math.Floor(timeSinceStart.TotalSeconds)} seconds");
                }
            }
        }
        #endregion
    }
}
