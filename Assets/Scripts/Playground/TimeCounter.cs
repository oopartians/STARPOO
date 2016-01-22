using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCounter : MonoBehaviour {
	public Text timer;
	public static float boringTime = 0;
	private static float timeOver = 30;

	static bool colorRed = false;


	Color gray = Color.black * 0.3f;
	Color red = Color.red * 0.7f;

	// Use this for initialization
	void Start () {
		timer.color = gray;
	}
	
	// Update is called once per frame
	public void FixedUpdate2 () {
		boringTime += Time.fixedDeltaTime;
		timer.text = (timeOver - boringTime).ToString("00.00");
		if(timeOver-boringTime < 3 && !colorRed){
			if(!colorRed){
				colorRed = true;
				timer.color = red;
			}
		}
		else if(colorRed){
			colorRed = false;
			timer.color = gray;
		}
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
