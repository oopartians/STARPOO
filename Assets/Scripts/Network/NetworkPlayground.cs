using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net.Sockets;

public class NetworkPlayground : MonoBehaviour {
    public Console console;
    int numRequest = 0;

    void OnMessageReceivedAsServer(TcpClient client, NetworkDecorator.NetworkMessage m)
    {

        switch (m.header)
        {
            case NetworkHeader.NEWBIE:

                break;
            case NetworkHeader.REQUESTTICK:
                numRequest++;
                if (numRequest >= Server.instance.GetNumClients() && NetworkValues.requestedTick > NetworkValues.acceptedTick)
                {
                    Server.instance.Send(NetworkDecorator.AttachHeader(NetworkHeader.ACCEPTTICK, NetworkValues.requestedTick.ToString()));
                    numRequest = 0;
                }
                break;
            case NetworkHeader.GAMEOVER:
                NetworkValues.acceptedTick += 1000;
                break;

        }

    }

    bool shouldRemoveListener = false;
    // Use this for initialization
    void Start () {
        if (!NetworkValues.isNetwork)
        {
            return;
        }
        shouldRemoveListener = true;
        DontDestroyOnLoad(gameObject);
        Client.instance.onMessageReceived.Add(OnMessageReceived);
        if(NetworkValues.isServer)
            Server.instance.onMessageReceived.Add(OnMessageReceivedAsServer);
    }

    void OnMessageReceived(NetworkDecorator.NetworkMessage m)
    {
        switch (m.header)
        {
            case NetworkHeader.ClOSESERVER:
                Client.instance.Close();
                SceneManager.LoadScene("MainMenu");
                break;
            case NetworkHeader.ACCEPTTICK:
                NetworkValues.acceptedTick = int.Parse(m.message);
                break;
            case NetworkHeader.CONSOLE:
				console.AddPendingCommand(m.message);
                break;
        }
    }

    void OnDestroy()
    {
        if (shouldRemoveListener)
        {
            Client.instance.onMessageReceived.Remove(OnMessageReceived);
        }

        
    }
}
