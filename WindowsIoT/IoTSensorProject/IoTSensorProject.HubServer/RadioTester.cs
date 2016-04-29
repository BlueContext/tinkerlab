using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices;
using Windows.Devices.Spi;
using Windows.Devices.Radios.nRF24L01P;
using IoTSensorProject.HubServer.Roles;

namespace IoTSensorProject.HubServer
{
    public class RadioTester
    {
        public SpiDevice spi = new SpiDevice();
        private Radio myRadio;
        public ReceiverRole rec;

        public void RadioTester()
        {
            myRadio = new Radio()
            rec.AttachRadio(myRadio);
            bool running = rec.Start();
            rec.DataArrived +=             
        }


        //Methods

    }
}
