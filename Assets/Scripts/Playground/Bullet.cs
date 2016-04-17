using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Bullet : CollisionPender,IJSONExportable {
	public static Queue<Bullet> readyQueue = new Queue<Bullet>();
	public static List<Bullet> list = new List<Bullet>();

	public const float speed = 25;
	public const float damage = 1;
    
    public static void GoBullets(){
        while(readyQueue.Count > 0){
            Bullet b = readyQueue.Dequeue();
            b.FixedStart();
        }
        foreach (Bullet bullet in list) {
			bullet.FixedUpdate2();
		}
        list.RemoveAll(item => item.removeFromListMark);
    }
    
	public float angle;
	public Fleet fleet;
	
	public Dictionary<string,double> exportableValues = new Dictionary<string,double> ();
	public Dictionary<string,double> GetExportableValues(){return exportableValues;}

    public Ship master;

    public bool removeFromListMark;
    
    
    public void Ready(){
        readyQueue.Enqueue(this);
    }
    

	public void FixedStart () {
        removeFromListMark = false;
		exportableValues.Add("x",transform.localPosition.x);
		exportableValues.Add("y",transform.localPosition.y);
		exportableValues.Add("angle",angle);
		exportableValues.Add("speed",speed);
        DoNumbering();
        GetComponent<Collider2D>().enabled = true;
		list.Add(this);
	}

	public void FixedUpdate2 () {
        
		float dt = Time.deltaTime;

		transform.localRotation = Quaternion.Euler (Vector3.forward * angle);
		transform.localPosition += (transform.localRotation * Vector3.right * dt * speed);
		
		exportableValues["x"] = transform.localPosition.x;
        exportableValues["y"] = transform.localPosition.y;

        DoCollision();
	}

    protected override void VirtualOnTriggerEnter2D(Collider2D cd)
    {
        if (cd == null)
        {
            Debug.Log("!!!! is null");
            return;
        }
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

    protected override void VirtualOnTriggerExit2D(Collider2D cd)
    {
        if (cd == null)
        {
            Debug.Log("!!!! is null");
            return;
        }
		switch (cd.tag) {
		case "Ground":
			Die();
			break;
		}
	}

	void Die(){
        cols.Clear();
		foreach(Team team in Match.teams){
            if (team.aiInfor.scannedBulletsCounter.ContainsKey(this))
            {
                team.aiInfor.scannedBulletsCounter.Remove(this);
                team.aiInfor.scannedBullets.Remove(this);
			}
		}
        
        GetComponent<Collider2D>().enabled = false;
        removeFromListMark = true;
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
