using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableSlowMotion : MonoBehaviour
{

    public bool isSlowmotion;
    // Start is called before the first frame update
    void Start()
    {
        isSlowmotion = false;   
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode slowMoButton = KeyCode.Mouse1;
        if(Input.GetKeyDown(slowMoButton))
        {
            Debug.Log("In Slow mo");
        }
        else if(Input.GetKeyUp(slowMoButton))
        {
            Debug.Log("Exited slow motion");
        }
    }
}
