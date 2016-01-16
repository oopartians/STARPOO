using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	private float circleDrawRate = 1.0f;

	// Use this for initialization
	void Awake () {
		CircleCollider2D col = GetComponent<CircleCollider2D> ();
		CircleDrawer circle = GetComponent<CircleDrawer> ();

		col.radius = GameValueSetter.groundSize;
		circle.r = GameValueSetter.groundSize;

		circle.Draw (circleDrawRate);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
