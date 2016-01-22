using UnityEngine;
using System.Collections;

public class DestroyParticle : MonoBehaviour {
	public GameObject particle;
	// Use this for initialization
	void OnDestroy(){
		GameObject p = (GameObject)Instantiate(particle);
		p.transform.position = transform.position;
	}
}
