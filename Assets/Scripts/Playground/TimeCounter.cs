using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCounter : MonoBehaviour {
	public Text timer;
	public AudioSource source;
	public AudioClip beep;
	public AudioClip thunder;

	public static float boringTime = 0;
	private static float timeOver = 30;

	static bool colorRed = false;


	Color gray = Color.black * 0.3f;
	Color red = Color.red * 0.7f;

	// Use this for initialization
	void Start () {
		boringTime = 0;
		timer.color = gray;
	}
	
	// Update is called once per frame
	public void FixedUpdate2 () {
		
		boringTime += Time.fixedDeltaTime;
		float lastTime = timeOver-boringTime;
		timer.text = (Mathf.Max(0,lastTime)).ToString("00.00");

		if(Mathf.Floor(lastTime) != Mathf.Floor(lastTime + Time.fixedDeltaTime) && lastTime < 3){
			if(lastTime > 0)
				source.PlayOneShot(beep);
			else
				source.PlayOneShot(thunder);
		}

		if(lastTime < 3 && !colorRed){
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
