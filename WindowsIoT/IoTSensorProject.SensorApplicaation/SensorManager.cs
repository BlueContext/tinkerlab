using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace IoTSensorProject.SensorApplicaation
{
    public class SensorManager
    {
        BME280Sensor BME280;

        protected async void  GetSensorData()
        {
            try
            {
                BME280 = new BME280Sensor();
                await BME280.Initialize();


                float temp = 0;
                float tempf = 0;
                float pressure = 0;
                float pressureHg = 0;
                float altitude = 0;
                float humidity = 0;

                //Create constant pressure for sea level based on your local sea level pressure in Hectopascals

                const float seaLevelPressure = 989.70f;

                //Read 10 samples of data

                for (int i = 0; i < 10; i++)
                {
                    temp = await BME280.ReadTemperature();
                    tempf = (float)(temp * 1.8) + 32;
                    pressure = await BME280.ReadPreasure();
                    pressureHg = (float)(0.0002952998751 * pressure);
                    altitude = await BME280.ReadAltitude(seaLevelPressure);
                    humidity = await BME280.ReadHumidity();

                    Debug.WriteLine("Temperature (C): " + temp.ToString() + " deg C");
                    Debug.WriteLine("Temperature (F): " + tempf.ToString() + "deg F");
                    Debug.WriteLine("Humidity: " + humidity.ToString() + " %");
                    Debug.WriteLine("Pressure (Pascals): " + pressure.ToString() + " Pa");
                    Debug.WriteLine("Pressure (Inches Hg): " + pressureHg.ToString() + " inHg");
                    Debug.WriteLine("Altitude: " + altitude.ToString() + " m");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
