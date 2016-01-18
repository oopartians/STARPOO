using UnityEngine;
using System.Collections.Generic;

public class Radar : MonoBehaviour {
    public float radarRadius;
	public HashSet<Transform> ourRadars = new HashSet<Transform>();
	
	private HashSet<IJSONExportable> ships = new HashSet<IJSONExportable>();
	private HashSet<IJSONExportable> bullets = new HashSet<IJSONExportable>();

    private CircleCollider2D circleCollider2D;
	private Team team;

    // Use this for initialization
    void Start () {
		// Update Radar Radius for develop.
        if (GetComponentInParent<Ship>().fleet != null)
        {
            team = GetComponentInParent<Ship>().fleet.team;
        }
        circleCollider2D = transform.GetComponent<CircleCollider2D>();
        circleCollider2D.radius = radarRadius;
    }
	
	// Update is called once per frame
	void Update () {
    }

	void OnTriggerEnter2D(Collider2D cd)
	{
		switch (cd.tag)
		{
		case "Radar":
			var radar = cd.gameObject.GetComponent<Radar>();
			if(radar.team == team){
				ourRadars.Add(radar.transform);
			}
			break;
		case "Bullet":
			var scannedBullets = team.aiInfor.scannedBullets;
			var target = cd.GetComponent<Bullet>();

            if (scannedBullets.ContainsKey(target))
                ++scannedBullets[target];
            else
            {
                if (ScanUtils.IsVisible(team))
                {
                    ScanUtils.ChangeLayersRecursively(target.transform, "Scanned");
                }
                scannedBullets.Add(target, 1);
            }
			
			bullets.Add(target);
			break;
			
		case "Ship":

			var enemyShipCollider = cd.gameObject.GetComponent<ShipCollider>();
			if (enemyShipCollider.ship.fleet.team != team)
            {
				var scannedEnemyShips = team.aiInfor.scannedEnemyShips;
                if (scannedEnemyShips.ContainsKey(enemyShipCollider.ship))
                    ++scannedEnemyShips[enemyShipCollider.ship];
                else
                {
                    if (ScanUtils.IsVisible(team))
                    {
                        ScanUtils.ChangeLayersRecursively(enemyShipCollider.ship.transform, "Scanned");
                    }
                    scannedEnemyShips.Add(enemyShipCollider.ship, 1);
                }

				ships.Add(enemyShipCollider.ship);
			}

			break;
			
		}
	}
	
	void OnTriggerExit2D(Collider2D cd){

		switch (cd.tag)
		{

		case "Radar":
			var radar = cd.gameObject.GetComponent<Radar>();
			if(radar.team == team){
				ourRadars.Remove(radar.transform);
			}
			break;

		case "Bullet":
			var scannedBullets = team.aiInfor.scannedBullets;
			var target = cd.GetComponent<Bullet>();

            if (scannedBullets[target] > 1)
                --scannedBullets[target];
            else
            {
                if (ScanUtils.IsVisible(team))
                {
                    ScanUtils.ChangeLayersRecursively(target.transform, "Unscanned");
                }
                scannedBullets.Remove(target);
            }
			
			bullets.Remove(target);
			break;
			
		case "Ship":


			var enemyShipCollider = cd.gameObject.GetComponent<ShipCollider>();
			if (enemyShipCollider.ship.fleet.team != team)
			{
				var scannedEnemyShips = team.aiInfor.scannedEnemyShips;
				if(scannedEnemyShips.ContainsKey(enemyShipCollider.ship)){
                    if (scannedEnemyShips[enemyShipCollider.ship] > 1)
                        --scannedEnemyShips[enemyShipCollider.ship];
                    else
                    {
                        if (ScanUtils.IsVisible(team))
                        {
                            ScanUtils.ChangeLayersRecursively(enemyShipCollider.ship.transform, "Unscanned");
                        }
                        scannedEnemyShips.Remove(enemyShipCollider.ship);
                    }
				}
				ships.Add(enemyShipCollider.ship);
			}


            if (cd.gameObject.GetComponent<ShipCollider>().ship.fleet.team != team)
                ships.Remove(cd.gameObject.GetComponent<ShipCollider>().ship);
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
