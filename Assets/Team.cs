using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team {
	public string name;
	public TeamStat stat;
	
	List<Fleet> fleets = new List<Fleet>();
	List<string> jsPaths = new List<string>();

	public Team MakeFleet(string jsPath ){
//		Team team = new Team ();
//		team.name = name;
//		teams.Add (team);
//		
//		return team;
	}

	public void AddJSPath(string path){
		jsPaths.Add(path);
	}

	public void ReportDestroy(Fleet fleet){

	}

	public void AddFleet(Fleet fleet){
		fleets.Add(fleet);
	}

}
