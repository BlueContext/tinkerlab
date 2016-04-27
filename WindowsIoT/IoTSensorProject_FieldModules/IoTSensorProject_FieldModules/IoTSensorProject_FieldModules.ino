/*
 Name:		IoTSensorProject_FieldModules.ino
 Created:	4/27/2016 12:15:46 PM
 Author:	Kevin
*/


#include <Sync.h>
#include <RF24Network_config.h>
#include <RF24Network.h>
#include <RF24_config.h>
#include <RF24.h>
#include <printf.h>
#include <nRF24L01.h>
#include <SPI.h>
#include <Wire.h>
#include <Adafruit_BME280.h>
#include <Adafruit_Sensor.h>

#define SEALEVELPRESSURE_HPA (1017.03)

Adafruit_BME280 bme;
RF24 radio(7, 8);
RF24Network network(radio);

const uint16_t local_node = 01;
const uint16_t remote_hub = 00;

const unsigned long interval = 5000; //ms 

unsigned long last_sent;
unsigned long packets_sent;

struct payload_t {
	unsigned long ms;
	unsigned long counter;
};


void setup()
{

	Serial.begin(57600);
	Serial.println(F("BME280 test"));

	if (!bme.begin()) {
		Serial.println("Could not find a valid BME280 sensor, check wiring!");
		while (1);
	}

	Serial.println("Starting RF24Network test...");
	SPI.begin();
	Serial.println("SPI.begin() complete...");
	radio.begin();
	Serial.println("radio.begin() complete...");
	network.begin(90, local_node);  //(channel, node address)
	Serial.println("Network.begin() complete!");


}

void loop()
{
	Serial.println("Starting network.update()...");
	network.update();
	Serial.println("network.update() complete.");

	unsigned long now = millis();
	Serial.println("The value of now is " + now);
	if (now - last_sent >= interval)
	{
		last_sent = now;

		Serial.print("Sending data...");
		payload_t payload = { millis(), packets_sent++ };
		RF24NetworkHeader header(remote_hub);
		bool ok = network.write(header, &payload, sizeof(payload));
		if (ok)
		{
			Serial.println("OK.");
		}
		else
		{
			Serial.println("Failed.");
		}
	}

	Serial.print("Temperature = ");
	Serial.print(bme.readTemperature());
	Serial.println(" *C");
	Serial.print((bme.readTemperature() * 1.8) + 32);
	Serial.println(" *F");
	Serial.print("Pressure = ");

	Serial.print(bme.readPressure() / 100.0F);
	Serial.println(" hPa");

	Serial.print("Humidity = ");
	Serial.print(bme.readHumidity());
	Serial.println(" %");

	Serial.println();
	delay(10000);



}

