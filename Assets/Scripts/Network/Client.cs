using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;

public class Client {
    public static Client instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Client();
            }
            return _instance;
        }
    }
    static Client _instance;


    public void Connect(string address = "127.0.0.1")
	{
        Debug.Log("connect start");
        client.Connect(IPAddress.Parse(address), NetworkConsts.port);
        Debug.Log("Client connected!");

	}

    public void SendMessage()
    {
        byte[] message = System.Text.Encoding.Unicode.GetBytes("The Message");
        client.GetStream().Write(message, 0, message.Length);
    }

    public void Close()
    {
        Debug.Log("close client");
        client.Close();
    }

    TcpClient client;
    NetworkStream stream;
    public Client()
    {
        client = new TcpClient();
    }
}
