using UnityEngine;
using System.Collections;

public class ShipSoundPlayer : MonoBehaviour {
    public AudioSource source;
    public AudioClip[] ah;
    public AudioClip[] die;
    public AudioClip[] shoot;

    static float shootTimer;
    static float dieTimer;

    public void PlayAh(){
        source.PlayOneShot(ah[Random.Range(0, ah.Length - 1)]);
    }

    public void PlayShoot()
    {
        if (shootTimer <= 0)
        {
            source.PlayOneShot(shoot[Random.Range(0, shoot.Length - 1)]);
            shootTimer = 0.2f;
        }
    }

    public void PlayDie()
    {
        source.transform.SetParent(transform.parent);
        if (dieTimer <= 0)
        {
            source.PlayOneShot(die[Random.Range(0, die.Length - 1)]);
            Destroy(source.gameObject,3);
            dieTimer = 0.2f;
        }
        else {
            Destroy(source.gameObject);
        }
        //
    }

    void Start()
    {

    }

    void Update()
    {
        if(shootTimer > 0)
            shootTimer -= Time.deltaTime;
        if (dieTimer > 0)
            dieTimer -= Time.deltaTime;
    }


}
