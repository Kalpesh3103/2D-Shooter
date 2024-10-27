using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{

    public GameObject enemyImpactObj;
    public GameObject otherImpactobj;

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy"  || other.gameObject.tag == "Player")
        {
            //enemyImpact.Play();
            Destroy(Instantiate(enemyImpactObj, other.contacts[0].point, Quaternion.identity), 0.5f);
        }
        else
        {
            //otherImpact.Play();
            Destroy(Instantiate(otherImpactobj, other.contacts[0].point, Quaternion.identity), 0.5f);

        }


        Destroy(this.gameObject);
    }
}
