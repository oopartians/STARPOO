using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public const float speed = 10;
	public const float damage = 1;

	public float angle;


	bool destroyed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float dt = Time.deltaTime;


		transform.localRotation = Quaternion.Euler (Vector3.up * angle);
		transform.localPosition += (transform.localRotation * Vector3.right * dt * speed);
	}

	void LateUpdate(){

	}

	void OnTriggerEnter(Collider cd){
		Debug.Log (cd.tag);
		if (cd.tag == "SpaceShip" && !destroyed) {
			cd.GetComponent<SpaceShip> ().Damage (damage);
			destroyed = true;
			Destroy (gameObject);
		} else if (cd.tag == "Bullet" && !destroyed) {
			destroyed = true;
			Destroy (gameObject);
		}

	}
}
