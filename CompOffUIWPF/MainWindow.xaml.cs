using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CompOffUIWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer timer;
        private int timeToBack;

        public AppColors AppColors { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.timer = new DispatcherTimer();
            this.timer.Tick += Timer_Tick;
            this.timer.Interval = new TimeSpan(0, 0, 1);
            this.timeToBack = 0;
            this.AppColors = new AppColors
            {
                BackgroundColor = new ObservableObject<Brush>(new SolidColorBrush(Colors.Black), null),
                ForegroundColor = new ObservableObject<Brush>(new SolidColorBrush(Colors.LightGray), null)
            };
            DataContext = AppColors;
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

                var psi = new ProcessStartInfo("shutdown", "/s /t 0");
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Process.Start(psi);
            }
        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            int _hour, _minute, _secound;
            this.timeToBack = 0;
            if(string.IsNullOrEmpty(this.hour.Text) == false)
            {
                if (int.TryParse(this.hour.Text, out _hour))
                {
                    timeToBack += _hour * 3600;
                }
                else
                {

                }
            }
            if (string.IsNullOrEmpty(this.minute.Text) == false)
            {
                if (int.TryParse(this.minute.Text, out _minute))
                {
                    timeToBack += _minute * 60;
                }
                else
                {

                }
            }
            if (string.IsNullOrEmpty(this.secound.Text) == false)
            {
                if (int.TryParse(this.secound.Text, out _secound))
                {
                    timeToBack += _secound;
                }
                else
                {

                }
            }

            this.btnPlay.Background = new SolidColorBrush( Colors.LightGreen);
            this.btnPause.Background = SystemColors.ControlLightBrush;
            this.btnResume.Background = SystemColors.ControlLightBrush;

            this.timer.Start();
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            this.btnPause.Background = new SolidColorBrush(Colors.Orange);
            this.btnPlay.Background = SystemColors.ControlLightBrush;
            this.btnResume.Background = SystemColors.ControlLightBrush;

            this.timer.Stop();
        }

        private void Button_Resume(object sender, RoutedEventArgs e)
        {
            this.btnResume.Background = new SolidColorBrush(Colors.Yellow);
            this.btnPlay.Background = SystemColors.ControlLightBrush;
            this.btnPause.Background = SystemColors.ControlLightBrush;

            this.timer.Start();
        }
    }
}
