using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class GameSequencer : MonoBehaviour {
	public GameUI ui;
	// Use this for initialization
	void Start () {
		Init ();
		string[] scripts = LoadJavascripts ();
		List<Fleet> fleets = MakeFleets (scripts);
		Record.Init (fleets,ui);
	}

	void Init()
	{
		Match.Init ();
	}

	string[] LoadJavascripts(){

		string[] javascriptPaths = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Script\", "*.js");
		for (int i = 0; i < javascriptPaths.Length; i++)
			javascriptPaths [i] = javascriptPaths [i];

		return javascriptPaths;
	}

	List<Fleet> MakeFleets(string[] javascriptPaths){
		List<Fleet> fleets = new List<Fleet>();
		for(int i = 0; i < javascriptPaths.Length; i++)
		{
			GameObject fleetObject = (GameObject)Instantiate(Resources.Load("Fleet"));
			Fleet fleet = fleetObject.GetComponent<Fleet>();
			fleet.javascriptPath = javascriptPaths[i];
			fleets.Add(fleet);
		}

		return fleets;
	}
}
