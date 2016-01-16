using UnityEngine;
using System.Collections;

public class ShipHpBar : MonoBehaviour {
	public Ship ship;
	private float currentRate = 1.0f;
	CircleDrawer circle;

    // Use this for initialization
    void Start ()
    {
		circle = GetComponent<CircleDrawer> ();
		circle.lineColor = ship.fleet.color;
    }
	
	// Update is called once per frame
	void Update () {
	}

	public void UpdateHpDraw()
	{
		circle.Draw(ship.hp / Ship.maxHp);
	}
}
