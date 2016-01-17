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

    // Use this for initialization
    void Start () {
        hp = maxHp;
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

		PushedByCrashedShips();

		json.UpdateProperties ();
		
		exportableValues ["x"] = GetPos().x;
		exportableValues ["y"] = GetPos().y;
		exportableValues ["angle"] = angle;
		exportableValues ["hp"] = hp;
    }

    void PushedByCrashedShips(){
        var crashedShips = collider.crashedShips;
    	crashedShips.RemoveAll(item => item == null);
		Vector3 pos = new Vector3(0,0,0);
    	foreach(Ship ship in crashedShips){
			pos.x += ship.GetPos().x;
			pos.y += ship.GetPos().y;
    	}
		pos.x /= crashedShips.Count;
		pos.y /= crashedShips.Count;

		transform.localPosition = GetPos() + (GetPos() - pos).normalized * 0.1f;
    	
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

            if (ScanUtils.IsVisible(fleet.team))
            {
                ScanUtils.ChangeLayersRecursively(bullet.transform,"Scanned");
            }
            else
            {
                ScanUtils.ChangeLayersRecursively(bullet.transform, "Unscanned");
            }
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
		hpBarDrawer.UpdateHpDraw ();
	}

    public Vector3 GetPos()
    {
        return transform.localPosition;
    }


    void OnDestroy()
    {
        destroyed = true;
        if(!Match.isGameOver && fleet != null)
		    fleet.ReportDestroy (this);
    }
}
