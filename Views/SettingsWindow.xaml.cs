using System.Windows;
using System.Windows.Media;


namespace VolumeCap.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            //loaed view
            startOnStartupCB.IsChecked = Properties.Settings.Default.startOnStartupAS;
            currentPassPassBox.IsEnabled = !string.IsNullOrEmpty(Properties.Settings.Default.passwordAS);
            if (string.IsNullOrEmpty(Properties.Settings.Default.passwordAS)) currentPassPassBox.Background = Brushes.DarkGray;

        }

        private void startOnStartupCB_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)startOnStartupCB.IsChecked)
            {
                if (App.rk.GetValue("VolumeCap") == null)
                {
                    App.rk.SetValue("VolumeCap", System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath + "\\VolumeCap.ExE"));
                }
            }
            else
            {
                App.rk.DeleteValue("VolumeCap", false);
            }

            Properties.Settings.Default.startOnStartupAS = (bool)startOnStartupCB.IsChecked;
            Properties.Settings.Default.Save();
        }

        private void setPasswordButton_Click(object sender, RoutedEventArgs e)
        {

            if (Properties.Settings.Default.passwordAS == currentPassPassBox.Password || string.IsNullOrEmpty(Properties.Settings.Default.passwordAS))
            {
                if (newPasswordPassBox.Password == repeatPasswordPassBox.Password)
                {
                    if (currentPassPassBox.Password == newPasswordPassBox.Password)
                    {
                        PassSettingStatusTB.Text = "Password unchanged!";
                    }
                    else if (newPasswordPassBox.Password == string.Empty & repeatPasswordPassBox.Password == string.Empty)
                    {
                        PassSettingStatusTB.Text = "Password removed!";
                        Properties.Settings.Default.passwordAS = string.Empty;
                        //Main.passToLock = false;
                        currentPassPassBox.IsEnabled = false;
                        currentPassPassBox.Background = Brushes.DarkGray;
                        Properties.Settings.Default.Save();

                        currentPassPassBox.Clear();
                    }
                    else if ((newPasswordPassBox.Password.Length == repeatPasswordPassBox.Password.Length & newPasswordPassBox.Password.Length >= 6 & !App.passwordRegex.IsMatch(newPasswordPassBox.Password)) & string.IsNullOrEmpty(currentPassPassBox.Password))
                    {
                        Properties.Settings.Default.passwordAS = newPasswordPassBox.Password;
                        Properties.Settings.Default.Save();
                        PassSettingStatusTB.Text = "Password created!";
                        currentPassPassBox.IsEnabled = true;
                        currentPassPassBox.Background = Brushes.White;
                        currentPassPassBox.Clear();
                    }
                    else if (newPasswordPassBox.Password.Length == repeatPasswordPassBox.Password.Length & newPasswordPassBox.Password.Length >= 6 & !App.passwordRegex.IsMatch(newPasswordPassBox.Password))
                    {
                        Properties.Settings.Default.passwordAS = newPasswordPassBox.Password;
                        Properties.Settings.Default.Save();
                        PassSettingStatusTB.Text = "Password changed!";
                        currentPassPassBox.IsEnabled = true;
                        currentPassPassBox.Background = Brushes.White;
                        currentPassPassBox.Clear();
                    }
                    else
                    {
                        PassSettingStatusTB.Text = "Password is not good! \na-z,A-Z,0-9 only !";
                    }
                }
                else
                {
                    PassSettingStatusTB.Text = "Passwords do not match!";
                }
            }
            else
            {
                PassSettingStatusTB.Text = "Current password \nis incorrect!";
            }

        }


    }
}
