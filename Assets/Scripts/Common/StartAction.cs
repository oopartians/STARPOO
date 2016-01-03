using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class StartAction : MonoBehaviour {
    public UnityEvent onStart;
	// Use this for initialization
	void Start () {
        onStart.Invoke();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
