using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NetworkHelper : MonoBehaviour {

    public void Host()
    {
        Server.instance.Start();
        Client.instance.Connect("127.0.0.1");
		ScreenFader.MoveSceneGlobal("MakeTeam");
    }

    public void Connect()
    {
        Client.instance.Connect();
		ScreenFader.MoveSceneGlobal("MakeTeam");
    }

    public void Close()
    {
        if (!NetworkValues.isNetwork) return;

        if (NetworkValues.isServer)
        {
            Server.instance.Close();
        }
        Client.instance.Close();
    }
    
    public void Send()
    {
        Client.instance.Send();
    }

    public void SetName(string v)
    {
        NetworkValues.name = v;
    }

    public void SetIp(string v)
    {
        NetworkValues.ip = v;
    }

    void FixedUpdate()
    {
        if (NetworkValues.isNetwork)
        {
            if (NetworkValues.isServer)
            {
                Server.instance.Update();
            }
            Client.instance.Update();
        }
    }

    void OnApplicationQuit()
    {
        Close();
    }

}
