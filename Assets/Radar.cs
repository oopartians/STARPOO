using UnityEngine;
using System.Collections.Generic;

public class Radar : MonoBehaviour {
    public float radarRadius;
    private CircleCollider2D circleCollider2D;

    // Use this for initialization
    void Start () {
        // Update Radar Radius for develop.
        circleCollider2D = transform.GetComponent<CircleCollider2D>();
        circleCollider2D.radius = radarRadius;
    }
	
	// Update is called once per frame
	void Update () {
//        bullets = GameObject.FindObjectsOfType<Bullet>();
////        Debug.Log("[Radar] Current Bullets count : " + bullets.Length);
//        
//        SpaceShipHandler[] spaceShips = GameObject.FindObjectsOfType<SpaceShipHandler>();
//        spaceShips_alliancea = GameObject.FindObjectsOfType<SpaceShipHandler>();
//        foreach (SpaceShipHandler _spaceShip in spaceShips)
//        {
//            if (_spaceShip.fleet.name.Equals(spaceShip.fleet.name))
//                spaceShips_alliance.Add(_spaceShip);
//            else
//                spaceShips_enemy.Add(_spaceShip);
//        }
    }
}
