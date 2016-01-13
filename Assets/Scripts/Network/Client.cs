using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;

public class Client {
    public delegate void OnMessageReceived(NetworkDecorator.NetworkMessage message);
    public List<OnMessageReceived> onMessageReceived = new List<OnMessageReceived>();

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


    public void Connect(string address = null)
	{
        if (client == null || !client.Connected)
        {
            if (address == null)
            {
                address = NetworkValues.ip;
            }
            client = new TcpClient();
            client.Connect(IPAddress.Parse(address), NetworkValues.port);
            NetworkValues.isNetwork = true;
            Debug.Log("Client connected!");
        }
        else
        {
            Debug.LogAssertion("Already Connected!");
        }

	}

    public void Send(string message = "empty message")
    {
        Debug.Log("SendMessage : " + message);
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("뷁" + message);
        client.GetStream().Write(buffer, 0, buffer.Length);
    }

    public void Update()
    {
        if (!NetworkValues.isNetwork)
        {
            return;
        }
        var stream = client.GetStream();
        if (stream.CanRead && stream.DataAvailable)
        {
            byte[] buffer = new Byte[client.ReceiveBufferSize+1];
            stream.Read(buffer, 0, buffer.Length);
            var message = System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            Debug.Log("Got Message buffer size : " + buffer.Length);

            string[] messages = message.Split('뷁');
            foreach (string msg in messages)
            {
                
                Debug.Log("Got Message : " + msg);
                if (onMessageReceived.Count == 0 || msg.Length == 0)
                    continue;

                NetworkDecorator.NetworkMessage m = NetworkDecorator.StringToMessage(msg);
                foreach (OnMessageReceived fn in onMessageReceived)
                {
                    fn(m);
                }
            }
        }
    }

    public void Close()
    {
        Debug.Log("close client");
        Send(NetworkDecorator.AttachHeader(NetworkHeader.ClOSE));
        NetworkValues.isNetwork = false;
        client.GetStream().Close();
        client.Close();
    }



    TcpClient client;
    NetworkStream stream;
}
