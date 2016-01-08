using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Cleaner : MonoBehaviour {
	public static UnityEvent onClean;

	// Use this for initialization
	void Start () {
		if(onClean != null){
			onClean.Invoke();
			onClean.RemoveAllListeners();
		}
	}
}
