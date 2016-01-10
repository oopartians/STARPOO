using UnityEngine;
using System.Collections;

public class TimeCounter : MonoBehaviour {
	public static float boringTime = 0;
	private float timeOver = 30;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		boringTime += Time.fixedDeltaTime;
		if (boringTime > timeOver) {
			Match.DamageToAllShips (1);
		}
	}

	public static void ReSetBoringTime()
	{
		boringTime = 0;
	}
}
