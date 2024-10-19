using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);
    }
}
