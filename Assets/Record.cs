using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Record {





	public static void Kill(string killerFleetName,string victimFleetName){
		if (killerFleetName == victimFleetName) {
			Suicide(killerFleetName);
		}


	}

	public static void Suicide(string poolFleetName){

	}






	class KillRecord {

	}
}
