using UnityEngine;
using System.Collections.Generic;

public class ShipCollider : MonoBehaviour {

    public GameObject mainObject;
    public Ship ship;
    public List<Ship> crashedShips = new List<Ship>();


    void OnTriggerEnter2D(Collider2D cd)
    {
        if (ship.isDestroyed)
        {
            return;
        }
        switch (cd.tag)
        {
            case "Bullet":
                break;

            case "Ship":
                crashedShips.Add(cd.gameObject.GetComponent<ShipCollider>().ship);
                break;

            case "Radar":
                if (cd.gameObject.GetComponentInParent<Ship>().fleet.team != ship.fleet.team)
                {
                    Dictionary<IJSONExportable, int> scannedEnemyShips = cd.gameObject.GetComponentInParent<Ship>().fleet.team.aiInfor.scannedEnemyShips;
                    if (scannedEnemyShips.ContainsKey(ship))
                        ++scannedEnemyShips[ship];
                    else
                        scannedEnemyShips.Add(ship, 1);
                }


                break;

        }
    }

    void OnTriggerExit2D(Collider2D cd)
    {
        if (ship.isDestroyed)
        {
            return;
        }
        switch (cd.tag)
        {
            case "Ground":
                Record.Kill(ship.fleet, ship.fleet);
			Destroy(ship.gameObject);
                break;

            case "Ship":
                crashedShips.Remove(cd.gameObject.GetComponent<ShipCollider>().ship);
                break;

            case "Radar":
                if (cd.gameObject.GetComponentInParent<Ship>().fleet.team != ship.fleet.team)
                {
                    Dictionary<IJSONExportable, int> scannedEnemyShips = cd.gameObject.GetComponentInParent<Ship>().fleet.team.aiInfor.scannedEnemyShips;
                    if (scannedEnemyShips.ContainsKey(ship))
                    {
                        if (scannedEnemyShips[ship] > 1)
                            --scannedEnemyShips[ship];
                        else
                            scannedEnemyShips.Remove(ship);
                    }
                }
                break;
        }
    }

    void OnDestroy()
    {
        ship.fleet.team.aiInfor.allyShips.Remove(ship);
        foreach (Team team in Match.teams)
        {
            if (team == ship.fleet.team)
                continue;
            if (team.aiInfor.scannedEnemyShips.ContainsKey(ship))
                team.aiInfor.scannedEnemyShips.Remove(ship);
        }
    }
}
