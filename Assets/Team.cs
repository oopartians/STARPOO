using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Team : MonoBehaviour {
	public string name;
	public TeamStat stat;
	public Color color;
	public float positionAngle;
	public List<Fleet> fleets{get{return _fleets;}}

	public HashSet<Bullet> scannedBullets = new HashSet<Bullet>();
	public List<Bullet> scannedBullets_;

    public HashSet<SpaceShipHandler> scannedSpaceShips_Alliance = new HashSet<SpaceShipHandler>();
    public List<SpaceShipHandler> scannedSpaceShips_Alliance_;

    public HashSet<SpaceShipHandler> scannedSpaceShips_Enemy = new HashSet<SpaceShipHandler>();
    public List<SpaceShipHandler> scannedSpaceShips_Enemy_;

    public JSONObject SpaceShips_Alliance;
    public JSONObject SpaceShips_Enemy;

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
        scannedSpaceShips_Alliance_ = scannedSpaceShips_Alliance.ToList();
        scannedSpaceShips_Enemy_ = scannedSpaceShips_Enemy.ToList();

        int i = 0;
        foreach(SpaceShipHandler spaceShip in scannedSpaceShips_Alliance)
        {
            SpaceShips_Alliance.AddField(i.ToString(), spaceShip.ship);
            i++;
        }

        i = 0;
        foreach(SpaceShipHandler spaceShip in scannedSpaceShips_Enemy)
        {
            SpaceShips_Enemy.AddField(i.ToString(), spaceShip.ship);
            i++;
        }
    }
}
