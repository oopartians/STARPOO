using UnityEngine;
using System.Collections.Generic;

public class Radar : CollisionPender {
    public float radarRadius;
    public HashSet<Transform> ourRadars = new HashSet<Transform>();
    public CircleCollider2D circleCollider2D;
	
	private HashSet<IJSONExportable> ships = new HashSet<IJSONExportable>();
	private HashSet<IJSONExportable> bullets = new HashSet<IJSONExportable>();

	private Team team;

    // Use this for initialization
    public void FixedStart () {
		// Update Radar Radius for develop.
        if (GetComponentInParent<Ship>().fleet != null)
        {
            team = GetComponentInParent<Ship>().fleet.team;
            DoNumbering();
            circleCollider2D.enabled = true;
            circleCollider2D.radius = radarRadius;
        }
    }
	
    protected override void VirtualOnTriggerEnter2D(Collider2D cd)
	{
        if (cd == null)
        {
            Debug.Log("!!!! is null");
            return;
        }
		switch (cd.tag)
		{
		case "Radar":
			var radar = cd.gameObject.GetComponent<Radar>();
			if(radar.team == team){
				ourRadars.Add(radar.transform);
			}
			break;
		case "Bullet":
			var scannedBulletsCounter = team.aiInfor.scannedBulletsCounter;
			var target = cd.GetComponent<Bullet>();

            if (scannedBulletsCounter.ContainsKey(target))
                ++scannedBulletsCounter[target];
            else
            {
                if (ScanUtils.NeedScanning(team))
                {
                    target.GetComponent<Scannable>().ChangeScanCount(1);
                }
                scannedBulletsCounter.Add(target, 1);
                team.aiInfor.scannedBullets.Add(target);
            }
			
			bullets.Add(target);
			break;
			
		case "Ship":

			var enemyShipCollider = cd.gameObject.GetComponent<ShipCollider>();
			if (enemyShipCollider.ship.fleet.team != team)
            {
				var scannedEnemyShipsCounter = team.aiInfor.scannedEnemyShipsCounter;
                if (scannedEnemyShipsCounter.ContainsKey(enemyShipCollider.ship))
                    ++scannedEnemyShipsCounter[enemyShipCollider.ship];
                else
                {
                    if (ScanUtils.NeedScanning(team,enemyShipCollider.ship.fleet.team))
                    {
                        enemyShipCollider.ship.GetComponent<Scannable>().ChangeScanCount(1);
                    }
                    scannedEnemyShipsCounter.Add(enemyShipCollider.ship, 1);
                    team.aiInfor.scannedEnemyShips.Add(enemyShipCollider.ship);
                }

				ships.Add(enemyShipCollider.ship);
			}

			break;
			
		}
	}

    protected override void VirtualOnTriggerExit2D(Collider2D cd)
    {
        if (cd == null)
        {
            Debug.Log("!!!! is null");
            return;
        }

		switch (cd.tag)
		{

		case "Radar":
			var radar = cd.gameObject.GetComponent<Radar>();
			if(radar.team == team){
				ourRadars.Remove(radar.transform);
			}
			break;

		case "Bullet":
			var scannedBulletsCounter = team.aiInfor.scannedBulletsCounter;
			var target = cd.GetComponent<Bullet>();

            if (scannedBulletsCounter.ContainsKey(target))
            {
                if (scannedBulletsCounter[target] > 1)
                    --scannedBulletsCounter[target];
                else
                {
                    if (ScanUtils.NeedScanning(team))
                    {
                        target.GetComponent<Scannable>().ChangeScanCount(-1);
                    }
                    scannedBulletsCounter.Remove(target);
                    team.aiInfor.scannedBullets.Remove(target);
                }
            }
			
			bullets.Remove(target);
			break;
			
		case "Ship":


			var enemyShipCollider = cd.gameObject.GetComponent<ShipCollider>();
			if (enemyShipCollider.ship.fleet.team != team)
            {
				var scannedEnemyShipsCounter = team.aiInfor.scannedEnemyShipsCounter;
				if(scannedEnemyShipsCounter.ContainsKey(enemyShipCollider.ship)){
                    if (scannedEnemyShipsCounter[enemyShipCollider.ship] > 1)
                        --scannedEnemyShipsCounter[enemyShipCollider.ship];
                    else
                    {
                        if (ScanUtils.NeedScanning(team,enemyShipCollider.ship.fleet.team))
                        {
                            enemyShipCollider.ship.GetComponent<Scannable>().ChangeScanCount(-1);
                        }
                        scannedEnemyShipsCounter.Remove(enemyShipCollider.ship);
                        team.aiInfor.scannedEnemyShips.Remove(enemyShipCollider.ship);
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
        cols.Clear();
		foreach (IJSONExportable ship in ships){
            if (team.aiInfor.scannedEnemyShipsCounter.ContainsKey(ship) && team.aiInfor.scannedEnemyShipsCounter[ship] > 1)
                --team.aiInfor.scannedEnemyShipsCounter[ship];
            else
            {
                team.aiInfor.scannedEnemyShipsCounter.Remove(ship);
                team.aiInfor.scannedEnemyShips.Remove(ship);
            }
		}
		foreach (IJSONExportable bullet in bullets){
            if (team.aiInfor.scannedBulletsCounter.ContainsKey(bullet) && team.aiInfor.scannedBulletsCounter[bullet] > 1)
                --team.aiInfor.scannedBulletsCounter[bullet];
            else
            {
                team.aiInfor.scannedBulletsCounter.Remove(bullet);
                team.aiInfor.scannedBullets.Remove(bullet);
            }
		}
	}

    public override void DoCollision()
    {
        //cols.TrueForAll()
        cols.Sort(Compare);
        base.DoCollision();
    }

    public int Compare(Col a, Col b)
    {
        var orderA = a.cd.gameObject.GetComponent<INumberable>().number;
        var orderB = b.cd.gameObject.GetComponent<INumberable>().number;
        if (orderA != orderB)
        {
            return orderA - orderB;
        }
        else
        {
            return a.number - b.number;
        }
    }
}
