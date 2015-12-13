using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;


public class Fleet : MonoBehaviour {
	static public int numShip = 9;

	public GameObject shipPrefab;
	public Color color;
	public string javascriptPath{set{
			_javascriptPath = value;
			name = Path.GetFileNameWithoutExtension(value);
			gameObject.name = "Fleet("+name+")";
			shipName = "Ship(" + name + ")";
		}}
	public string shipName;
	public string name;
	public Team team;

	public float positionAngle;


	public void ReportDestroy(Ship ship){
		ships.Remove (ship);
		if (ships.Count == 0) {
			team.ReportDestroy(this);
		}
	}
	
	string _javascriptPath;
	public HashSet<Ship> ships = new HashSet<Ship>();

	void Start(){
		MakeShips ();
		GetComponent<FleetAILoader>().Ready();
	}


	void MakeShips(){
		//여기서 우주선들을 만들고, 적절히 위치시킨다.


		for (int i = 0; i < numShip; ++i) {
			GameObject Ship = MakeShip ();

			int numRow = Mathf.CeilToInt (Mathf.Sqrt((float)numShip));
			float row = i%numRow;
			float column = Mathf.Floor(i/numRow);

			float angle = positionAngle + (row-(numRow-1)/2) * 4;
			float distance = column * 4;

			float rad = Mathf.PI * angle / 180;
			float x = Mathf.Cos (rad) * (40 + distance);
			float y = Mathf.Sin (rad) * (40 + distance);
			Ship.transform.position = new Vector2(x,y);
			Ship.GetComponent<Ship>().angle = positionAngle - 180;
		}
	}

	GameObject MakeShip(){
		GameObject ship = (GameObject)Instantiate(Resources.Load("Ship"),Vector3.right * Random.Range(0,50),Quaternion.identity);
		ship.GetComponent<Ship> ().fleet = this;
		ship.GetComponent<CircleDrawer> ().lineColor = color;
		ship.name = shipName;
		ships.Add (ship.GetComponent<Ship> ());
		team.aiInfor.allyShips.Add(ship.GetComponent<Ship>());
		return ship;
	}
}
