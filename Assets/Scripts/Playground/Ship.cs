using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Ship : MonoBehaviour,IJSONExportable {
    public ShipSoundPlayer sfx;

//    public const float hitRange = 1;
    public const float maxSpeed = 5;
    public const float maxAngleSpeed = 360;
    public const float maxHp = 5;
//    public const float raderRadius = 10;
//    public const float raderAngle = 120;
    public const float maxAmmo = 10;
    public const float fireFrequency = 2;
    public const float reloadFrequency = 0.2f;

    public float hp = maxHp;
    public float angle = 0;
    public float speed = 0;
    public float angleSpeed = 0;
    public float ammo = maxAmmo;
    public float fireDelay = 0;
	public Fleet fleet;
    public ShipCollider collider;
	public ShipHpBar hpBarDrawer;

    public Dictionary<string, double> exportableValues = new Dictionary<string, double>();
	public Dictionary<string,double> GetExportableValues(){return exportableValues;}

    bool destroyed = false;
    public bool isDestroyed { get { return destroyed; } }
	public bool destroyedByTimePenalty = false;
	bool wantToShoot = false;
	public ShipJSObject json;

	Vector3 pushing;

    // Use this for initialization
    public void FixedStart () {
        hp = maxHp;
        ammo = maxAmmo;
		json.UpdateProperties ();
		exportableValues ["x"] = GetPos().x;
		exportableValues ["y"] = GetPos().y;
		exportableValues ["angle"] = angle;
		exportableValues ["hp"] = hp;
    }
	
	// Update is called once per frame
	public void FixedUpdate2 () {
		
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

    public void ComputePushing(){
        var crashedShips = collider.crashedShips;
    	crashedShips.RemoveAll(item => item == null);

        var myPos = GetPos();
    	foreach(Ship ship in crashedShips){
            float d = Vector3.Distance(myPos, ship.GetPos());
			pushing.x += ship.GetPos().x/d;
			pushing.y += ship.GetPos().y/d;
    	}
		pushing.x /= crashedShips.Count;
		pushing.y /= crashedShips.Count;
    }

	public void ApplyPushing(){
		transform.localPosition = GetPos() + (GetPos() - pushing).normalized * 0.05f;
		pushing.x = 0;
		pushing.y = 0;

		exportableValues ["x"] = GetPos().x;
		exportableValues ["y"] = GetPos().y;
	}

	void ShootBullet(){
		if (ammo >= 1 && fireDelay <= 0) {
            sfx.PlayShoot();
			--ammo;
			fireDelay = 1/fireFrequency;
			GameObject bullet = (GameObject)Instantiate(Resources.Load("Bullet"));
			var p = GameObject.Find("Bullets");
			bullet.transform.SetParent(p.transform);
            bullet.transform.localPosition = transform.localPosition;// +(transform.localRotation * Vector3.right * 1);

            Bullet b = bullet.GetComponent<Bullet>();
            b.angle = angle;
            b.fleet = fleet;
            b.Ready();
            b.master = this;
            

            if (ScanUtils.NeedScanning(fleet.team))
            {
                bullet.GetComponent<Scannable>().ChangeScanCount(1);
            }
		}
	}

	void LateUpdate(){

	}

    public bool Shoot()
    {
		if (ammo >= 1 && fireDelay <= 0) {
			wantToShoot = true;
			return true;
		}
		return false;
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
        if (hp <= 0)
        {
            sfx.PlayDie();
            
            Collider2D[] cols = GetComponentsInChildren<Collider2D>();
            for(int i = 0; i < cols.Length; ++i){
                cols[i].enabled = false;
            }

            destroyed = true;
            Destroy(gameObject);
            GetComponentInChildren<LightningEffect>().gameObject.transform.SetParent(transform.parent);
            if (!Match.isGameOver && fleet != null)
                fleet.ReportDestroy(this);
            return;
		}
        else
        {
            sfx.PlayAh();
        }
		hpBarDrawer.UpdateHpDraw ();
	}

    public Vector3 GetPos()
    {
        return transform.localPosition;
    }
}
