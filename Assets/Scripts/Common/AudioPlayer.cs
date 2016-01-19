using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {
    public AudioSource source;
    public AudioClip clip;

	void Start () {
        DontDestroyOnLoad(gameObject);
        source.clip = clip;
        source.Play();
        Destroy(gameObject, clip.length);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
