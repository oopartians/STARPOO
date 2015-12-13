using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class GameSequencer : MonoBehaviour {
	public GameUI ui;
	// Use this for initialization
	void Start () {
		Init ();



		MakeFleets ();
		Record.Init (ui);
	}

	void Init()
	{
	}

	void MakeFleets(){
		foreach (Team team in Match.teams) {
			foreach(string path in team.GetJSPaths()){
				GameObject fleetObject = (GameObject)Instantiate(Resources.Load("Fleet"));
				Fleet fleet = fleetObject.GetComponent<Fleet>();
				FleetAILoader fleetAILoader = fleetObject.GetComponent<FleetAILoader>();
				fleetAILoader.SetJavaScriptPath(path);
				fleet.team = team;
				team.AddFleet(fleet);
				fleet.javascriptPath = path;
			}
			team.CompleteAddFleets();
		}
	}
}
