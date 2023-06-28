# Using the HoloLens 2 to communicate with Arduino MKR WiFi 1010
This section is dedicated to explaining communication between the HoloLens 2 and a WiFi-enabled Arduino Module. Using the HoloLens 2, we can interact with Augmented Reality(AR)-elements that trigger real-world events by sending a WiFi signal to the Arduino. The Arduino can then send out an electronic analog signal which can be used to interact with a diverse array of electronic systems and components.

## Tools
Navigating to the **Assets --> Arduino** folder, we find all the scripts and sketches we need that allow the HoloLens 2 to communicate with the Arduino:
- ArduinoCommunication script
- ipaddress sketch
- secrets
- sketch_jun5a

### ArduinoCommunication Script
This code is responsible for establishing communication between a Unity application and an Arduino device over a TCP/IP connection. It allows the Unity application to send signals or commands to the Arduino for various purposes.

The code utilizes the **TcpClient** class from the **System.Net.Sockets** namespace to establish a TCP/IP connection with the Arduino. It relies on the IP address and port number to connect to the Arduino device. The IP address should be set to the current IP address of the Arduino(**details below**), which might change daily. The port number is the specific port on which the Arduino is listening for incoming connections.

The **SendSignal** method is called when the Unity application wants to send a signal to the Arduino. It first checks if the connection with the Arduino is established (**isConnected**). If not, it asynchronously calls the **ConnectToArduino** method to establish the connection.

If the connection is successful, the method writes the string "activate" to the **StreamWriter** instance, which is responsible for sending data over the network. The **Flush** method ensures that the data is sent immediately.

In case of any errors during the connection or signal sending process, appropriate error messages are logged and displayed using the **UserAlert** script attached to the **alertObject** game object. The **displayMessage** method of the **UserAlert** script is called to show the error message to the user.

The **ConnectToArduino** method attempts to connect to the Arduino by creating a new **TcpClient** instance and calling the **ConnectAsync** method with the Arduino's IP address and port number. If the connection is successful, a **NetworkStream** is obtained from the client, and a **StreamWriter** is created to write data to the stream. The **isConnected** flag is set to true to indicate a successful connection.

The **OnDestroy** method is responsible for closing the writer and client connection when the Unity application is being destroyed to ensure proper cleanup.

**IMPORTANT**
Note that the IP address of the Arduino will change each time it is disconnected from a power source, or each day it re-connects to the WiFi network. As such, you will need to upload the **ipaddress** sketch to the Arduino and get the IP address from the Serial Output in the Arduino IDE. Once you have the new IP address, simply change the address in the **ipAddress** field.

### ipAddress sketch
