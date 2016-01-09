using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Cleaner : MonoBehaviour {
	public static UnityEvent onClean = new UnityEvent();

	// Use this for initialization
	void Start () {
		Debug.Log ("hi");
		if(onClean != null){
			onClean.Invoke();
			Debug.Log ("good");
			onClean.RemoveAllListeners();
		}
	}
}
