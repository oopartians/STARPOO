using UnityEngine;
using System.Collections;
using System.Net;

public static class NetworkValues {
	public const int port = 55847;
    public static string ip = "127.0.0.1";
    public static bool isServer = false;
    public static bool isNetwork = false;
    public static bool waiting = false;
    public static string name;
    public static float aiTick;
    

	static NetworkValues()
    {
		Cleaner.onCleanPermanently.AddListener(()=>{
    		aiTick = 0;
		});
    }
}
