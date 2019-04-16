using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalRenderLight : MonoBehaviour
{
    public float renderIntensity = 1;

    public float intensity;
    public float intensityMax = 1f;
    public float intensityMin = 0.0f;
    public float intensityMin2 = 0.0f;

    public bool startDecreasing = false;
    public bool turnOff = false;
    public bool turnOn = false;

    //bool to control lightRangeScene1
    public bool fadeOutScene1C = false;

    static float tDecrease, t2Decrease, t3Decrease, t4Decrease, t5Decrease = 0.0f;
    static float t, t2, t3, t4, t5 = 0.0f;
    float increaseValue;

    //
    public GameObject gameObContainingEnableDisableSceneOverall;


    // Update is called once per frame
    void Update()
    {
        //get info from other script
        AnimalEnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingEnableDisableSceneOverall.GetComponent<AnimalEnableDisableSceneOverall>();

        //
        RenderSettings.ambientIntensity = renderIntensity;

        //lighting increasing over time
        if (EnableDisableSceneOverallScript.canvasOn == true)
        {
            startDecrease();
        }

        else
        {
            turnOnLight();
        }

    }

    void startDecrease()
    {
        renderIntensity = Mathf.Lerp(intensityMax, intensityMin, t);
        t += 0.5f * Time.deltaTime;
    }

    void startDecreaseFast()
    {
        renderIntensity = Mathf.Lerp(intensityMax, intensityMin, t);
        t += 1.0f * Time.deltaTime;
    }

    void startDecreaseFastHard()
    {
        renderIntensity = Mathf.Lerp(intensityMax, intensityMin2, tDecrease);
        tDecrease += 1.5f * Time.deltaTime;
    }

    void startDecreaseFastHard2()
    {
        renderIntensity = Mathf.Lerp(intensityMax, intensityMin2, t2Decrease);
        t2Decrease += 1.5f * Time.deltaTime;
    }

    //

    void startIncreaseFast()
    {
        renderIntensity = Mathf.Lerp(intensityMin2, intensityMax, t2);
        t2 += 1.0f * Time.deltaTime; ;
    }

    void startIncreaseFast2()
    {
        renderIntensity = Mathf.Lerp(intensityMin2, intensityMax, t3);
        t3 += 1.0f * Time.deltaTime; ;
    }

    void startIncreaseFast3()
    {
        renderIntensity = Mathf.Lerp(intensityMin2, intensityMax, t4);
        t4 += 1.0f * Time.deltaTime; ;
    }


    void turnOffLight()
    {
        renderIntensity = 0;
    }

    void turnOnLight()
    {
        renderIntensity = 1;
    }
}
