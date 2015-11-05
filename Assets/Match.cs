using UnityEngine;
using System.Collections.Generic;

public class Match {

	public static List<Team> teams = new List<Team>();

	public static void Init(){

		teams.Clear ();
	}

	public static Team MakeTeam(string name){
		Team team = new Team ();
		team.name = name;
		teams.Add (team);

		return team;
	}

	public static void RemoveTeam(Team team){
		teams.Remove (team);
	}

	public static int GetNumTeams(){
		return teams.Count;
	}
}


