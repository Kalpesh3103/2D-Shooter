using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class gunAim : MonoBehaviour
{

        public Transform recoilMod;
        public Transform weapon;
        float maxRecoil_x = -20;
        float recoilSpeed= 50;
        float recoil = 0.0f;
        public GameObject bullet;
        public float bulletSpeed = 500000f;
        private AudioSource audioSource;

    public LineRenderer lr;
    public Transform gunTip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lr.positionCount = 2;

    }

    // Update is called once per frame
    void Update()
    {
        GunAim();

    }

    void GunAim()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.z * -1));

        //mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0f);
        Vector3 newPos = new Vector3(worldPos.x, worldPos.y, transform.position.z);

        Vector3 aimDirection = (newPos - transform.position).normalized;
        float angle = MathF.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);


        Vector3 aimLocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.y = -1f;
        }
        else
        {
            aimLocalScale.y = +1f;
        }
        transform.localScale = aimLocalScale;

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, newPos);


        if(Input.GetMouseButtonDown(0))
        {
            recoil += 0.1f;
            Shoot(newPos);
            PlaySound();
        }

        Recoiling();
    }

    void PlaySound()
    {
        if(audioSource.isPlaying){
            audioSource.Stop();
        }
        audioSource.pitch = Mathf.Lerp(1f, 0.5f, 1 - Time.timeScale);

        audioSource.Play();
    }
    void Shoot(Vector3 target)
    {
        GameObject spawnedBullet = Instantiate(bullet, gunTip.position,Quaternion.identity);
        // spawnedBullet.transform.rotation.y = 90;
        Rigidbody bulletRb = spawnedBullet.AddComponent<Rigidbody>();
        bulletRb.useGravity = false;
        bulletRb.AddForce((target-gunTip.position).normalized * bulletSpeed * Time.unscaledDeltaTime, ForceMode.Force);


        Destroy(spawnedBullet, 10f);
    }
    void  Recoiling() {
        if(recoil > 0)
        {
            var maxRecoil = Quaternion.Euler (maxRecoil_x, 0, 0);
            // Dampen towards the target rotation
            recoilMod.rotation = Quaternion.Slerp(recoilMod.rotation, maxRecoil, Time.deltaTime * recoilSpeed);
            weapon.localEulerAngles = new Vector3(recoilMod.localEulerAngles.x, weapon.localEulerAngles.y, weapon.localEulerAngles.z );
            recoil -= Time.deltaTime;
        }
        else
        {
            recoil = 0;
            var minRecoil = Quaternion.Euler (0, 0, 0);
            // Dampen towards the target rotation
            recoilMod.rotation = Quaternion.Slerp(recoilMod.rotation, minRecoil,Time.deltaTime * recoilSpeed / 2);
            weapon.transform.localEulerAngles = new Vector3(recoilMod.localEulerAngles.x, weapon.transform.localEulerAngles.y, weapon.transform.localEulerAngles.z);
        }
    }
}
