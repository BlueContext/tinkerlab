﻿using System;
using Windows.Devices.Radios.nRF24L01P;
using Windows.Devices.Radios.nRF24L01P.Enums;

namespace IoTSensorProject.HubServer.Roles
{
    public class ReceiverRole : BaseRole
    {
        public event EventHandler<byte[]> DataArrived;

        public override bool Start()
        {
            if (!base.Start()) return false;

            Reader = Radio.ReceivePipes[1];
            Reader.AutoAcknowledgementEnabled = true;
            Reader.DynamicPayloadLengthEnabled = true;
            Reader.Address = ReceiveAddress;
            Reader.PayloadWidth = Radio.Configuration.PayloadWidth;
            Reader.Enabled = true;

            Reader.FlushBuffer();
            Radio.TransmitPipe.FlushBuffer();

            Radio.Interrupted += Radio_Interrupted;
            Radio.OperatingMode = OperatingModes.ReceiveMode;

            return IsRunning = true;
        }

        protected override void Radio_Interrupted(object sender, InterruptedEventArgs e)
        {
            if (!e.StatusRegister.ReceiveDataReady) return;
            base.Radio_Interrupted(sender, e);

            DataArrived?.Invoke(this, Reader.ReadBufferAll());
            lock (SyncRoot)
            {
                Radio.OperatingMode = OperatingModes.StandBy;
                e.StatusRegister.ResetToDefault();
                e.StatusRegister.Save();
                Radio.OperatingMode = OperatingModes.ReceiveMode;
            }
        }
    }
}
