#include <Arduino.h>

const int PUMP_PIN = 5;
const int lightSensorPin = A2;
const int moistSensorPin = A3;

void ActivateWaterPump(int millisecondsToPump)
{
  digitalWrite(PUMP_PIN, HIGH);
  delay(3000);
  digitalWrite(PUMP_PIN, LOW);
  delay(1000);
}

double GetHumidityPrecent()
{
  double humidityValue = analogRead(moistSensorPin);
  double humidityProcent = (100-(humidityValue/1023.00)*100);
  delay(1000);
  return humidityProcent;
}

int GetLightValue(){
  int lightValue = analogRead(lightSensorPin);
  return lightValue;
}

void setup() {
  Serial.begin(9600);
  pinMode(PUMP_PIN, OUTPUT);
  pinMode(moistSensorPin, INPUT);
  pinMode(lightSensorPin, INPUT);
}
 
void loop() {
  double humidityProcent = GetHumidityPrecent();
  int lightValue = GetLightValue();
  
  Serial.print("humidity:");
  Serial.print(humidityProcent);
  Serial.print(",");
  Serial.print("lightValue:");
  Serial.println(lightValue);

  delay(1000);
}