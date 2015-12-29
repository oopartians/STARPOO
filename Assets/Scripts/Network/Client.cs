using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;

public class Client {


	Socket m_socket;

	void ClientProcess(IPAddress m_address)
	{
		m_socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
		m_socket.NoDelay = true;
		m_socket.SendBufferSize = 0;
		m_socket.Connect(m_address, NetworkConsts.port);
	}

	void 
}
