using UnityEngine;
using System.Collections.Generic;

public class Radar : MonoBehaviour {
    public float radarRadius;
	
	private HashSet<IJSONExportable> ships = new HashSet<IJSONExportable>();
	private HashSet<IJSONExportable> bullets = new HashSet<IJSONExportable>();

    private CircleCollider2D circleCollider2D;
	private Team team;

    // Use this for initialization
    void Start () {
		// Update Radar Radius for develop.
		team = gameObject.GetComponentInParent<Ship> ().fleet.team;
        circleCollider2D = transform.GetComponent<CircleCollider2D>();
        circleCollider2D.radius = radarRadius;
    }
	
	// Update is called once per frame
	void Update () {
//        bullets = GameObject.FindObjectsOfType<Bullet>();
////        Debug.Log("[Radar] Current Bullets count : " + bullets.Length);
//        
//        Ship[] ships = GameObject.FindObjectsOfType<Ship>();
//        ships_alliancea = GameObject.FindObjectsOfType<Ship>();
//        foreach (Ship _ship in ships)
//        {
//            if (_ship.fleet.name.Equals(ship.fleet.name))
//                ships_alliance.Add(_ship);
//            else
//                ships_enemy.Add(_ship);
//        }
    }

	void OnTriggerEnter2D(Collider2D cd)
	{
		switch (cd.tag)
		{
		case "Bullet":
			bullets.Add(cd.gameObject.GetComponent<Bullet>());
			break;
			
		case "Ship":
			if(cd.gameObject.GetComponent<Ship>().fleet.team != team)
				ships.Add(cd.gameObject.GetComponent<Ship>());
			break;
			
		}
	}
	
	void OnTriggerExit2D(Collider2D cd){

		switch (cd.tag)
		{
		case "Bullet":
			bullets.Remove(cd.gameObject.GetComponent<Bullet>());
			break;
			
		case "Ship":
			if(cd.gameObject.GetComponent<Ship>().fleet.team != team)
				ships.Remove(cd.gameObject.GetComponent<Ship>());
			break;
			
		}
	}

	void OnDisable(){
		foreach (IJSONExportable ship in ships){
			if(team.aiInfor.scannedEnemyShips.ContainsKey(ship) && team.aiInfor.scannedEnemyShips[ship] > 1)
				--team.aiInfor.scannedEnemyShips[ship];
			else
				team.aiInfor.scannedEnemyShips.Remove(ship);
		}
		foreach (IJSONExportable bullet in bullets){
			if(team.aiInfor.scannedBullets.ContainsKey(bullet) && team.aiInfor.scannedBullets[bullet] > 1)
				--team.aiInfor.scannedBullets[bullet];
			else
				team.aiInfor.scannedBullets.Remove(bullet);
		}
	}
}
