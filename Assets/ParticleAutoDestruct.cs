using UnityEngine;
using System.Collections;

public class ParticleAutoDestruct : MonoBehaviour {

	public  ParticleSystem thisParticleSystem;

	// Update is called once per frame
	void Update() {
		if (thisParticleSystem.isPlaying)
			return;

		Destroy (gameObject);
	}
}
