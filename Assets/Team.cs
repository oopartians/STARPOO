using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team {
	public TeamStat stat;
	
	List<Fleet> fleets = new List<Fleet>();

	public Team(){
		Match.RegisterTeam (this);
	}

	public void ReportDestroy(Fleet fleet){

	}
}
