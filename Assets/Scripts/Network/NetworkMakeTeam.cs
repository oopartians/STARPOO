using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Net.Sockets;
using System;

public class NetworkMakeTeam : MonoBehaviour {
	public GameObject teamList;

	public Text textGroundSize;
	public Text textShipsPerFleet;


	public void ChangeGroundSize(string groundSize){
		if(NetworkValues.isServer){
			Client.instance.Send(NetworkDecorator.AttachHeader(NetworkHeader.CHANGEGROUNDSIZE,groundSize));
		}
		else{
			GameValueSetter.groundSize = int.Parse(groundSize);
			textGroundSize.text = groundSize;
		}
	}
	public void ChangeShipsPerFleet(string shipsPerFleet){
		if(NetworkValues.isServer){
			Client.instance.Send(NetworkDecorator.AttachHeader(NetworkHeader.CHANGESHIPSPERFLEET,shipsPerFleet));
		}
		else{
			GameValueSetter.numShipsPerFleet = int.Parse(shipsPerFleet);
			textShipsPerFleet.text = shipsPerFleet;
		}
	}


    void SendMessagesToNewbie(TcpClient client, NetworkDecorator.NetworkMessage message)
    {
        if (message.header != NetworkHeader.NEWBIE) return;

        Debug.Log("NEWBIE COMES!");

        int numTeam = teamList.transform.childCount;
        if (numTeam > 2)
        {
            for (int i = 0; i < numTeam - 2; ++i)
            {
                Server.instance.SendToCleint(client,NetworkDecorator.AttachHeader(NetworkHeader.ADDTEAM));
            }
        }
        foreach (Transform team in teamList.transform)
        {
            foreach (Transform fleet in team)
            {
                string str = team.gameObject.GetComponent<NetworkTeamPannel>().MakeNetworkMessage(NetworkHeader.ADDJS,fleet.gameObject, true);
                Debug.Log("NEWBIE : " + str);
                Server.instance.SendToCleint(client, str);
            }
        }
        Server.instance.SendToCleint(client, NetworkDecorator.AttachHeader(NetworkHeader.CHANGEGROUNDSIZE, GameValueSetter.groundSize.ToString()));
        Server.instance.SendToCleint(client, NetworkDecorator.AttachHeader(NetworkHeader.CHANGESHIPSPERFLEET, GameValueSetter.numShipsPerFleet.ToString()));
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
				teamList.GetComponent<TeamListPannel>().AddTeam();
                break;
            case NetworkHeader.REMOVETEAM:
				teamList.GetComponent<TeamListPannel>().RemoveTeam();
                break;
            case NetworkHeader.START:
				teamList.GetComponent<TeamListPannel>().Complete();
                SceneManager.LoadScene("Playground");
                break;
            case NetworkHeader.ClOSESERVER:
                Client.instance.Close();
                SceneManager.LoadScene("SelectMode");
				break;
			case NetworkHeader.CHANGEGROUNDSIZE:
				ChangeGroundSize(m.message);
				break;
			case NetworkHeader.CHANGESHIPSPERFLEET:
				ChangeShipsPerFleet(m.message);
				break;
        }
    }

    void AddJS(int index, string name, string code)
    {
        GameObject scriptPannelObj = (GameObject)Instantiate(Resources.Load("ScriptPannel"));
        scriptPannelObj.GetComponent<DragHandler>().enabled = false;
		scriptPannelObj.transform.SetParent(teamList.transform.GetChild(index));
        scriptPannelObj.transform.localScale = Vector3.one;

        var scriptPannel = scriptPannelObj.GetComponent<JavascriptPannel>();
        scriptPannel.jsInfo.name = name;
        scriptPannel.jsInfo.code = code.Replace(Convert.ToChar(0x0).ToString(), "");;
        scriptPannel.jsInfo.color = GoodColor.DequeueColor();
		scriptPannel.jsInfo.isMine = false;
        scriptPannel.UpdateInfo();
    }

    void RemoveJS(int index, string name)
    {
        GameObject removingObj = teamList.transform.GetChild(index).FindChild(name).gameObject;
        GoodColor.EnQueueColor(removingObj.GetComponent<JavascriptPannel>().jsInfo.color);
        Destroy(removingObj);
    }

    void OnDestroy()
    {
        if (!NetworkValues.isNetwork)
        {
            return;
        }

        Client.instance.onMessageReceived.Remove(OnMessageReceived);
    }
}
