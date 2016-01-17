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
        if (ship.fleet != null)
        {
            circle.lineColor = ship.fleet.color;
        }
        else
        {
            circle.lineColor = Color.green;
        }
    }
	
	// Update is called once per frame
	void Update () {
	}

	public void UpdateHpDraw()
	{
		circle.Draw(ship.hp / Ship.maxHp);
	}
}
