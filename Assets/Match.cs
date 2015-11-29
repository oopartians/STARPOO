using UnityEngine;
using System.Collections.Generic;

public class Match {
	public static int namingNumber = 1;
	public static List<Team> teams = new List<Team>();

	public static void Init(){
		namingNumber = 1;
		teams.Clear ();
	}

	public static Team MakeTeam(string name = null){
		if(name == null){
			name = "Team"+namingNumber++;
		}
		Team team = new Team ();
		team.name = name;
		teams.Add (team);
		return team;
	}

	public static void CompleteMakeTeams(){
		int i = 0;
		foreach (Team team in teams) {
			team.positionAngle = (i++)*360/teams.Count;
		}
	}

	public static void RemoveTeam(Team team){
		teams.Remove (team);
	}

	public static int GetNumTeams(){
		Debug.Log (teams.Count);
		return teams.Count;
	}
}


