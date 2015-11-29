using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Bullet : MonoBehaviour {

	public const float speed = 10;
	public const float damage = 1;
	public float angle;
	public Fleet fleet;


	public UnityEvent onDestroyed = new UnityEvent();

	public void ListenDestroy(){

	}







	bool destroyed = false;

	void Start () {
	
	}

	void FixedUpdate () {
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
			SpaceShipHandler spaceShip = cd.GetComponent<SpaceShipHandler> ();
			Record.Damage(fleet,spaceShip.fleet);
			if(spaceShip.hp <= 1){
				Record.Kill(fleet,spaceShip.fleet);
			}
			spaceShip.Damage (damage);
			destroyed = true;
			Destroy (gameObject);
			break;
		case "Bullet":
			destroyed = true;
			Destroy (gameObject);
			break;
		}
	}
	
	void OnTriggerExit2D(Collider2D cd){
		if (destroyed)
		{
			return;
		}
		switch (cd.tag) {
		case "Ground":
			Destroy (gameObject);
			destroyed = true;
			break;
		}
	}
}
