using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Match {
	public static int namingNumber = 1;
	public static List<Team> teams = new List<Team>();
	public static List<Team> loseTeams = new List<Team> ();
	public static List<Team> myTeam = new List<Team>();

    public static bool isGameOver;

    Match(){
		Cleaner.onCleanPermanently.AddListener(Init);
    }

	public static void Init(){
        isGameOver = false;
		namingNumber = 1;
        teams.Clear ();
		loseTeams.Clear();
		myTeam.Clear();
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
		
    public static void ReportDestroy(Team team)
    {
		loseTeams.Add (team);
		if (teams.Count - 1 == loseTeams.Count)
        {
            GameOver();
        }
    }

    public static void GameOver()
    {
        isGameOver = true;
        SceneManager.LoadScene("Score");
    }
}


