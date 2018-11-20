using Colmart.Sync;
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
        private string path = @"C:\Users\Dev-2018-Oct-PC\Desktop\Colmart\RepoVPS\Colmart\Colmart\Sync\Log.txt";
        ProductsController pcCMS = new ProductsController();
        UpdateController Uc = new UpdateController();
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
                using (TextWriter textWriter = new StreamWriter(path))
                {
                    textWriter.WriteLine("Colmart Polling log:");
                }
            }
            else if (File.Exists(path))
            {
                using (var textWriter = new StreamWriter(path, true))
                {
                    textWriter.WriteLine($"{DateTime.Now} : Timer has started");
                }
            }
            Debug.WriteLine($"{DateTime.Now} : Timer has started");
            #endregion
        }
        #region Polling action from timer

        public async void PollDataFromColmart(object sender, System.Timers.ElapsedEventArgs e)
        {
            var timeSinceStart = DateTime.Now - _startTime;

            // This is where we place the calls
            Debug.WriteLine("Poll.........................");
            Uc.UcThreader(1);
            // End Thread call

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
                using (TextWriter textWriter = new StreamWriter(path))
                {
                    textWriter.WriteLine("Colmart Polling log:");
                }
            }
            else if (File.Exists(path))
            {
                using (var textWriter = new StreamWriter(path, true))
                {
                    textWriter.WriteLine($"{DateTime.Now} : New poll - ");
                    textWriter.WriteLine($"{DateTime.Now} : Total up-time {(int)Math.Floor(timeSinceStart.TotalSeconds)} seconds");
                }
            }
        }
        #endregion
    }
}
