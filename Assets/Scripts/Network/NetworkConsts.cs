using UnityEngine;
using System.Collections;
using System.Net;

public class NetworkConsts {
	public const int port = 55847;

	public enum NetEventType
	{
		Connect,Disconnect,SendError,ReceiveError
	}
	public enum NetworkEventResult
	{
		Success,Failure
	}

	public class NetEventState
	{
		public NetEventType type;
		public NetworkEventResult result;
	}

	public delegate void EventHandler(NetEventState state);
}
