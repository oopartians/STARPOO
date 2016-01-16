using UnityEngine;
using System.Collections;

public class TimeCounter : MonoBehaviour {
	public static float boringTime = 0;
	private static float timeOver = 10;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	public void FixedUpdate2 () {
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
