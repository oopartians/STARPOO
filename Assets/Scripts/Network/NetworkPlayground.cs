using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net.Sockets;

public class NetworkPlayground : MonoBehaviour {
    public Console console;

    void SendMessagesToNewbie(TcpClient client, NetworkDecorator.NetworkMessage message)
    {
        if (message.header != NetworkHeader.NEWBIE) return;
    } 

    // Use this for initialization
    void Start () {
        if (!NetworkValues.isNetwork)
        {
            return;
        }

        Client.instance.onMessageReceived.Add(OnMessageReceived);
        if(NetworkValues.isServer)
            Server.instance.onMessageReceived.Add(SendMessagesToNewbie);
    }

    void OnMessageReceived(NetworkDecorator.NetworkMessage m)
    {
        switch (m.header)
        {
            case NetworkHeader.ClOSESERVER:
                Client.instance.Close();
                SceneManager.LoadScene("SelectMode");
                break;
        }
    }

    void OnDestroy()
    {if (!NetworkValues.isNetwork)
        {
            return;
        }

        Client.instance.onMessageReceived.Remove(OnMessageReceived);
    }
}
