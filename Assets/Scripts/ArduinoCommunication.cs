using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class ArduinoCommunication : MonoBehaviour
{
    [SerializeField] private Button sendButton;
    private const string ipAddress = "192.168.1.100"; // Replace with Arduino IP address
    private const int port = 1234;

    private TcpClient client;
    private StreamWriter writer;
    private bool isConnected = false;

    private void Start()
    {
        sendButton.onClick.AddListener(SendSignal);
    }

    private async void SendSignal()
    {
        if (!isConnected)
        {
            await ConnectToArduino();
        }

        if (isConnected)
        {
            try
            {
                writer.WriteLine("activate");
                writer.Flush();
            }
            catch (Exception e)
            {
                Debug.LogError("Error sending signal: " + e.Message);
            }
        }
    }

    private async Task ConnectToArduino()
    {
        try
        {
            client = new TcpClient();
            //Connect to Arduino
            await client.ConnectAsync(ipAddress, port);
            NetworkStream stream = client.GetStream();
            writer = new StreamWriter(stream, Encoding.ASCII);
            isConnected = true;
            Debug.Log("Connected to Arduino");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to connect to Arduino: " + e.Message);
        }
    }

    private void OnDestroy()
    {
        if (isConnected)
        {
            //Close writer and client connection
            writer.Close();
            client.Close();
        }
    }
}
