using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakeTeamNetworkHelper : MonoBehaviour
{
    public List<GameObject> objectForServer;
    public List<GameObject> objectForClient;
    public List<GameObject> objectForNetwork;

	// Use this for initialization
	void Start () {
        if (!NetworkValues.isNetwork || NetworkValues.isServer)
        {
            SetActiveInList(objectForServer, true);
            SetActiveInList(objectForClient, false);
        }
        else
        {
            SetActiveInList(objectForServer, false);
            SetActiveInList(objectForClient, true);
        }
        if (!NetworkValues.isNetwork)
        {
            SetActiveInList(objectForNetwork, false);
        }
	}

    void SetActiveInList(List<GameObject> list,bool active){
        foreach(GameObject obj in list){
            obj.SetActive(active);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
