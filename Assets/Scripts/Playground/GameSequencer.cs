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
			team.MakeFleets();
			team.InitFleetsAngle();
		}
	}
}
