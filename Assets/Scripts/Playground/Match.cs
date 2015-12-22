using UnityEngine;
using System.Collections.Generic;

public class Match {
	public static int namingNumber = 1;
	public static List<Team> teams = new List<Team>();
    public static int destroyedteamcount = 0;

	public static void Init(){
		namingNumber = 1;
	    destroyedteamcount = 0;
        teams.Clear ();
	}

	public static Team MakeTeam(string name = null){
		if(name == null){
			name = "Team"+namingNumber++;
		}
		GameObject obj = new GameObject();
		obj.name = name;
		obj.AddComponent<TeamAIInformation>();
		GameObject.DontDestroyOnLoad(obj);
		Team team = obj.AddComponent<Team>();
//		Team team = new Team ();
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

    public static void ReportDestroy(Team team)
    {
        destroyedteamcount++;
        if (teams.Count - 1 == destroyedteamcount)
        {
            GameOver();
        }
    }

    public static void GameOver()
    {
        Application.LoadLevel("Score");
    }
}


