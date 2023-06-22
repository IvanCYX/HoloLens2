#include <WiFi.h>

const char* ssid = "YourWiFiNetworkName";
const char* password = "YourWiFiNetworkPassword";
WiFiServer server(1234);
int ledPin = 13;      // Pin number connected to the LED

void setup() {
  Serial.begin(115200);

  pinMode(ledPin, OUTPUT);
  digitalWrite(ledPin, LOW);  // Initially set the LED pin to LOW (0V)

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
          delay(1000);  // Keep the LED on for 1 second
          digitalWrite(ledPin, LOW);  // Turn off the LED
        }
      }
    }
    client.stop();
  }
}
