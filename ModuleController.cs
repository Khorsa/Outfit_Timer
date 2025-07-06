using Timer.Commands;
using OutfitTool.Common;
using System.Globalization;
using System.Windows.Media.Imaging;

namespace Timer
{
    internal class ModuleController : ModuleControllerInterface
    {
        public static TimerForm timerForm = new TimerForm();

        public ModuleController() 
        {
        }

        public List<CommandInterface> getCommandList()
        {
            return new List<CommandInterface>([
                new ToggleStopwatchCommand(),
                new ToggleTimerCommand()
            ]);
        }

        public void init()
        {
            timerForm = new TimerForm();
        }

        public void setLanguage(string language)
        {
            LocalizationHelper.Language = new CultureInfo(language);
        }

        public void shutdown()
        {
            if (timerForm != null)
            {
                timerForm.Close(); 
            }
        }

        public BitmapImage? getTaskbarIcon()
        {
            return null;
        }
        public string? getTaskbarIconText()
        {
            return null;
        }

        public Notification? popNotification()
        {
            return null;
        }

        public void openSettings()
        {
        }

    }
}
