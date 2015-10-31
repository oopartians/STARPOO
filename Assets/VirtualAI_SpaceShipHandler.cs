using UnityEngine;
using System.Collections;

public class VirtualAI_SpaceShipHandler : MonoBehaviour {

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
    void Start()
    {
        hp = maxHp;
        angle = 0;
        speed = 0;
        angleSpeed = 0;
        ammo = maxAmmo;
        fireDelay = 0;
    }

    void Update()
    {
        float dt = Time.deltaTime;

        angle += angleSpeed * dt;
        ammo = Mathf.Min(maxAmmo, ammo + reloadFrequency / dt);
        fireDelay = Mathf.Max(0, fireDelay -= dt);

        transform.localRotation = Quaternion.Euler(Vector3.forward * angle);
        transform.localPosition += (transform.localRotation * Vector3.right * dt * speed);
    }

    void LateUpdate()
    {
        VirtualAI();
    }

    void Shoot()
    {
        if (ammo >= 1 && fireDelay <= 0)
        {
            --ammo;
            fireDelay = 1 / fireFrequency;
            GameObject bullet = (GameObject)Instantiate(Resources.Load("Bullet"));
            bullet.transform.localPosition = transform.localPosition + (transform.localRotation * Vector3.right * 1);
            bullet.GetComponent<Bullet>().angle = angle;
        }
    }

    void SetAngleSpeed(float aAngleSpeed)
    {
        angleSpeed = Mathf.Max(-maxAngleSpeed, Mathf.Min(maxAngleSpeed, aAngleSpeed));
    }

    void SetSpeed(float aSpeed)
    {
        speed = Mathf.Max(0, Mathf.Min(maxSpeed, aSpeed));
    }

    public void Damage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void VirtualAI()
    {
        if (Random.value < 0.5 * Time.deltaTime)
            SetAngleSpeed(maxAngleSpeed - Random.value * maxAngleSpeed * 2);
        if (Random.value < 0.5 * Time.deltaTime)
            SetSpeed(Random.value * maxSpeed / 2 + maxSpeed / 2);
        if (Random.value < 10.5 * Time.deltaTime)
            Shoot();
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
