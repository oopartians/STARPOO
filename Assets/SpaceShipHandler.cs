using UnityEngine;
using System.Collections;

public class SpaceShipHandler : MonoBehaviour {

    public const float hitRange = 1;
    public const float maxSpeed = 5;
    public const float maxAngleSpeed = 180;
    public const float maxHp = 3;
    public const float raderRadius = 10;
    public const float raderAngle = 120;
    public const float maxAmmo = 10;
    public const float fireFrequency = 1;
    public const float reloadFrequency = 0.3f;


    public float hp;
    public float angle;
    public float speed;
    public float angleSpeed;
    public float ammo;
    public float fireDelay;


    bool destroyed = false;


    // Use this for initialization
    void Start () {
		hp = maxHp;
		angle = 0;
		speed = 0;
		angleSpeed = 0;
		ammo = maxAmmo;
		fireDelay = 0;
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;

        angle += angleSpeed * dt;
        ammo = Mathf.Min(maxAmmo, ammo + reloadFrequency / dt);
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
			bullet.transform.localPosition = transform.localPosition + (transform.localRotation * Vector3.right * 1);
			bullet.GetComponent<Bullet>().angle = angle;
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
            case "Wall":
                Destroy(gameObject);
                break;
        }
    }

    void OnDestroy()
    {
        destroyed = true;
    }
}
