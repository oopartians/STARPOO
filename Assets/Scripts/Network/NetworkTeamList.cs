using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net.Sockets;

public class NetworkTeamList : MonoBehaviour {

    public void SendMessagesToNewbie(TcpClient client, NetworkDecorator.NetworkMessage message)
    {
        Debug.Log("NEWBIE COMES! : " + message.header + message.message);
        if (message.header != NetworkHeader.NEWBIE) return;

        Debug.Log("NEWBIE COMES!");

        int numTeam = transform.childCount;
        if (numTeam > 2)
        {
            for (int i = 0; i < numTeam - 2; ++i)
            {
                Server.instance.SendToCleint(client,NetworkDecorator.AttachHeader(NetworkHeader.ADDTEAM));
            }
        }
        foreach (Transform team in transform)
        {
            foreach (Transform fleet in team)
            {
                string str = team.gameObject.GetComponent<NetworkTeamPannel>().MakeNetworkMessage(fleet.gameObject, true);
                Debug.Log("NEWBIE : " + str);
                Server.instance.SendToCleint(client, str);
            }
        }

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
        else
            Client.instance.Send(NetworkDecorator.AttachHeader(NetworkHeader.NEWBIE));
	}

    void OnMessageReceived(NetworkDecorator.NetworkMessage m)
    {
        Debug.Log("OnMessageReceived : " + m.message);
        switch (m.header)
        {
            case NetworkHeader.ADDJS:
                string[] strings = m.message.Split(':');
                AddJS(int.Parse(strings[0]), strings[1], m.message.Split('콛')[1]);
                break;

            case NetworkHeader.REMOVEJS:
                string[] strings2 = m.message.Split(':');
                RemoveJS(int.Parse(strings2[0]), strings2[1]);
                break;
            case NetworkHeader.ADDTEAM:
                GetComponent<TeamListPannel>().AddTeam();
                break;
            case NetworkHeader.REMOVETEAM:
                GetComponent<TeamListPannel>().RemoveTeam();
                break;
            case NetworkHeader.START:
                GetComponent<TeamListPannel>().Complete();
                SceneManager.LoadScene("Playground");
                break;
            case NetworkHeader.ClOSESERVER:
                Client.instance.Close();
                SceneManager.LoadScene("SelectMode");
                break;

        }
    }

    void AddJS(int index, string name, string code)
    {
        GameObject scriptPannelObj = (GameObject)Instantiate(Resources.Load("ScriptPannel"));
        scriptPannelObj.GetComponent<DragHandler>().enabled = false;
        scriptPannelObj.transform.SetParent(transform.GetChild(index));
        scriptPannelObj.transform.localScale = Vector3.one;

        var scriptPannel = scriptPannelObj.GetComponent<JavascriptPannel>();
        scriptPannel.jsInfo.name = name;
        scriptPannel.jsInfo.code = code;
        scriptPannelObj.GetComponentInChildren<Text>().text = scriptPannel.jsInfo.name;
        scriptPannelObj.name = scriptPannel.jsInfo.name;
    }

    void RemoveJS(int index, string name)
    {
        Destroy(transform.GetChild(index).FindChild(name).gameObject);
    }

    void OnDestroy()
    {
        Client.instance.onMessageReceived.Remove(OnMessageReceived);
    }
}
