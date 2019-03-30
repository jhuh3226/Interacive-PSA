using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLight : MonoBehaviour
{
    public float renderIntensity;

    //public float intensity;
    public float intensityMax = 0f;
    public float intensityMin = 1f;

    public bool startDecreasing = false;
    public bool turnOff = false;
    public bool turnOn = false;

    static float t = 0.0f;

    // Update is called once per frame
    void Update()
    {
        RenderSettings.ambientIntensity = renderIntensity;

        //lighting increasing over time
        if (startDecreasing == true)
        {
            renderIntensity = Mathf.Lerp(intensityMax, intensityMin, t);
            t += 0.7f * Time.deltaTime;
        }

        else if (turnOff == true)
        {
            renderIntensity = 0;
        }

        else if (turnOn == true)
        {
            renderIntensity = 1;
        }

    }
}
