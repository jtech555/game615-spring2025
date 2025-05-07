using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public EnemyHealth enemyHealth;
    public int damage = 2;

    private float holdTime = 0f;
    private const float maxHoldTime = 2.3f;

    public AudioSource singleShot; 
    public AudioClip soundClip;    

    public AudioSource gunBlast;   
    public AudioClip gunBlastSoundClip; 

    private void Start()
    {
        
        AudioSource[] audioSources = GetComponents<AudioSource>();

        if (audioSources.Length >= 2)
        {
            singleShot = audioSources[0];    
            gunBlast = audioSources[1];      
        }

        singleShot.clip = soundClip;
        gunBlast.clip = gunBlastSoundClip;
    }

    void Update()
    {
      
        if (Input.GetKey(KeyCode.M))
        {
            FireSingleShot();
        }

      
        if (Input.GetKey(KeyCode.N))
        {
            ChargeShot();
        }

        if (Input.GetKeyUp(KeyCode.N))
        {
            ReleaseChargedShot();
        }
    }

    void FireSingleShot()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().linearVelocity = bulletSpawnPoint.forward * bulletSpeed;

        if (!singleShot.isPlaying)
        {
            singleShot.Play();
        }
    }

    void ChargeShot()
    {
       
        holdTime = Mathf.Min(holdTime + 0.008f, maxHoldTime);
    }

    void ReleaseChargedShot()
    {
        if (!gunBlast.isPlaying)
        {
            gunBlast.Play();
        }

        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.transform.localScale = new Vector3(holdTime, holdTime, holdTime);
        bullet.GetComponent<Rigidbody>().linearVelocity = bulletSpawnPoint.forward * bulletSpeed;

       
        holdTime = 0f;
    }
}
