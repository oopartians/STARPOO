using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public static class Match {
	public static int namingNumber = 1;
	public static List<Team> teams = new List<Team>();
	public static List<Team> loseTeams = new List<Team> ();
	public static List<Team> myTeam = new List<Team>();

    public static bool isGameOver;

    static Match(){
		Debug.Log ("one call Match init()");
		Cleaner.onCleanPermanently.AddListener(Init);
    }

	public static void Init(){
        isGameOver = false;
		namingNumber = 1;
		CleanAllFleet ();
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
        if (NetworkValues.isNetwork)
        {
            Client.instance.Send(NetworkDecorator.AttachHeader(NetworkHeader.GAMEOVER, NetworkValues.name));
        }
        isGameOver = true;
        ScreenFader.MoveSceneGlobal("Score");
    }

	public static void DamageToAllShips(float damage)
	{
		TimeCounter.ReSetBoringTime ();
		List<Ship> tmpShips = new List<Ship> ();
		foreach (Team team in teams) {
			foreach (Fleet fleet in team.fleets) {
				foreach (Ship ship in fleet.ships) {
					tmpShips.Add (ship);
					if (ship.hp <= damage){
						ship.destroyedByTimePenalty = true;
						ship.gameObject.GetComponent<LightningEffect>().Show();
						if (fleet.ships.Count <= 1) {
							fleet.destroyedByTimePenalty = true;
							if (fleet.team.fleets.Count == fleet.team.destroyedfleetcount + 1)
								fleet.team.destroyedByTimePenalty = true;
						}
					}
					break;
				}

			}
		}
		foreach (Ship ship in tmpShips) {
			ship.Damage (damage);
			ship.gameObject.GetComponent<LightningEffect>().Show();
		}
	}

	public static Fleet FindFleet(string fleetName){
		foreach (Team team in teams) {
			foreach (Fleet fleet in team.fleets) {
				if(fleet.name == fleetName){
					return fleet;
				}
			}
		}
		return null;
	}

	private static void CleanAllFleet()
	{
		foreach (Team team in teams) {
			foreach (Fleet fleet in team.fleets) {
				GameObject.DestroyObject (fleet);
			}
		}
	}
}
