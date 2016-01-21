using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TimeAction : MonoBehaviour {
	public float time;
	public UnityEvent action;
	// Use this for initialization

	bool did = false;
	float fromStart;
	void Start () {
		fromStart = 0;
	}
	
	// Update is called once per frame
	void Update () {
		fromStart += Time.deltaTime;

		if(time <= fromStart && !did){
			did = true;
			action.Invoke();
		}
	}
}
