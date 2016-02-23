using UnityEngine;
using System.Collections;

public class NetworkScore : MonoBehaviour {
    public Chat chat;
    bool shouldRemoveListener = false;

    void Start()
    {
        if (!NetworkValues.isNetwork)
        {
            return;
        }
        shouldRemoveListener = true;
        Client.instance.onMessageReceived.Add(OnMessageReceived);
        Client.instance.Send(NetworkDecorator.AttachHeader(NetworkHeader.NEWBIE, NetworkValues.name));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

void OnMessageReceived(NetworkDecorator.NetworkMessage m)
    {
        switch (m.header)
        {
            case NetworkHeader.RESTART:
                ScreenFader.MoveSceneGlobal("MakeTeam");
                break;
            case NetworkHeader.ClOSESERVER:
                Client.instance.Close();
                ScreenFader.MoveSceneGlobal("MainMenu");
				break;
            case NetworkHeader.ClOSE:
                chat.AddMessage("<color='#ff0000'>" + m.message + " gone.." + "</color>");
                break;
        }
    }


    public void Restart()
    {
        if (NetworkValues.isServer)
        {
            Client.instance.Send(NetworkDecorator.AttachHeader(NetworkHeader.RESTART));
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
