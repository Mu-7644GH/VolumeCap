using NAudio.CoreAudioApi;
using VolumeCap.Views;

namespace VolumeCap.Volume
{
    public class VolumeControl
    {
        MMDeviceEnumerator mme = new MMDeviceEnumerator();
        MMNotificationClient mmnc = new MMNotificationClient();
        public MMDevice mmDevice;

        public VolumeControl()
        {
            mmDevice = mme.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            mme.RegisterEndpointNotificationCallback(mmnc);
            mmDevice.AudioEndpointVolume.OnVolumeNotification += AudioEndpointVolume_OnVolumeNotification;
        }

        public void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data)
        {
            int nwvol = (int)(mmDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
            int mxvol = MainWindow.s_maxVolume;
            bool ia = MainWindow.s_isApplied;

            if ((nwvol > mxvol) && ia)
            {
                mmDevice.AudioEndpointVolume.MasterVolumeLevelScalar = ((float)mxvol) / 100;
            }

        }
    }
}
