using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;
using System;



public class Server
{
    public delegate void OnMessageReceived(TcpClient client,NetworkDecorator.NetworkMessage message);
    public List<OnMessageReceived> onMessageReceived = new List<OnMessageReceived>();
	public static Server instance{
		get{
			if(_instance == null){
				_instance = new Server();
			}
			return _instance;
		}
	}
    static Server _instance;

    TcpListener server;
    List<TcpClient> clients = new List<TcpClient>();

    ManualResetEvent tcpClientConnected = new ManualResetEvent(false);
    public void Start()
    {
        if(!NetworkValues.isServer){
            NetworkValues.isServer = true;
            server = new TcpListener(IPAddress.Parse("0.0.0.0"),NetworkValues.port);
            server.Start();
            Debug.Log("Server Started");
        }
        else
        {
            Debug.LogAssertion("Already Server is Opened");
        }
    }

    public void Send(string message)
    {
        foreach (TcpClient client in clients)
        {
            SendToCleint(client, message);
        }
    }

    public void SendToCleint(TcpClient client, string message)
    {
        var stream = client.GetStream();
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("흙"+message);
        stream.Write(buffer, 0, buffer.Length);
    }

    public void Mirroring(TcpClient sender, string message)
    {
        foreach (TcpClient client in clients)
        {
            if (sender == client) continue;
            SendToCleint(client, message);
        }
    }

    public void Update()
    {
        if (server.Pending())
        {
            clients.Add(server.AcceptTcpClient());
        }
        foreach (TcpClient client in clients)
        {
            if (!client.Connected)
            {
                Debug.Log("a client closed connection");
                clients.Remove(client);
                continue;
            }
            var stream = client.GetStream();
            if (stream.DataAvailable)
            {
                byte[] buffer = new Byte[client.ReceiveBufferSize];
                stream.Read(buffer, 0, (int)client.ReceiveBufferSize);
                var message = System.Text.Encoding.UTF8.GetString(buffer);

                Mirroring(client, message);

                string[] messages = message.Split('흙');
                foreach (string msg in messages)
                {
                    string msg2 = msg.Replace(Convert.ToChar(0x0).ToString(), "");
                    if (msg2.Length == 0) continue;
                    foreach (OnMessageReceived fn in onMessageReceived)
                    {
                        fn(client, NetworkDecorator.StringToMessage(msg));
                    }
                    Debug.Log("Mirroring : " + msg);
                }
            }
        }
    }

    public void Close()
    {
        Debug.Log("close server");
        Send(NetworkDecorator.AttachHeader(NetworkHeader.ClOSESERVER));
        NetworkValues.isServer = false;
        server.Stop();
    }

}
