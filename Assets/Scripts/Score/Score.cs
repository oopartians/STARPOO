using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Score : MonoBehaviour {
	Color winColor = new Color(147 / 255.0F, 241 / 255.0F, 152 / 255.0F, 1.0f);
	Color loseColor = new Color(255 / 255.0F, 133 / 255.0F, 133 / 255.0F, 1.0f);
	// Use this for initialization
	void Start () {
		Cleaner.onClean.AddListener(CleanDataEvent);
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

		RawImage ri = scoreInfoObj.GetComponent<RawImage> ();
		if (IsGameWin (fleet)) {
			ri.color = winColor;
			scoreStat.Result.text = "Win";

		} else {
			ri.color = loseColor;
			scoreStat.Result.text = "Lose";
		}
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

	bool IsGameWin(Fleet fleet)
	{
		Debug.Log ("loseTeams Contain fleet team? : " + Match.loseTeams.Contains (fleet.team).ToString ());
		// TODO: 왜인지 모르겠는데 GameOver 함수가 호출된뒤 Score Scene으로 넘어오면 loseTeam 마지막에 최후의 이긴 승리팀이 add되게 된다.
		// 구조상 그렇게되면 안되는데 원인을 알수가없다. 추측해보자면 Scene이 넘어가면서 모든 Object가 Destroy되면서 add되는건지.. 
		// 일다는 임시방편으로 loseTeams의 맨마지막 Team인지 아닌지에 따라 승패를 체크한다.
		if (Match.loseTeams.Last().Equals(fleet.team) == false)
			return false;
		return true;
	}

	static void CleanDataEvent()
	{
		Match.Init();
	}
}
