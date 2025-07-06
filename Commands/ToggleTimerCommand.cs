using OutfitTool.Common;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Timer.Commands
{
    class ToggleTimerCommand: CommandInterface
    {
        public string Name => "ShowHideTimer";
        public string Description => LocalizationHelper.GetString("Timer");
        public CommandContextMenu? ContextMenu => getContextMenu();

        public CommandContextMenu? getContextMenu()
        {
            string resourceName = "timer.png";
            return new CommandContextMenu(
                LocalizationHelper.GetString("Timer"),
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
                if (ModuleController.timerForm.timerTabs.SelectedIndex == 0)
                {
                    ModuleController.timerForm.timerTabs.SelectedIndex = 1;
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
                ModuleController.timerForm.timerTabs.SelectedIndex = 1;
                ModuleController.timerForm.Focus();
            }
        }
    }
}
