using System.Media;
using System.Windows;

namespace Timer
{
    internal class Beep
    {
        public void Sound()
        {
            var uri = new Uri(String.Format("pack://application:,,,/Timer;component/Resources/clock_alarm.wav", LocalizationHelper.Language), UriKind.Absolute);
            System.Windows.Resources.StreamResourceInfo res =Application.GetResourceStream(uri);
            SoundPlayer sndPing = new SoundPlayer(res.Stream);
            sndPing.Play();
        }
    }
}
