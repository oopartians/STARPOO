using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Bullet : MonoBehaviour,IJSONExportable {
	public static Queue<Bullet> readyQueue = new Queue<Bullet>();
	public static List<Bullet> list = new List<Bullet>();

	public const float speed = 15;
	public const float damage = 1;
    
    public static void GoBullets(){
        Debug.Log("GoBullets-------------------------------->");
        while(readyQueue.Count > 0){
            Bullet b = readyQueue.Dequeue();
            b.FixedStart();
            list.Add(b);
        }
        foreach (Bullet bullet in list) {
			bullet.FixedUpdate2();
		}
        Debug.Log("GoBullets--------------------------------<");
    }
    
	public float angle;
	public Fleet fleet;
	
	public Dictionary<string,double> exportableValues = new Dictionary<string,double> ();
	public Dictionary<string,double> GetExportableValues(){return exportableValues;}

    public Ship master;
    
    bool started = false;
    
    public void Ready(){
        Debug.Log("ready bullet");
        readyQueue.Enqueue(this);
    }
    
    public void Start(){
        Debug.Log("bullet start");
        started = true;
    }

	public void FixedStart () {
        Debug.Log("bullet fixedstart");
		exportableValues.Add("x",transform.localPosition.x);
		exportableValues.Add("y",transform.localPosition.y);
		exportableValues.Add("angle",angle);
		exportableValues.Add("speed",speed);
        GetComponent<Collider2D>().enabled = true;
		list.Add(this);
	}

	public void FixedUpdate2 () {
        
		float dt = Time.deltaTime;

		transform.localRotation = Quaternion.Euler (Vector3.forward * angle);
		transform.localPosition += (transform.localRotation * Vector3.right * dt * speed);
		
		exportableValues["x"] = transform.localPosition.x;
		exportableValues["y"] = transform.localPosition.y;
	}

	void OnTriggerEnter2D(Collider2D cd){
		switch (cd.tag) {
	        case "Ship":
		        Ship ship = cd.GetComponent<ShipCollider> ().ship;
                if (ship == master)
                {
                    return;
                }
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
        
        GetComponent<Collider2D>().enabled = false;
        
        list.Remove(this);
		Destroy (gameObject);
	}

    void OnDestroy()
    {
        if(list.Contains(this)){
            list.Remove(this);
        }
        if(readyQueue.Contains(this)){
            Debug.Log("??????????");
            readyQueue.Clear();
        }
    }

}
