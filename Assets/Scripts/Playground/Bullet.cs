﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Bullet : MonoBehaviour,IJSONExportable {
	public static List<Bullet> list = new List<Bullet>();

	public const float speed = 10;
	public const float damage = 1;
	public float angle;
	public Fleet fleet;
	
	public Dictionary<string,double> exportableValues = new Dictionary<string,double> ();
	public Dictionary<string,double> GetExportableValues(){return exportableValues;}



	bool destroyed = false;

	void Start () {
		exportableValues.Add("x",transform.localPosition.x);
		exportableValues.Add("y",transform.localPosition.y);
		exportableValues.Add("angle",angle);
		exportableValues.Add("speed",speed);
		list.Add(this);
	}

	public void FixedUpdate2 () {
		float dt = Time.deltaTime;

		transform.localRotation = Quaternion.Euler (Vector3.forward * angle);
		transform.localPosition += (transform.localRotation * Vector3.right * dt * speed);
		
		exportableValues["x"] = transform.localPosition.x;
		exportableValues["y"] = transform.localPosition.y;
	}

	void LateUpdate(){

	}

	void OnTriggerEnter2D(Collider2D cd){
		if (destroyed) {
			return;
		}
		switch (cd.tag) {
	        case "Ship":
		        Ship ship = cd.GetComponent<ShipCollider> ().ship;
                Record.Damage(fleet,ship.fleet);
		        if(ship.hp <= 1){
			        Record.Kill(fleet,ship.fleet);
                }
                ship.Damage(damage);
                Die();
		        break;

	        case "Bullet":
		        Die();
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
		}
	}

	void Die(){
		foreach(Team team in Match.teams){
			if(team.aiInfor.scannedBullets.ContainsKey(this)){
				team.aiInfor.scannedBullets.Remove(this);
			}
		}
		destroyed = true;
		Destroy (gameObject);
	}

	void OnDestroy(){
		list.Remove(this);
	}
}
