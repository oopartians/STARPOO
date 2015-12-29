using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Collections;

public class Server {
	public static Server instance{
		get{
			if(_instance == null){
				_instance = new Server();
			}
			return _instance;
		}
	}

	public bool StartServer(){

	}
	public void StopServer(){

	}




	static Server _instance;

	enum State{AcceptClient}
	State state;

	Socket m_listener;
	Socket m_socket;
	bool m_isConnected = false;


	void StartListener()
	{
		m_listener = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
		m_listener.Bind(new IPEndPoint(IPAddress.Any,NetworkConsts.port));
		m_listener.Listen(1);
		state = State.AcceptClient;
	}

	void AcceptClient()
	{
		if(m_listener != null && m_listener.Poll(0,SelectMode.SelectRead)){
			m_socket = m_listener.Accept();
			m_isConnected = true;
		}
	}
}
