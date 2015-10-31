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


		transform.localRotation = Quaternion.Euler (Vector3.forward * angle);
		transform.localPosition += (transform.localRotation * Vector3.right * dt * speed);
	}

	void LateUpdate(){

	}

	void OnTriggerEnter2D(Collider2D cd){
		if (destroyed) {
			return;
		}
		switch (cd.tag) {
		case "SpaceShip":
			cd.GetComponent<SpaceShipHandler> ().Damage (damage);
			break;
		case "Bullet":
		case "Wall":
			break;

		}
		destroyed = true;
		Destroy (gameObject);
	}
}
