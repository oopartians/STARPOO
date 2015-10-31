using UnityEngine;
using System.Collections;

public class SpaceShipHpBar : MonoBehaviour {

    SpaceShipHandler spaceShipHandler;

    // Use this for initialization
    void Start () {
        //spaceShipHandler = GetComponent<SpaceShipHandler>();
        //Debug.Log("game object name : " + gameObject.name);
        //Debug.Log("ship hp : " + gameObject.transform.root.GetComponent<SpaceShipHandler>().hp);
    }
	
	// Update is called once per frame
	void Update () {
		RectTransform rt = GetComponent<RectTransform> ();
		rt.localScale = Vector3.one - Vector3.right * (1- gameObject.transform.root.GetComponent<SpaceShipHandler>().hp / SpaceShipHandler.maxHp);
	}
}
