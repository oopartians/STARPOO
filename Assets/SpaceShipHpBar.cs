using UnityEngine;
using System.Collections;

public class SpaceShipHpBar : MonoBehaviour {

    SpaceShipHandler spaceShipHandler;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
		RectTransform rt = GetComponent<RectTransform> ();
		rt.localScale = Vector3.one - Vector3.right * (1- gameObject.transform.root.GetComponent<SpaceShipHandler>().hp / SpaceShipHandler.maxHp);
	}
}
