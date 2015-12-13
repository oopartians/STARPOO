using UnityEngine;
using System.Collections;

public class SpaceShipHpBar : MonoBehaviour {
	public Ship ship;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
		RectTransform rt = GetComponent<RectTransform> ();
		rt.localScale = Vector3.one - Vector3.right * (1- ship.hp / Ship.maxHp);
	}
}
