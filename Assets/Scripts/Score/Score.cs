using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Score : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Debug.Log("hi");
		foreach (Team team in Match.teams)
		{
			foreach (Fleet fleet in team.fleets)
			{
				CreateScoreInfo(fleet);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void CreateScoreInfo(Fleet fleet)
	{
		GameObject scoreInfoObj = (GameObject)UnityEngine.GameObject.Instantiate(Resources.Load("ScoreInfo"));
		RectTransform rt = scoreInfoObj.GetComponent<RectTransform> ();
		rt.SetParent (transform);
		// rt.localScale = Vector3.one;
		
		ScoreStat scoreStat = scoreInfoObj.GetComponent<ScoreStat>();
		scoreStat.teamColor.color = fleet.team.color;
		scoreStat.fleetColor.color = fleet.color;
		scoreStat.fleetName.text = fleet.name;
		scoreStat.KillCountEnemyShip.text = GetKillEnemyCount(fleet).ToString();
		scoreStat.DamagePointEnemyShip.text = GetDamagePointEnemy(fleet).ToString();
		scoreStat.KillCountAllyShip.text = GetKillAllyCount(fleet).ToString();
		scoreStat.DamagePointAllyShip.text = GetDamagePointAlly(fleet).ToString();
		scoreStat.TotalDamagePoint.text = GetDamagePointTotal(fleet).ToString();
		
		// return teamStat;
	}
	
	int GetKillEnemyCount(Fleet fleet)
	{
		int totalCount = 0;
		foreach (KeyValuePair<Fleet, float> victimFleet in Record.killInfo[fleet])
		{
			if(fleet.team != victimFleet.Key.team) 
				totalCount += (int)victimFleet.Value;
		}
		return totalCount;
	}
	
	float GetDamagePointEnemy(Fleet fleet)
	{
		float totalDamage = 0;
		foreach (KeyValuePair<Fleet, float> victimFleet in Record.damageInfo[fleet])
		{
			if (fleet.team != victimFleet.Key.team)
				totalDamage += victimFleet.Value;
		}
		return totalDamage;
	}
	
	int GetKillAllyCount(Fleet fleet)
	{
		int totalCount = 0;
		foreach (KeyValuePair<Fleet, float> victimFleet in Record.killInfo[fleet])
		{
			if (fleet.team == victimFleet.Key.team)
				totalCount += (int)victimFleet.Value;
		}
		return totalCount;
	}
	
	float GetDamagePointAlly(Fleet fleet)
	{
		float totalDamage = 0;
		foreach (KeyValuePair<Fleet, float> victimFleet in Record.damageInfo[fleet])
		{
			if (fleet.team == victimFleet.Key.team)
				totalDamage += victimFleet.Value;
		}
		return totalDamage;
	}
	
	float GetDamagePointTotal(Fleet fleet)
	{
		float totalDamage = 0;
		foreach (KeyValuePair<Fleet, float> victimFleet in Record.damageInfo[fleet])
		{
			totalDamage += victimFleet.Value;
		}
		return totalDamage;
	}
}
