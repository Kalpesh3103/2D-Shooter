using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHiBox : MonoBehaviour
{
    private float enemyHealth = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth <= 0)
        {
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
