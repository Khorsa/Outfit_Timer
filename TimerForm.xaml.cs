using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Timer
{
    public partial class TimerForm : Window
    {
        private const int STOPWATCH_INTERVAL = 10;
        private const int TIMER_INTERVAL = 100;

        private DispatcherTimer stopWatchTimer = new DispatcherTimer();
        private List<DateTime> cutoffs = new List<DateTime>();

        public TimerForm()
        {
            stopWatchTimer.Interval = TimeSpan.FromMilliseconds(STOPWATCH_INTERVAL);
            stopWatchTimer.Tick += StopWatchTimer_Tick;

            timerTimer.Interval = TimeSpan.FromMilliseconds(TIMER_INTERVAL);
            timerTimer.Tick += TimerTimer_Tick;

            LocalizationHelper.LanguageChanged += LanguageChanged;
            InitializeComponent();
        }

        private void StopWatchTimer_Tick(object? sender, EventArgs e)
        {
            string text = "";
            DateTime first = cutoffs.FirstOrDefault(DateTime.Now);
            foreach (DateTime dt in cutoffs.GetRange(1, cutoffs.Count - 1))
            {
                text += FormatTimerOutput(dt - first) + "\n";
            }
            text += FormatTimerOutput(DateTime.Now - first);
            TimerText.Text = text;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (stopWatchTimer.IsEnabled)
            {
                cutoffs.Add(DateTime.Now);
            }
            else {
                cutoffs.Clear();
                cutoffs.Add(DateTime.Now);
                stopWatchTimer.Start();
                StopButton.IsEnabled = true;
                SetStartButtonText("Cutoff");
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StopButton.IsEnabled = false;
            SetStartButtonText("Start");
            stopWatchTimer.Stop();
        }


        private void SetStartButtonText(string TextKey)
        {
            StartButton.Content = LocalizationHelper.GetString(TextKey);
        }



        private string FormatTimerOutput(TimeSpan dateTime)
        {
            string text = "";
            if (dateTime.Days != 0)
            {
                text += dateTime.Days + ":";
            }
            if (dateTime.Hours != 0)
            {
                text += dateTime.Hours.ToString("D2") + ":";
            }
            text += dateTime.Minutes.ToString("D2") + ":" + dateTime.Seconds.ToString("D2") + "." + dateTime.Milliseconds.ToString("D3");

            return text;
        }



        private void LanguageChanged(Object? sender, EventArgs e)
        {
            ResourceDictionary oldDict = (from d in this.Resources.MergedDictionaries
                                          where d.Source != null && d.Source.OriginalString.Contains("Languages/lang.")
                                          select d).First();
            if (oldDict != null)
            {
                int ind = this.Resources.MergedDictionaries.IndexOf(oldDict);
                this.Resources.MergedDictionaries.Remove(oldDict);
                this.Resources.MergedDictionaries.Insert(ind, LocalizationHelper.CurrentDictionary);
            }
            else
            {
                this.Resources.MergedDictionaries.Add(LocalizationHelper.CurrentDictionary);
            }
            if (stopWatchTimer.IsEnabled) {
                SetStartButtonText("Cutoff");
            } else {
                SetStartButtonText("Start");
            }
        }


        private DispatcherTimer timerTimer = new DispatcherTimer();
        private TimeSpan _currentTimer = TimeSpan.Zero;
        private TimeSpan CurrentTimer {
            set
            {
                _currentTimer = value;
                TimerTextBlock.Text = (value.Days * 24 + value.Hours).ToString("D2") + ":" + value.Minutes.ToString("D2") + ":" + value.Seconds.ToString("D2") + "." + (value.Milliseconds / 100).ToString("D1");
            }
            get
            {
                return _currentTimer;
            }
        }

        private void TimerButtons_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                string tag = (sender as Button).Tag.ToString();
                if (tag == "Reset")
                {
                    CurrentTimer = TimeSpan.Zero;
                    return;
                }
                int plusSeconds = int.Parse(tag);
                CurrentTimer += new TimeSpan(0,0,plusSeconds);
            }
        }

        private void TimerStart_Click(object sender, RoutedEventArgs e)
        {
            timerTimer.Start();
        }

        private void TimerTimer_Tick(object? sender, EventArgs e)
        {
            CurrentTimer -= new TimeSpan(0,0,0,0, TIMER_INTERVAL);
            if (CurrentTimer < TimeSpan.Zero)
            {
                timerTimer.Stop();
                CurrentTimer = TimeSpan.Zero;

                var beep = new Beep();
                beep.Sound();
            }
        }


    }
}
