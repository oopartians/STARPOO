using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Ship : MonoBehaviour,IJSONExportable {

    public const float hitRange = 1;
    public const float maxSpeed = 5;
    public const float maxAngleSpeed = 360;
    public const float maxHp = 3;
    public const float raderRadius = 10;
    public const float raderAngle = 120;
    public const float maxAmmo = 5;
    public const float fireFrequency = 2;
    public const float reloadFrequency = 0.3f;


    public float hp;
    public float angle = 0;
    public float speed;
    public float angleSpeed;
    public float ammo;
    public float fireDelay;
	public Fleet fleet;

	public Dictionary<string,double> exportableValues = new Dictionary<string,double> ();
	public Dictionary<string,double> GetExportableValues(){return exportableValues;}

    bool destroyed = false;
	bool wantToShoot = false;
	ShipJSObject json;

    // Use this for initialization
    void Start () {
		hp = maxHp;
		speed = 0;
		angleSpeed = 0;
		ammo = maxAmmo;
		fireDelay = 0;

		json = GetComponent<ShipJSObject> ();
		
		json.UpdateProperties ();
		exportableValues.Add ("x", GetPos ().x);
		exportableValues.Add ("y", GetPos ().y);
		exportableValues.Add ("angle", angle);
		exportableValues.Add ("hp", hp);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if(NetworkValues.isNetwork && NetworkValues.currentTick >= NetworkValues.acceptedTick) return;
		
        float dt = Time.deltaTime;

		angle += angleSpeed * dt;
		angle %= 360;
		if (angle > 180) {
			angle -= 360;
		}
		if (angle < -180) {
			angle += 360;
		}
        ammo = Mathf.Min(maxAmmo, ammo + reloadFrequency * dt);
        fireDelay = Mathf.Max(0, fireDelay -= dt);

        transform.localRotation = Quaternion.Euler(Vector3.forward * angle);
        transform.localPosition += (transform.localRotation * Vector3.right * dt * speed);

		if (wantToShoot) {
			ShootBullet();
			wantToShoot = false;
		}

		json.UpdateProperties ();
		
		exportableValues ["x"] = GetPos().x;
		exportableValues ["y"] = GetPos().y;
		exportableValues ["angle"] = angle;
		exportableValues ["hp"] = hp;
    }

	void ShootBullet(){
		if (ammo >= 1 && fireDelay <= 0) {
			--ammo;
			fireDelay = 1/fireFrequency;
			GameObject bullet = (GameObject)Instantiate(Resources.Load("Bullet"));
			var p = GameObject.Find("Bullets");
			bullet.transform.SetParent(p.transform);
			bullet.transform.localPosition = transform.localPosition + (transform.localRotation * Vector3.right * 1);
			bullet.GetComponent<Bullet>().angle = angle;
			bullet.GetComponent<Bullet>().fleet = fleet;
		}
	}

	void LateUpdate(){

	}

	public void Shoot(){
		wantToShoot = true;
	}
	
	public void SetAngleSpeed(float aAngleSpeed){
		angleSpeed = Mathf.Max(-maxAngleSpeed, Mathf.Min(maxAngleSpeed, aAngleSpeed));
		json.UpdateProperty ("angleSpeed", angleSpeed);
    }
	
	public void SetSpeed(float aSpeed){
		speed = Mathf.Max (0, Mathf.Min (maxSpeed, aSpeed));
		json.UpdateProperty ("speed", speed);
	}

	public void Damage(float damage){
		hp -= damage;
		if (hp <= 0) {
			Destroy(gameObject);
		}
	}

    public Vector3 GetPos()
    {
        return transform.localPosition;
    }

	void OnTriggerEnter2D(Collider2D cd)
    {
        if (destroyed)
        {
            return;
        }
        switch (cd.tag)
        {
        case "Bullet":
            break;

        case "Ship":
            break;

		case "Radar":
//                Debug.Log("[Ship] Radar hit" + Random.Range(0, 1000).ToString());
            if (cd.gameObject.GetComponentInParent<Ship>().fleet.team != fleet.team)
            {
				Dictionary<IJSONExportable,int> scannedEnemyShips = cd.gameObject.GetComponentInParent<Ship>().fleet.team.aiInfor.scannedEnemyShips;
				if (scannedEnemyShips.ContainsKey(this))
					++scannedEnemyShips[this];
				else
					scannedEnemyShips.Add(this,1);
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
            Record.Kill (fleet, fleet);
            Destroy (gameObject);
            break;

        case "Radar":
            if (cd.gameObject.GetComponentInParent<Ship>().fleet.team != fleet.team)
			{
				Dictionary<IJSONExportable,int> scannedEnemyShips = cd.gameObject.GetComponentInParent<Ship>().fleet.team.aiInfor.scannedEnemyShips;
				if (scannedEnemyShips.ContainsKey(this)){
					if(scannedEnemyShips[this] > 1)
						--scannedEnemyShips[this];
					else
						scannedEnemyShips.Remove(this);
				}
	        }
            break;
        }
	}

    void OnDestroy()
    {
		this.fleet.team.aiInfor.allyShips.Remove(this);
		foreach(Team team in Match.teams){
			if(team == fleet.team)
				continue;
			if (team.aiInfor.scannedEnemyShips.ContainsKey(this))
				team.aiInfor.scannedEnemyShips.Remove(this);
		}

        destroyed = true;
        if(!Match.isGameOver)
		    fleet.ReportDestroy (this);
    }
}
