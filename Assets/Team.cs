using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Jurassic.Library;

public class Team : MonoBehaviour {
	public string name;
	public TeamStat stat;
	public Color color;
	public float positionAngle;
	public List<Fleet> fleets{get{return _fleets;}}

	public HashSet<Bullet> scannedBullets = new HashSet<Bullet>();
	public List<Bullet> scannedBullets_;

	public HashSet<SpaceShipHandler> spaceShips_Alliance = new HashSet<SpaceShipHandler>();

    public HashSet<SpaceShipHandler> scannedSpaceShips_Enemy = new HashSet<SpaceShipHandler>();
    public List<SpaceShipHandler> scannedSpaceShips_Enemy_;

	public JSONObject SpaceShips_Alliance = new JSONObject(JSONObject.Type.ARRAY);
	public JSONObject SpaceShips_Enemy = new JSONObject(JSONObject.Type.ARRAY);
	
	public ObjectInstance allyShips = new ObjectInstance();
	public ObjectInstance enemyShips = new ObjectInstance();

    List<Fleet> _fleets = new List<Fleet>();
	List<string> jsPaths = new List<string>();

	public void AddJSPath(string path){
		jsPaths.Add(path);
	}

	public List<string> GetJSPaths(){
		return jsPaths;
	}

	public void ReportDestroy(Fleet fleet){

	}

	public void AddFleet(Fleet fleet){
		fleet.color = color;
		_fleets.Add(fleet);
	}

	public void CompleteAddFleets(){
		int i = 0;
		foreach (Fleet fleet in _fleets) {
			fleet.positionAngle = positionAngle + ((i++) - (_fleets.Count-1)/2) * 20;
		}
	}


	void FixedUpdate(){
		scannedBullets_ = scannedBullets.ToList();
        scannedSpaceShips_Enemy_ = scannedSpaceShips_Enemy.ToList();

		
		int i = 0;
		foreach(SpaceShipHandler spaceShip in spaceShips_Alliance)
		{
			allyShips[i++] = spaceShip.ship;
		}




		i = 0;
		if(spaceShips_Alliance.Count != SpaceShips_Alliance.list.Count){
			SpaceShips_Alliance.list.Clear();
			foreach(SpaceShipHandler spaceShip in spaceShips_Alliance)
			{
				SpaceShips_Alliance.Add(spaceShip.ship);
			}
		}
		
//		foreach(SpaceShipHandler spaceShip in spaceShips_Alliance)
//		{
//			SpaceShips_Alliance.SetField(i.ToString(), spaceShip.ship);
//			i++;
//		}

		Debug.Log(SpaceShips_Alliance.Print());
		i = 0;
		foreach(SpaceShipHandler spaceShip in scannedSpaceShips_Enemy)
		{
			SpaceShips_Enemy.SetField(i.ToString(), spaceShip.ship);
			i++;
		}
    }
}
