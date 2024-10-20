using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHiBox : MonoBehaviour
{
    public float enemyHealth = 100f;
    public GameObject killEffect;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(enemyHealth <= 0)
        {
            Destroy(Instantiate(killEffect, this.transform.position, Quaternion.identity), 5f);
            Destroy(this.transform.parent.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            enemyHealth -= 25f;
        }
    }
}
