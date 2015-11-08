using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameUI : MonoBehaviour {
	Dictionary<Team,TeamStat> stats = new Dictionary<Team,TeamStat>();

	public void Init(){

	}

	public TeamStat CreateTeamStat(Team team){
		GameObject teamStatObj = (GameObject)UnityEngine.GameObject.Instantiate(Resources.Load("TeamStat"));
		RectTransform rt = teamStatObj.GetComponent<RectTransform> ();
		rt.SetParent (transform);
		rt.anchoredPosition = new Vector2(20 + (teamStatObj.GetComponent<RectTransform>().rect.width + 5) * stats.Count,-20);
		rt.localScale = Vector3.one;

		TeamStat teamStat = teamStatObj.GetComponent<TeamStat> ();
		teamStat.team = team;
		stats.Add (team,teamStat);

		return teamStat;
	}

	public TeamStat GetTeamStat(Team team){
		return stats[team];
	}

	public void UpdateTeamStat(Team team,int killAlly,int killEnemy){
		stats [team].textKillAlly.text = killAlly.ToString ();
		stats [team].textKillEnemy.text = killEnemy.ToString ();
	}
}

