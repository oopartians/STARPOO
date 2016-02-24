using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Cleaner : MonoBehaviour
{
    public static UnityEvent onClean = new UnityEvent();
    public static UnityEvent onCleanPermanently = new UnityEvent();

	// Use this for initialization
    void Awake()
    {
        onClean.Invoke();
        onClean.RemoveAllListeners();
        onCleanPermanently.Invoke();
        Match.Init();
	}
}
