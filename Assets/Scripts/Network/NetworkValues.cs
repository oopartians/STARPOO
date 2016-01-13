using UnityEngine;
using System.Collections;
using System.Net;

public static class NetworkValues {
    public const int networkTickTerm = 10;
	public const int port = 55847;
    public static string ip = "127.0.0.1";
    public static bool isServer = false;
    public static bool isNetwork = false;
    public static string name;
    public static int currentTick=0;
    public static int requestedTick = networkTickTerm;
    public static int acceptedTick = networkTickTerm;
    

	static NetworkValues()
    {
		Cleaner.onCleanPermanently.AddListener(()=>{
    		currentTick = 0;
            acceptedTick = networkTickTerm;
            requestedTick = networkTickTerm;
		});
    }
}
