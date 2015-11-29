using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SpaceShipHandler : MonoBehaviour {

    public const float hitRange = 1;
    public const float maxSpeed = 5;
    public const float maxAngleSpeed = 180;
    public const float maxHp = 3;
    public const float raderRadius = 10;
    public const float raderAngle = 120;
    public const float maxAmmo = 10;
    public const float fireFrequency = 10;
    public const float reloadFrequency = 10.3f;


    public float hp;
    public float angle = 0;
    public float speed;
    public float angleSpeed;
    public float ammo;
    public float fireDelay;
	public Fleet fleet;


    bool destroyed = false;


    // Use this for initialization
    void Start () {
		hp = maxHp;
		speed = 0;
		angleSpeed = 0;
		ammo = maxAmmo;
		fireDelay = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float dt = Time.deltaTime;

		angle += angleSpeed * dt;
        ammo = Mathf.Min(maxAmmo, ammo + reloadFrequency * dt);
        fireDelay = Mathf.Max(0, fireDelay -= dt);

        transform.localRotation = Quaternion.Euler(Vector3.forward * angle);
        transform.localPosition += (transform.localRotation * Vector3.right * dt * speed);
    }

	void LateUpdate(){

	}

	public void Shoot(){
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
	
	public void SetAngleSpeed(float aAngleSpeed){
        angleSpeed = Mathf.Max(-maxAngleSpeed, Mathf.Min(maxAngleSpeed, aAngleSpeed));
    }
	
	public void SetSpeed(float aSpeed){
		speed = Mathf.Max (0, Mathf.Min (maxSpeed, aSpeed));
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
            case "SpaceShip":
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
		}
	}

    void OnDestroy()
    {
        destroyed = true;
		fleet.ReportDestroy (gameObject);
    }
}
