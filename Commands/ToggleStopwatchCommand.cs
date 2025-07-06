using OutfitTool.Common;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Timer.Commands
{
    class ToggleStopwatchCommand: CommandInterface
    {
        public string Name => "ShowHideStopwatch";
        public string Description => LocalizationHelper.GetString("Stopwatch");
        public CommandContextMenu? ContextMenu => getContextMenu();

        public CommandContextMenu? getContextMenu()
        {
            string resourceName = "stopwatch.png";
            return new CommandContextMenu(
                LocalizationHelper.GetString("Stopwatch"),
                new BitmapImage(
                    new Uri("pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/Resources/" + resourceName)
                    )
                );
        }

        public void run()
        {
            if (ModuleController.timerForm == null)
            {
                return;
            }

            if (ModuleController.timerForm.Visibility == Visibility.Visible)
            {
                if (ModuleController.timerForm.timerTabs.SelectedIndex == 1)
                {
                    ModuleController.timerForm.timerTabs.SelectedIndex = 0;
                }
                else
                {
                    ModuleController.timerForm.Hide();
                    ModuleController.timerForm.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                ModuleController.timerForm.Topmost = true;
                ModuleController.timerForm.Show();
                ModuleController.timerForm.Activate();
                ModuleController.timerForm.timerTabs.SelectedIndex = 0;
                ModuleController.timerForm.Focus();
            }
        }
    }
}
