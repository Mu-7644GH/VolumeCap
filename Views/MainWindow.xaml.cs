using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace VolumeCap.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int s_maxVolume = Properties.Settings.Default.lastMaxVolumeAS;
        public static bool s_isApplied = Properties.Settings.Default.isAppliedAS;
        public static Boolean s_isLocked;

        public MainWindow()
        {
            InitializeComponent();

            //loading saved values from settings 
            isAppliedCB.IsChecked = s_isApplied = Properties.Settings.Default.isAppliedAS;
            if (s_isApplied) { App.vc.mmDevice.AudioEndpointVolume.MasterVolumeLevelScalar = ((float)s_maxVolume) / 100; }
            maxVolumeSlider.Value = Properties.Settings.Default.lastMaxVolumeAS;

            versionInfoTB.Text = "VolumeCap V." + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

            //Start minimized and hidden
            this.StateChanged += winStateChanged;
            this.WindowState = WindowState.Minimized;
            this.Visibility = Visibility.Hidden;

            IsLockedCheck();
        }

        private void maxVolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s_maxVolume = (int)maxVolumeSlider.Value;
            if ((bool)isAppliedCB.IsChecked)
            {
                App.vc.mmDevice.AudioEndpointVolume.MasterVolumeLevelScalar = ((float)s_maxVolume) / 100;
            }
        }
        private void isAppliedCB_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)isAppliedCB.IsChecked)
            {
                App.vc.mmDevice.AudioEndpointVolume.MasterVolumeLevelScalar = ((float)s_maxVolume) / 100;
            }
            Properties.Settings.Default.isAppliedAS = s_isApplied = (bool)isAppliedCB.IsChecked;

            App.sysTray.icon.ToolTipText = MainWindow.s_isApplied ? $"Volume capped at:\n {MainWindow.s_maxVolume}." : "Not Active";
        }

        private void lockUnlockButton_Click(object sender, RoutedEventArgs e)
        {
            var pwd = Properties.Settings.Default.passwordAS;

            if (s_isLocked)
            {
                if (mainLockUnlockPassBox.Password != pwd)
                {
                    loginPassStatus.Text = "Worng password!";
                }
                else
                {
                    s_isLocked = false;
                    unlockAll();
                    loginPassStatus.Text = "Unlockd!";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(mainLockUnlockPassBox.Password))
                {
                    loginPassStatus.Text = "Enter password to lock!";
                }
                else if (mainLockUnlockPassBox.Password != pwd)
                {
                    loginPassStatus.Text = "wrong password, try again!";
                }
                else if (mainLockUnlockPassBox.Password == pwd)
                {
                    s_isLocked = true;
                    loginPassStatus.Text = "Locked!";
                    lockAll();
                }
            }
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow setWin = new SettingsWindow();
            setWin.ShowDialog();
            setWin.Owner = this;
        }

        public void IsLockedCheck()
        {
            if (s_isLocked)
                lockAll();
            else
                unlockAll();
        }

        public void unlockAll()
        {
            maxVolumeSlider.IsEnabled = true;
            isAppliedCB.IsEnabled = true;
            lockUnlockButton.Content = "lock";
            mainLockUnlockPassBox.Clear();
            settingsButton.IsEnabled = true;
            Properties.Settings.Default.isLockedAS = s_isLocked = false;
        }

        public void lockAll()
        {
            maxVolumeSlider.IsEnabled = false;
            isAppliedCB.IsEnabled = false;
            lockUnlockButton.Content = "unlock";
            mainLockUnlockPassBox.Clear();
            settingsButton.IsEnabled = false;
            Properties.Settings.Default.isLockedAS = s_isLocked = true;
        }

        public void winStateChanged(object sender, System.EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Visibility = Visibility.Collapsed;
            }
            else
            {

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (s_isLocked)
            {
                e.Cancel = true;
            }

            Properties.Settings.Default.lastMaxVolumeAS = s_maxVolume;
            Properties.Settings.Default.Save();
        }


        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Reset();
        }

    }
}
