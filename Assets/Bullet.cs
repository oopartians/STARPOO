using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

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
			Die();
			break;
		case "Bullet":
			Die();
			break;
		case "Radar":
			if(!cd.gameObject.GetComponentInParent<SpaceShipHandler>().fleet.team.scannedBullets.Contains(this)){
				cd.gameObject.GetComponentInParent<SpaceShipHandler>().fleet.team.scannedBullets.Add(this);
			}
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
			Die();
			break;
		case "Radar":
			if(cd.gameObject.GetComponentInParent<SpaceShipHandler>().fleet.team.scannedBullets.Contains(this)){
				cd.gameObject.GetComponentInParent<SpaceShipHandler>().fleet.team.scannedBullets.Remove(this);
			}
			break;
		}
	}

	void Die(){
		foreach(Team team in Match.teams){
			if(team.scannedBullets.Contains(this)){
				team.scannedBullets.Remove(this);
			}
		}
		destroyed = true;
		Destroy (gameObject);
	}
}
