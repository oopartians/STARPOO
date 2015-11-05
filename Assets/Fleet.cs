using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;


public class Fleet : MonoBehaviour {
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

	public Fleet(){
		if (team == null) {
			team = new Team();
			Debug.Log("TEAM!");
		}
	}

	void Start(){
		MakeSpaceShips ();
	}


	void MakeSpaceShips(){
		//여기서 우주선들을 만들고, 적절히 위치시킨다.
		spaceShips.AddLast (MakeSpaceShip ());
	}

	GameObject MakeSpaceShip(){
		GameObject spaceShip = (GameObject)Instantiate(spaceShipPrefab,Vector3.right * Random.Range(0,50),Quaternion.identity);
		spaceShip.GetComponent<AILoader> ().SetJavaScriptPath (_javascriptPath);
		spaceShip.GetComponent<SpaceShipHandler> ().fleet = this;
		spaceShip.name = spaceShipName;

		return spaceShip;
	}
}
