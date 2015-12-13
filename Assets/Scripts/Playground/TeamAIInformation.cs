using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TeamAIInformation : MonoBehaviour {
	
	public Dictionary<IJSONExportable,int> scannedBullets = new Dictionary<IJSONExportable,int>();
	//public List<IJSONExportable> scannedBullets_;
	public HashSet<IJSONExportable> allyShips = new HashSet<IJSONExportable>();
	public Dictionary<IJSONExportable,int> scannedEnemyShips = new Dictionary<IJSONExportable,int>();
	//public List<IJSONExportable> scannedEnemyShips_;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate(){
		//scannedBullets_ = scannedBullets.Keys.ToList();
		//scannedEnemyShips_ = scannedEnemyShips.Keys.ToList();
		
		
	}
}
