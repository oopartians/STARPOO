using UnityEngine;
using System.Collections;

public class TimeCounter : MonoBehaviour {
	public static float boringTime = 0;
	private static float timeOver = 30;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(NetworkValues.isNetwork && NetworkValues.waiting) return;
		boringTime += Time.fixedDeltaTime;
		if (boringTime > timeOver) {
			Match.DamageToAllShips (1);
			TimeCounter.ReSetBoringTime (0.3f);
		}
	}

	public static void ReSetBoringTime(float ratio = 1.0f)
	{
		boringTime = timeOver - timeOver*ratio;
	}
}
