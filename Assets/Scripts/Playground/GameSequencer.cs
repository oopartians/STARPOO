using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class GameSequencer : MonoBehaviour {
	public GameUI ui;
	// Use this for initialization
	void Start () {
		MakeFleets ();
		Record.Init (ui);
	}

	void MakeFleets(){
		foreach (Team team in Match.teams) {
			team.MakeFleets();
			team.InitFleetsAngle();
		}
	}
}
