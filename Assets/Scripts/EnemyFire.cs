using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{

    public GameObject gunHolder;
    public GameObject player;
    public float clampAngle = 45f;

    public float shootDelay = 1.5f;
    public GameObject bullet;
    public float bulletSpeed = 5000f;
    public Transform gunTip;
    public ParticleSystem muzzleFlash;

    private float shootTimer;

    void Start()
    {
        shootTimer = shootDelay;
    }

    void Update()
    {
        Vector3 playerDirection =  player.transform.position - this.transform.position;

        float angle = MathF.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

       Vector3 enemyRotation = Vector3.zero;
       Vector3 gunScale = Vector3.one;

        if (angle > 90 || angle < -90)
        {
            enemyRotation.y = 180f;
            gunScale.y = -1f;
            //angle = Mathf.Clamp(angle, -180f-clampAngle , 180f+clampAngle);

        }
        else
        {
            enemyRotation.y = 0f;
            gunScale.y = 1f;
           //angle = Mathf.Clamp(angle, -clampAngle, clampAngle);

        }

        this.gunHolder.transform.eulerAngles = new Vector3(0, 0, angle);

        this.transform.rotation = Quaternion.Euler(transform.rotation.x, enemyRotation.y, transform.rotation.z);
        
        this.gunHolder.transform.localScale = gunScale;

    }

    void FixedUpdate()
    {
        // Countdown the timer
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            Shoot();
            shootTimer = shootDelay; // Reset the timer after shooting
        }
    }


    void Shoot()
    {
        
            if(!muzzleFlash.isPlaying) muzzleFlash.Play();
            Vector3 playerDirection = player.transform.position - this.transform.position;

            GameObject spawnedBullet = Instantiate(bullet, gunTip.position,Quaternion.identity);

            Rigidbody bulletRb = spawnedBullet.AddComponent<Rigidbody>();
            bulletRb.useGravity = false;

            bulletRb.velocity  = playerDirection.normalized * bulletSpeed * Time.unscaledDeltaTime;

            Destroy(spawnedBullet, 10f);

    }


}
