using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{

    public GameObject gunHolder;
    public GameObject player;
    public float clampAngle = 45f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
            angle = Mathf.Clamp(angle, -135f + clampAngle, 135f+clampAngle);
        }
        else
        {
            enemyRotation.y = 0f;
            gunScale.y = 1f;
            angle = Mathf.Clamp(angle, -clampAngle, clampAngle);

        }

        this.transform.rotation = Quaternion.Euler(enemyRotation);
        this.gunHolder.transform.localScale = gunScale;
        
        //angle = Mathf.Clamp(angle, -45f, 45f);
        this.gunHolder.transform.eulerAngles = new Vector3(0, 0, angle);
        
       

        //transform.rotation = Quaternion.LookRotation(playerDirection, new Vector3(0,0,1));
    }
}