using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;
using System;
using System.IO;



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
    string restMessage = "";

    TcpListener server;
    List<TcpClient> clients = new List<TcpClient>();
    List<TcpClient> removingClients = new List<TcpClient>();

    ManualResetEvent tcpClientConnected = new ManualResetEvent(false);
    public void Start()
    {
        if (!NetworkValues.isServer)
        {
            clients.Clear();
            removingClients.Clear();
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
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("뷁"+message+"끊");
        stream.Write(buffer, 0, buffer.Length);
    }


    public void Update()
    {
        if (server.Pending())
        {
            clients.Add(server.AcceptTcpClient());
        }
        foreach (TcpClient client in clients)
        {
            var stream = client.GetStream();
            if (stream.DataAvailable)
            {
                byte[] buffer = NetworkStreamReader.Read(stream,client.ReceiveBufferSize);
                
                Mirroring(client, buffer);
                
                var message = restMessage + System.Text.Encoding.UTF8.GetString(buffer);
                restMessage = "";
                string[] messages = message.Split('뷁');
                
                for (int i = 1; i < messages.Length; ++i)
                {
                    string msgRaw = messages[i];
                    string[] msgs = msgRaw.Split('끊');
                    
                    if (msgs.Length == 1){
                        restMessage = msgs[0];
                        continue;
                    }
                    string msg = msgs[0];
                    
                    if (msg.Length == 0)
                        continue;
                    if (msg.Length == 0) continue;
                    foreach (OnMessageReceived fn in onMessageReceived)
                    {
                        NetworkDecorator.NetworkMessage m = NetworkDecorator.StringToMessage(msg);
                        fn(client, m);
                        if (m.header == NetworkHeader.ClOSE)
                        {
                            removingClients.Add(client);
                        }
                    }
                }
            }
        }
        foreach (TcpClient client in removingClients)
        {
            clients.Remove(client);
        }
        removingClients.Clear();
    }

    public int GetNumClients()
    {
        return clients.Count;
    }

    public void Close()
    {
        Debug.Log("close server");
        Send(NetworkDecorator.AttachHeader(NetworkHeader.ClOSESERVER));
        NetworkValues.isServer = false;
        server.Stop();
        clients.Clear();
        removingClients.Clear();
    }

    void Mirroring(TcpClient sender, byte[] buffer)
    {
        foreach (TcpClient client in clients)
        {
            if (sender == client) continue;
            client.GetStream().Write(buffer, 0, buffer.Length);
        }
    }

}
