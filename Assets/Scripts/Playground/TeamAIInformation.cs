using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TeamAIInformation : MonoBehaviour {
	
	public Dictionary<IJSONExportable,int> scannedBullets = new Dictionary<IJSONExportable,int>();
	public HashSet<IJSONExportable> allyShips = new HashSet<IJSONExportable>();
	public Dictionary<IJSONExportable,int> scannedEnemyShips = new Dictionary<IJSONExportable,int>();
}
