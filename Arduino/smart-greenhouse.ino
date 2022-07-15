
#include "DHT.h"

#define DHTPIN 3 
#define DHTTYPE DHT11
 
#define INPT 2

DHT dht(DHTPIN, DHTTYPE, 6);
void setup() {
  pinMode(INPT,INPUT);
  pinMode(9,OUTPUT);
  Serial.begin(9600); //initialise serial monitor
  dht.begin();
}

void loop() {
  unsigned int AnalogValue;
  
  AnalogValue = analogRead(A0);
  //int light = map(AnalogValue, 0, 1023, 0, 100);
  Serial.print("Intenzitet: ");
  Serial.print(AnalogValue);
  Serial.println();
   ///
  int temp=digitalRead(INPT);      //assign value of LDR sensor to a temporary variable
  Serial.print("Dan/noc:"); //print on serial monitor using ""
  Serial.print(temp);         //display output on serial monitor
  Serial.println();
  
  float h = dht.readHumidity();
  // Read temperature as Celsius
  float t = dht.readTemperature();
  // Read temperature as Fahrenheit
  float f = dht.readTemperature(true);
  
  // Check if any reads failed and exit early (to try again).
  if (isnan(h) || isnan(t) || isnan(f)) {
    Serial.println("Failed to read from DHT sensor!");
    return;
  }

  // Compute heat index
  // Must send in temp in Fahrenheit!
  float hi = dht.computeHeatIndex(f, h);

  Serial.print("Vlaznostttt zraka: "); 
  Serial.print(h);
  Serial.println();
  
  Serial.print("Temperatura: "); 
  Serial.print(t);
  Serial.print("C - ");
  Serial.print(f);
  Serial.print("F"); 
  Serial.println(); 
  delay(3000);
}
