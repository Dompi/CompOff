using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CompOffUIWPF
{
    public class AppData
    {
        public ModeButtons ActualModeButtonType { get; set; }

        public ObservableObject<Brush> BackgroundColor { get; set; }
        public ObservableObject<Brush> ForegroundColor { get; set; }
        public ObservableObject<Visibility> DateVisibility { get; set; }
        public ObservableObject<Visibility> TimerVisibility { get; set; }
        public ObservableObject<DateTime> SelectedDate { get; set; }

        public void ModeButtonsVisibilitySetter(ModeButtons modeButton)
        {
            switch (modeButton)
            {
                case ModeButtons.TimerPicker:
                    this.TimerVisibility.Value = Visibility.Visible;
                    this.DateVisibility.Value = Visibility.Collapsed;
                    break;
                case ModeButtons.DatePicker:
                    this.TimerVisibility.Value = Visibility.Collapsed;
                    this.DateVisibility.Value = Visibility.Visible;
                    break;
            }
        }
    }
}
