using Microsoft.Win32;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using VolumeCap.Views;
using VolumeCap.Volume;


namespace VolumeCap
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static System.Threading.Mutex singleton = new Mutex(true, "VolumeCap");
        public static Regex passwordRegex = new Regex("[^a-zA-Z0-9]"); //Regex for password

        public static RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        public static SystemTray sysTray = new SystemTray();
        public static VolumeControl vc = new VolumeControl();

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            if (!singleton.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("Already open!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.MainWindow.Close();
            }
        }
    }
}
