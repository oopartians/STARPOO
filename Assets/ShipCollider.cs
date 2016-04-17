using UnityEngine;
using System.Collections.Generic;

public class ShipCollider : CollisionPender
{

    public GameObject mainObject;
    public Ship ship;
    public Collider2D collider;
    public List<Ship> crashedShips = new List<Ship>();


    public void FixedStart()
    {
        DoNumbering();
        collider.enabled = true;
    }

    protected override void VirtualOnTriggerEnter2D(Collider2D cd)
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

    protected override void VirtualOnTriggerExit2D(Collider2D cd)
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
        cols.Clear();
        if(ship.fleet != null)
            ship.fleet.team.aiInfor.allyShips.Remove(ship);
        foreach (Team team in Match.teams)
        {
            if (team == ship.fleet.team)
                continue;
            if (team.aiInfor.scannedEnemyShipsCounter.ContainsKey(ship))
                team.aiInfor.scannedEnemyShipsCounter.Remove(ship);
        }
    }
}
