using UnityEngine;
using System.Collections.Generic;

public class TeamListPannel : MonoBehaviour {
	List<GameObject> teamPannels = new List<GameObject>();

	// Use this for initialization
	void Start () {
		AddTeam();
		AddTeam();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void AddTeam(){
		GameObject pannel = (GameObject)Instantiate(Resources.Load("TeamPannel"));
		pannel.transform.SetParent(transform);
		pannel.transform.localScale = Vector3.one;
		teamPannels.Add(pannel);
	}
	public void RemoveTeam(){
		if(teamPannels.Count <= 2){
			return;
		}
		foreach(GameObject pannel in teamPannels){
			if(pannel.transform.childCount <= 0){
				teamPannels.Remove(pannel);
				Destroy (pannel);
				break;
			}
		}
	}


}
