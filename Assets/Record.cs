using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Record {
	static GameUI _ui;
	static Dictionary<Fleet,Dictionary<Fleet,float>> damageInfo;
	static Dictionary<Fleet,Dictionary<Fleet,float>> killInfo;
	static Dictionary<Team,int> killAlly;
	static Dictionary<Team,int> killEnemy;

	public static void Init(List<Fleet> fleets,GameUI ui){
		_ui = ui;
		InitFleetRecord (fleets);
		InitTeamRecord ();
	}

	public static void Kill(Fleet killer,Fleet victim){
		killInfo [killer] [victim]++;
		if (killer.team != victim.team) {
			killEnemy[killer.team]++;
		} else {
			killAlly[killer.team]++;
		}
		_ui.UpdateTeamStat (killer.team, killAlly [killer.team], killEnemy [killer.team]);
	}

	public static void Damage(Fleet attacker,Fleet victim){
		damageInfo [attacker] [victim]++;
	}

	static void InitFleetRecord(List<Fleet> fleets){
		damageInfo = new Dictionary<Fleet,Dictionary<Fleet,float>> ();
		killInfo = new Dictionary<Fleet,Dictionary<Fleet,float>> ();
		
		foreach (Fleet fleet in fleets) {
			damageInfo.Add(fleet,new Dictionary<Fleet, float>());
			killInfo.Add(fleet,new Dictionary<Fleet, float>());
			
			foreach(Fleet target in fleets){
				damageInfo[fleet].Add (target,0);
				killInfo[fleet].Add (target,0);
			}
		}
	}

	static void InitTeamRecord(){
		killAlly = new Dictionary<Team, int> ();
		killEnemy = new Dictionary<Team, int> ();

		foreach(Team team in Match.teams){
			killAlly.Add(team,0);
			killEnemy.Add(team,0);
			_ui.CreateTeamStat(team);
		}
	}
}
