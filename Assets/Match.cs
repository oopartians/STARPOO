using UnityEngine;
using System.Collections.Generic;

public class Match {

	public static List<Team> teams = new List<Team>();

	public static void Init(){

		teams.Clear ();
	}

	public static void RegisterTeam(Team team){

		teams.Add (team);
		Debug.Log (teams.Count);
	}

}
