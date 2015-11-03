using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Record {
	
	static Dictionary<Fleet,Dictionary<Fleet,float>> damageInfo;
	static Dictionary<Fleet,Dictionary<Fleet,float>> killInfo;

	public static void Init(List<Fleet> fleets){
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

	public static void Kill(Fleet killer,Fleet victim){
		killInfo [killer] [victim]++;
	}

	public static void Damage(Fleet attacker,Fleet victim){
		damageInfo [attacker] [victim]++;
	}
}
