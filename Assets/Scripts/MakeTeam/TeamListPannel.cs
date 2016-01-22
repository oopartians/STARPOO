using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Random = UnityEngine.Random;

public class TeamListPannel : MonoBehaviour {
	List<GameObject> teamPannels = new List<GameObject>();
    private int defaultColorCount = 13;

    void Start()
    {
        GoodColor.Init();
        GoodColor.SetColorsList(defaultColorCount);

        AddTeam(false);
        AddTeam(false);
    }
	
	void Update () {
	    
	}

	public void AddTeam(bool sendMessage = true){
		GameObject pannel = (GameObject)Instantiate(Resources.Load("TeamPannel"));
		pannel.transform.SetParent(transform);
		pannel.transform.localScale = Vector3.one;

        pannel.GetComponent<Image>().color = GoodColor.DequeueColor();

        teamPannels.Add(pannel);

        if (NetworkValues.isServer && NetworkValues.isNetwork && sendMessage)
        {
            Client.instance.Send(NetworkDecorator.AttachHeader(NetworkHeader.ADDTEAM));
        }
	}

	public void RemoveTeam(){
		if(teamPannels.Count <= 2){
			return;
		}
		foreach(GameObject pannel in teamPannels){
			if(pannel.transform.childCount <= 0){
                GoodColor.EnQueueColor(pannel.GetComponent<Image>().color);
				teamPannels.Remove(pannel);
				Destroy (pannel);
				break;
			}
		}

        if (NetworkValues.isServer)
        {
            Client.instance.Send(NetworkDecorator.AttachHeader(NetworkHeader.REMOVETEAM));
        }
	}

	public void Complete(){
		foreach(GameObject pannel in teamPannels){
			Team team = Match.MakeTeam();
			team.color = pannel.GetComponent<Image>().color;
			team.color.a = 1;

			foreach(Transform js in pannel.transform){
				JavascriptPannel jsPannel = js.gameObject.GetComponent<JavascriptPannel>();
				team.AddJSInfo(jsPannel.jsInfo);

				if(jsPannel.jsInfo.isMine && !Match.myTeam.Contains(team)){
					Match.myTeam.Add(team);
				}
			}
		}
		Match.CompleteMakeTeams ();

        if (NetworkValues.isServer)
        {
            Client.instance.Send(NetworkDecorator.AttachHeader(NetworkHeader.START));
        }
        ScreenFader.MoveSceneGlobal("Playground");
	}

}
