using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VolumeCap.Views
{
    public class SystemTray
    {
        public Hardcodet.Wpf.TaskbarNotification.TaskbarIcon icon = new Hardcodet.Wpf.TaskbarNotification.TaskbarIcon();
        ContextMenu trayIconContextMenu = new ContextMenu();
        MenuItem closeCM = new MenuItem();

        public SystemTray()
        {
            //load
            icon.Icon = Properties.Resources.Icon;
            icon.ToolTipText = MainWindow.s_isApplied ? $"Volume capped at:\n {MainWindow.s_maxVolume}." : "Not Active";
            icon.TrayMouseDoubleClick += Sti_TrayMouseDoubleClick;

            //ContextMenue
            icon.ContextMenu = trayIconContextMenu;
            trayIconContextMenu.Closed += TrayIconContextMenu_Closed;
            trayIconContextMenu.Items.Add(closeCM);
            closeCM.Header = "Close";
            closeCM.Click += CloseCM_Click;


        }



        private void TrayIconContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            closeCM.Foreground = Brushes.Black;
        }

        private void CloseCM_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.s_isLocked)
            {
                closeCM.Foreground = Brushes.Red;
            }
            else
            {
                Application.Current.MainWindow.Close();
            }

        }

        private void Sti_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
            {
                Application.Current.MainWindow.Visibility = Visibility.Visible;
                Application.Current.MainWindow.WindowState = WindowState.Normal;
                SystemCommands.RestoreWindow(Application.Current.MainWindow);
            };
        }
    }
}
