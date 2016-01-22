using UnityEngine;
using System.Collections;

public class LightingController : MonoBehaviour {
	LineRenderer line;
	// Use this for initialization
	void Start () {
		line = GetComponents<LineRenderer> ();
		line.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowLightingEffect()
	{
		line.enabled = true;
	}
}
