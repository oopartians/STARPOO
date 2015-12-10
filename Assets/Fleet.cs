using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;


public class Fleet : MonoBehaviour {
	static public int numShip = 9;

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

	public float positionAngle;


	public void ReportDestroy(SpaceShipHandler spaceShip){
		ships.Remove (spaceShip);
		if (ships.Count == 0) {
			team.ReportDestroy(this);
		}
	}
	
	string _javascriptPath;
	public HashSet<SpaceShipHandler> ships = new HashSet<SpaceShipHandler>();

	void Start(){
		MakeSpaceShips ();
		GetComponent<FleetAILoader>().Ready();
	}


	void MakeSpaceShips(){
		//여기서 우주선들을 만들고, 적절히 위치시킨다.


		for (int i = 0; i < numShip; ++i) {
			GameObject spaceShip = MakeSpaceShip ();

			int numRow = Mathf.CeilToInt (Mathf.Sqrt((float)numShip));
			float row = i%numRow;
			float column = Mathf.Floor(i/numRow);

			float angle = positionAngle + (row-(numRow-1)/2) * 4;
			float distance = column * 4;

			float rad = Mathf.PI * angle / 180;
			float x = Mathf.Cos (rad) * (40 + distance);
			float y = Mathf.Sin (rad) * (40 + distance);
			spaceShip.transform.position = new Vector2(x,y);
			spaceShip.GetComponent<SpaceShipHandler>().angle = positionAngle - 180;
		}
	}

	GameObject MakeSpaceShip(){
		GameObject spaceShip = (GameObject)Instantiate(spaceShipPrefab,Vector3.right * Random.Range(0,50),Quaternion.identity);
		spaceShip.GetComponent<AILoader> ().SetJavaScriptPath (_javascriptPath);
		spaceShip.GetComponent<SpaceShipHandler> ().fleet = this;
		spaceShip.GetComponent<CircleDrawer> ().lineColor = color;
		spaceShip.name = spaceShipName;
		ships.Add (spaceShip.GetComponent<SpaceShipHandler> ());
		team.aiInfor.allyShips.Add(spaceShip.GetComponent<SpaceShipHandler>());
		return spaceShip;
	}
}
