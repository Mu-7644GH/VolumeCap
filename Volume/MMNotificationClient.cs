using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;
using System;
using VolumeCap.Views;

namespace VolumeCap.Volume
{
    class MMNotificationClient : IMMNotificationClient
    {
        public void OnDefaultDeviceChanged(DataFlow flow, Role role, string defaultDeviceId)
        {
            App.vc.mmDevice = new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            App.vc.mmDevice.AudioEndpointVolume.OnVolumeNotification += App.vc.AudioEndpointVolume_OnVolumeNotification;
            App.vc.mmDevice.AudioEndpointVolume.MasterVolumeLevelScalar = ((float)MainWindow.s_maxVolume) / 100;
        }

        public void OnDeviceAdded(string pwstrDeviceId)
        {
            throw new NotImplementedException();
        }

        public void OnDeviceRemoved(string deviceId)
        {
            throw new NotImplementedException();
        }

        public void OnDeviceStateChanged(string deviceId, DeviceState newState)
        {
            throw new NotImplementedException();
        }

        public void OnPropertyValueChanged(string pwstrDeviceId, PropertyKey key)
        {
            throw new NotImplementedException();
        }
    }
}
