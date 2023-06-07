#include <WiFi.h>

const char* ssid = "YourWiFiNetworkName";
const char* password = "YourWiFiNetworkPassword";
WiFiServer server(1234);
int buttonPin = 2;    // Pin number connected to the button
int ledPin = 13;      // Pin number connected to the LED

void setup() {
  Serial.begin(115200);
  pinMode(buttonPin, INPUT_PULLUP);
  pinMode(ledPin, OUTPUT);
  
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.println("Connecting to WiFi...");
  }
  Serial.println("Connected to WiFi");
  
  server.begin();
}

void loop() {
  WiFiClient client = server.available();
  if (client) {
    while (client.connected()) {
      if (client.available()) {
        String message = client.readStringUntil('\n');
        Serial.println("Received: " + message);
        
        if (message.equals("activate")) {
          digitalWrite(ledPin, HIGH);  // Turn on the LED
          simulateButtonPress();       // Simulate button press
        }
      }
    }
    client.stop();
  }
}

void simulateButtonPress() {
  // Simulate button press by briefly pressing and releasing the button
  digitalWrite(buttonPin, HIGH);
  delay(100);
  digitalWrite(buttonPin, LOW);
  delay(100);
}
