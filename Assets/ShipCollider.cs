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
                ship.Damage(Ship.maxHp);
                break;

            case "Ship":
                crashedShips.Remove(cd.gameObject.GetComponent<ShipCollider>().ship);
                break;
        }
    }

    void OnDestroy()
    {
        if(ship.fleet != null)
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
