﻿using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;


public class Fleet : MonoBehaviour {
	public static Dictionary<string,Fleet> fleets = new Dictionary<string, Fleet>();

	public Color color;
	public string javascriptPath{set{
			_javascriptPath = value;
			name = Path.GetFileNameWithoutExtension(value);
			gameObject.name = "Fleet("+name+")";
		}}
	public string name;
	public Team team;

	public void ReportDestroy(GameObject spaceShip){
		spaceShips.Remove (spaceShip);
		if (spaceShips.Count == 0) {
			team.ReportDestroy(this);
		}
	}


	
	string _javascriptPath;
	LinkedList<GameObject> spaceShips = new LinkedList<GameObject>();

	void Start(){
		fleets.Add (name,this);
		MakeSpaceShips ();
		if (team == null) {
			team = new Team();
		}
	}


	void MakeSpaceShips(){
		//여기서 우주선들을 만들고, 적절히 위치시킨다.
		
		spaceShips.AddLast (MakeSpaceShip ());
	}

	GameObject MakeSpaceShip(){
		GameObject spaceShip = (GameObject)Instantiate(Resources.Load("SpaceShip"));
		spaceShip.GetComponent<JavaScriptLoader> ().SetJavaScriptPath (_javascriptPath);
		spaceShip.GetComponent<SpaceShipHandler> ().fleet = this;
		return spaceShip;
	}
}
