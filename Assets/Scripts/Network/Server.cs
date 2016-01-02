using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;
using System;



public class Server {
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
        Debug.Log("server started");
        server.Start();
        Debug.Log("server BeginAcceptTcpClient");
        //server.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpClientCallback), server);
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
                Debug.Log("can read!!!!!!!!!!!!!!!" + stream.CanRead);
                byte[] buffer = new Byte[256];
                stream.Read(buffer, 0, (int)256);
                Debug.Log(System.Text.Encoding.Unicode.GetString(buffer));
            }
        }
    }

    public void Close()
    {
        Debug.Log("close server");
        server.Stop();
    }

    Server()
    {
        server = new TcpListener(IPAddress.Parse("127.0.0.1"),NetworkConsts.port);
    }

    void DoAcceptTcpClientCallback(IAsyncResult ar)
    {
        Debug.Log("server DoAcceptTcpClientCallback");
        // Get the listener that handles the client request.
        TcpListener listener = (TcpListener)ar.AsyncState;

        // End the operation and display the received data on 
        // the console.
        TcpClient client = listener.EndAcceptTcpClient(ar);

        // Process the connection here. (Add the client to a
        // server table, read data, etc.)
        Debug.Log("Client connected completed");

        // Signal the calling thread to continue.
        //tcpClientConnected.Set();
    }
}
