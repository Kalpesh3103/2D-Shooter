using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{

    public Transform camHolder;
    public float smoothness = 0.08f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        

        //transform.position = Vector3.Lerp(transform.position, camHolder.position, smoothness);

        transform.position = Vector3.SmoothDamp(transform.position, camHolder.position, ref velocity, smoothness);
    }
}
