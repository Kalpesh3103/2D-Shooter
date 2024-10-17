using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal; 

public class SlowMotionEffect : MonoBehaviour
{ 
    public Volume volume;  // Reference to the Volume component in your scene

    private Bloom bloom;
    private MotionBlur motionBlur;
    private ChromaticAberration chromaticAberration;
    private Vignette vignette;
    private AudioSource audioSource;

    private bool isSlowMotion = false;
    public float slowMotionFactor = 0.3f;  // How slow you want slow motion to be
    private float transitionDuration = 1f;  // Duration of transition in seconds
    private float targetTimeScale = 1f;
    private float timeElapsed = 0f;

    private float bloomIntensity = 1f;
    private float motionBlurMax = 180f;
    private float vignetteMax = 0.4f;
    private float chromaticMax = 0.5f;

    private float currentEffectLerp = 0f;
    private float targetEffectLerp = 0f;

    private float slowMotionDuration = 5f;
    public float remainingDuration;
    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        if(volume.profile.TryGet(out bloom))
        {

        }
         // Get references to the effects from the Volume profile
        if (volume.profile.TryGet(out motionBlur))
        {
            // Motion Blur settings exist in the profile
        }
        
        if (volume.profile.TryGet(out chromaticAberration))
        {
            // Chromatic Aberration settings exist in the profile
        }
        
        if (volume.profile.TryGet(out vignette))
        {
            // Vignette settings exist in the profile
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(remainingDuration < slowMotionDuration && !isSlowMotion)
        {
            remainingDuration += Time.unscaledDeltaTime;
        }

        if(isSlowMotion)
        {
            SlowMotionTimer();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && remainingDuration > 0)
        {
            StartSlowMotion();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
           ExitSlowMotion();
        }

        ProcessSlowMotion();
    }

    private void StartSlowMotion()
    {
        

        audioSource.Play();
        isSlowMotion = true;
        targetTimeScale = slowMotionFactor;
        targetEffectLerp = 1f;

        timeElapsed = 0f;
    }

    private void ExitSlowMotion()
    {
         isSlowMotion = false;
        targetTimeScale = 1f;
        targetEffectLerp = 0f;

        timeElapsed = 0f;
    }

    private void ProcessSlowMotion()
    {
        if (timeElapsed < transitionDuration)
        {
            timeElapsed += Time.unscaledDeltaTime;
            float t = timeElapsed / transitionDuration;

            // Smoothly adjust time scale
            Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, t);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            // Smoothly interpolate post-processing effect strength (independent of time scale)
            currentEffectLerp = Mathf.Lerp(currentEffectLerp, targetEffectLerp, t);


            if (bloom != null)
                bloom.intensity.value = Mathf.Lerp(0, bloomIntensity, currentEffectLerp);

            if (motionBlur != null)
                motionBlur.intensity.value = Mathf.Lerp(0, motionBlurMax, currentEffectLerp);

            if (chromaticAberration != null)
                chromaticAberration.intensity.value = Mathf.Lerp(0, chromaticMax, currentEffectLerp);

            if (vignette != null)
                vignette.intensity.value = Mathf.Lerp(0, vignetteMax, currentEffectLerp);
            // Adjust audio pitch smoothly
            //audioSource.pitch = Mathf.Lerp(1f, 0.5f, Time.timeScale);
        }
    }

    void SlowMotionTimer()
    {
        //Debug.Log("Slow Motion Started");
        if(remainingDuration > 0)
        {
            remainingDuration -= Time.unscaledDeltaTime;
            Debug.Log("Decreasing duration : " + remainingDuration);
        }
        if(remainingDuration <= 0)
        {
            ExitSlowMotion();
        }

    }

}
