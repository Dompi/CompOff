using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CompOffUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly DispatcherTimer timer;
        private int timeToBack;
        public Brush BackgroundColor { get; set; }
        public Brush ForegroundColor { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            this.timer = new DispatcherTimer();
            this.timer.Tick += Timer_Tick;
            this.timer.Interval = new TimeSpan(0, 0, 1);
            this.timeToBack = 0;
            this.BackgroundColor = new SolidColorBrush(Colors.Black);
            this.ForegroundColor = new SolidColorBrush(Colors.LightGray);
        }

        private void Timer_Tick(object sender, object e)
        {
            this.timeToBack--;
            int time = timeToBack;

            int hours = (time / 3600);
            this.hourBack.Text = hours.ToString();
            time = time - (hours * 3600);

            int minutes = (time / 60);
            this.minuteBack.Text = minutes.ToString();
            time = time - (minutes * 60);

            this.secoundBack.Text = time.ToString();

            if (this.timeToBack <= 0)
            {
                this.timer.Stop();

                //ShutdownManager.BeginShutdown(ShutdownKind.Shutdown, TimeSpan.FromSeconds(0));

                //var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                //psi.CreateNoWindow = true;
                //psi.UseShellExecute = false;
                //Process.Start(psi);
            }

        //    throw new NotImplementedException();
        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            int _hour, _minute, _secound;
            this.timeToBack = 0;
            if (int.TryParse(this.hour.Text, out _hour))
            {
                timeToBack += _hour * 3600;
                if (int.TryParse(this.minute.Text, out _minute))
                {
                    timeToBack += _minute * 60;
                    if (int.TryParse(this.secound.Text, out _secound))
                    {
                        timeToBack += _secound;
                        this.timer.Start();
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
            else
            {

            }
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            this.timer.Stop();
        }
    }
}
