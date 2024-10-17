using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownTimer : MonoBehaviour
{

    SlowMotionEffect slowMotionEffect;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slowMotionEffect = FindAnyObjectByType<SlowMotionEffect>();
        Debug.Log("Found slow motion effect" + slowMotionEffect.remainingDuration);
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = slowMotionEffect.remainingDuration;
    }
}
