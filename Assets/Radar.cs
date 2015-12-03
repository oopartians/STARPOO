using UnityEngine;
using System.Collections.Generic;

public class Radar : MonoBehaviour {

    public List<SpaceShipHandler> spaceShips_alliance;
    public List<SpaceShipHandler> spaceShips_enemy;
    public List<Bullet> bullets;

    // Use this for initialization
    void Start () {
        Debug.Log("Hello Radar");

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D cd)
    {
        // Q. destroy 를 체크해야하는가?
        //if (destroyed)
        //{
        //    return;
        //}

        switch (cd.tag)
        {
            case "Wall":
                break;
            case "Radar":
                break;
            case "Bullet":
                Debug.Log("Bullet!!!");
                
                // Add Bullet Information in Radar
                break;
            case "SpaceShip":
                // Add SpaceShip Information in Radar
                break;
        }
    }
}
