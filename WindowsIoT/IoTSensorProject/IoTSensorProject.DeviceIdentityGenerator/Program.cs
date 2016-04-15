using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace IoTSensorProject.DeviceIdentityGenerator
{
    class Program
    {
        static RegistryManager registryManager;
        static string connectionString = "HostName=IoTSensorProject.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=pizN83d1vnaYvE4vKmCz2M5vpT7eX+YhjS0E4B7Z1fg=";
        static void Main(string[] args)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            AddDeviceAsync().Wait();
            Console.ReadLine();
        }

        private static async Task AddDeviceAsync()
        {
            string deviceId = "RASPI1";
            Device device;

            try
            {
                device = await registryManager.AddDeviceAsync(new Device(deviceId));
            }
            catch (DeviceAlreadyExistsException)
            {
                device = await registryManager.GetDeviceAsync(deviceId);
            }

            Console.WriteLine("Generated device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
        }
    }
}
