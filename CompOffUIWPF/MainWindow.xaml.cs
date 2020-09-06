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

        public AppData AppData { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.timer = new DispatcherTimer();
            this.timer.Tick += Timer_Tick;
            this.timer.Interval = new TimeSpan(0, 0, 1);
            this.timeToBack = 0;
            this.AppData = new AppData
            {
                BackgroundColor = new ObservableObject<Brush>(new SolidColorBrush(Colors.Black), null),
                ForegroundColor = new ObservableObject<Brush>(new SolidColorBrush(Colors.LightGray), null),
                DateVisibility = new ObservableObject<Visibility>(Visibility.Collapsed),
                TimerVisibility = new ObservableObject<Visibility>(Visibility.Collapsed),
                ActualModeButtonType = ModeButtons.TimerPicker,
                SelectedDate = new ObservableObject<DateTime>(DateTime.Parse(DateTime.Now.ToString("d")))
            };
            ModeButtonsUISetter(this.AppData.ActualModeButtonType);

            DataContext = AppData;
        }

        private void Timer_Tick(object sender, object e)
        {
            this.timeToBack--;
            int time = this.timeToBack;

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
                //Process.Start(psi);
            }
        }

        // Mode Handler Buttons
        private void Button_Start(object sender, RoutedEventArgs e)
        {
            int _hour, _minute, _secound;
            this.timeToBack = 0;
            switch (this.AppData.ActualModeButtonType)
            {
                case ModeButtons.TimerPicker:
                    if (string.IsNullOrEmpty(this.hour.Text) == false)
                    {
                        if (int.TryParse(this.hour.Text, out _hour))
                        {
                            this.timeToBack += _hour * 3600;
                        }
                    }
                    if (string.IsNullOrEmpty(this.minute.Text) == false)
                    {
                        if (int.TryParse(this.minute.Text, out _minute))
                        {
                            this.timeToBack += _minute * 60;
                        }
                    }
                    if (string.IsNullOrEmpty(this.secound.Text) == false)
                    {
                        if (int.TryParse(this.secound.Text, out _secound))
                        {
                            this.timeToBack += _secound;
                        }
                    }
                    break;
                case ModeButtons.DatePicker:
                    var selectedDate = this.dateSelector.SelectedDate;
                    if (selectedDate.HasValue)
                    {
                        if (string.IsNullOrEmpty(this.hour2.Text) == false)
                        {
                            if (int.TryParse(this.hour2.Text, out _hour))
                            {
                                selectedDate = selectedDate.Value.AddHours(_hour);
                            }
                        }
                        if (string.IsNullOrEmpty(this.minute2.Text) == false)
                        {
                            if (int.TryParse(this.minute2.Text, out _minute))
                            {
                                selectedDate = selectedDate.Value.AddMinutes(_minute);
                            }
                        }

                        this.timeToBack = (int)(selectedDate - DateTime.Now).Value.TotalSeconds;
                    }
                    break;
                default:
                    break;
            }


            OperationButtonsUISetter(OperationButtons.Start);
            this.timer.Start();
        }
        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            OperationButtonsUISetter(OperationButtons.Pause);
            this.timer.Stop();
        }
        private void Button_Resume(object sender, RoutedEventArgs e)
        {
            OperationButtonsUISetter(OperationButtons.Resume);
            this.timer.Start();
        }

        // Time Picker Buttons
        private void Button_DatePicker(object sender, RoutedEventArgs e)
        {
            this.AppData.ActualModeButtonType = ModeButtons.DatePicker;
            ModeButtonsUISetter(this.AppData.ActualModeButtonType);
        }
        private void Button_TimerPicker(object sender, RoutedEventArgs e)
        {
            this.AppData.ActualModeButtonType = ModeButtons.TimerPicker;
            ModeButtonsUISetter(this.AppData.ActualModeButtonType);
        }

        // UI
        private void ModeButtonsUISetter(ModeButtons modeButton)
        {
            switch (modeButton)
            {
                case ModeButtons.TimerPicker:
                    this.btnTimerPicker.Background = new SolidColorBrush(Colors.Orange);
                    this.btnDatePicker.Background = SystemColors.ControlLightBrush;
                    this.AppData.ModeButtonsVisibilitySetter(ModeButtons.TimerPicker);
                    break;
                case ModeButtons.DatePicker:
                    this.btnDatePicker.Background = new SolidColorBrush(Colors.Orange);
                    this.btnTimerPicker.Background = SystemColors.ControlLightBrush;
                    this.AppData.ModeButtonsVisibilitySetter(ModeButtons.DatePicker);
                    break;
            }
        }
        private void OperationButtonsUISetter(OperationButtons operationButton)
        {
            switch (operationButton)
            {
                case OperationButtons.Start:
                    this.btnPlay.Background = new SolidColorBrush(Colors.LightGreen);
                    this.btnPause.Background = SystemColors.ControlLightBrush;
                    this.btnResume.Background = SystemColors.ControlLightBrush;
                    break;
                case OperationButtons.Pause:
                    this.btnPause.Background = new SolidColorBrush(Colors.Orange);
                    this.btnPlay.Background = SystemColors.ControlLightBrush;
                    this.btnResume.Background = SystemColors.ControlLightBrush;
                    break;
                case OperationButtons.Resume:
                    this.btnResume.Background = new SolidColorBrush(Colors.Yellow);
                    this.btnPlay.Background = SystemColors.ControlLightBrush;
                    this.btnPause.Background = SystemColors.ControlLightBrush;
                    break;
                default:
                    break;
            }
        }
    }
}
