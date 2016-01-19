using UnityEngine;
using System.Collections;

public class ShipHpBar : MonoBehaviour {
	public Ship ship;
	CircleDrawer circle;

    // Use this for initialization
    void Start ()
    {
		circle = GetComponent<CircleDrawer> ();
        if (ship.fleet != null)
        {
            circle.lineColor = new Color(ship.fleet.color.r * 0.8f, ship.fleet.color.g * 0.8f, ship.fleet.color.b * 0.8f, 1.0f);
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
