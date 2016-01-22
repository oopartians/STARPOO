using UnityEngine;
using System.Collections;

[RequireComponent (typeof (LineRenderer))]
[ExecuteInEditMode]
public class LightingController : MonoBehaviour {
	LineRenderer line;
	// Use this for initialization
	void Start () {
		line = gameObject.GetComponent<LineRenderer> ();
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
