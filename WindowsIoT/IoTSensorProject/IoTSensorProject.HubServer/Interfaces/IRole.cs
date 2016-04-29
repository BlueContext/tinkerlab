﻿using Windows.Devices.Radios.nRF24L01P.Interfaces;

namespace IoTSensorProject.HubServer.Interfaces
{
    public interface IRole
    {
        void AttachRadio(IRadio radio);
        void DetachRadio();
        bool Start();
        void Stop();
    }
}
