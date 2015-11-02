using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;


public class Fleet : MonoBehaviour {
	public static Dictionary<string,Fleet> fleets = new Dictionary<string, Fleet>();

	public GameObject spaceShipPrefab;
	public Color color;
	public string javascriptPath{set{
			_javascriptPath = value;
			name = Path.GetFileNameWithoutExtension(value);
			gameObject.name = "Fleet("+name+")";
			spaceShipName = "SpaceShip(" + name + ")";
		}}
	public string spaceShipName;
	public string name;
	public Team team;

	public void ReportDestroy(GameObject spaceShip){
		spaceShips.Remove (spaceShip);
		if (spaceShips.Count == 0) {
			team.ReportDestroy(this);
		}
	}


	
	string _javascriptPath;
	LinkedList<GameObject> spaceShips = new LinkedList<GameObject>();

	void Start(){
		Debug.Log ("start - " + name);
		fleets.Add (name,this);
		MakeSpaceShips ();
		if (team == null) {
			team = new Team();
		}
	}


	void MakeSpaceShips(){
		//여기서 우주선들을 만들고, 적절히 위치시킨다.
		Debug.Log ("MakeSpaceShips - " + name);
		spaceShips.AddLast (MakeSpaceShip ());
	}

	GameObject MakeSpaceShip(){
		Debug.Log ("MakeSpaceShip - " + name);
		GameObject spaceShip = (GameObject)Instantiate(spaceShipPrefab,Vector3.right * Random.Range(0,50),Quaternion.identity);
		spaceShip.GetComponent<JavaScriptLoader> ().SetJavaScriptPath (_javascriptPath);
		spaceShip.GetComponent<SpaceShipHandler> ().fleet = this;
		spaceShip.name = spaceShipName;

		return spaceShip;
	}
}
