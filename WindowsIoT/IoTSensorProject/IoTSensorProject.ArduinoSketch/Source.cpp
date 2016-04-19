#include <SPI.h>
#include <Wire.h>
#include <Adafruit_BME280.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_TCS34725.h>

#define BME_SCK 13
#define BME_MISO 12
#define BME_MOSI 11
#define BME_CS 10

#define TCS_SDA 9
#define TCS_SCL 8
#define TCS_LED 4

#define SEALEVELPRESSURE_HPA (1017.03)

Adafruit_BME280 bme(BME_CS, BME_MOSI, BME_MISO, BME_SCK);
Adafruit_TCS34725 tcs = Adafruit_TCS34725(TCS34725_INTEGRATIONTIME_700MS, TCS34725_GAIN_1X);


void setup() {
	Serial.begin(9600);
	Serial.println(F("BME280 test"));

	if (!bme.begin()) {
		Serial.println("Could not find a valid BME280 sensor, check wiring!");
		while (1);
	}

	if (tcs.begin())
	{
		Serial.println("Found TCS34725 sensor.");
	}
	else
	{
		Serial.println("No TCS34725 sensor was found, check wiring!");
		while (1);
	}
	pinMode(TCS_LED, OUTPUT);
	digitalWrite(TCS_LED, 0);
}

void loop() {
	uint16_t r, g, b, c, colorTemp, lux;
	//uint16_t clear, red, green, blue;
	// tcs.setInterrupt(false);
	delay(60);
	tcs.getRawData(&r, &g, &b, &c);
	// tcs.setInterrupt(true);
	colorTemp = tcs.calculateColorTemperature(r, g, b);
	lux = tcs.calculateLux(r, g, b);

	Serial.print("Color Temp: "); Serial.print(colorTemp, DEC); Serial.println(" K");
	Serial.print("Lux: "); Serial.println(lux, DEC); //Serial.print(" - ");
	Serial.print("R: "); Serial.print(r, DEC); Serial.println(" ");
	Serial.print("G: "); Serial.print(g, DEC); Serial.println(" ");
	Serial.print("B: "); Serial.print(b, DEC); Serial.println(" ");
	Serial.print("C: "); Serial.print(c, DEC); Serial.println(" ");
	Serial.println(" ");


	Serial.print("Temperature = ");
	Serial.print(bme.readTemperature());
	Serial.println(" *C");
	Serial.print((bme.readTemperature() * 1.8) + 32);
	Serial.println(" *F");
	Serial.print("Pressure = ");

	Serial.print(bme.readPressure() / 100.0F);
	Serial.println(" hPa");

	//Serial.print("Approx. Altitude = ");
	//Serial.print(bme.readAltitude(SEALEVELPRESSURE_HPA));
	//Serial.println(" m");

	Serial.print("Humidity = ");
	Serial.print(bme.readHumidity());
	Serial.println(" %");

	Serial.println();
	delay(2000);
}