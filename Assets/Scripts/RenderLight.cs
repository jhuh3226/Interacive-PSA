using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLight : MonoBehaviour
{
    public float renderIntensity = 1;

    public float intensity;
    public float intensityMax = 1f;
    public float intensityMin = 0.4f;
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
    public GameObject gameObContainingCanvasAppear;
    public GameObject gameObContainingEnableDisableSceneOverall;
    public GameObject gameObContainingEnableDisableScene1;

    // Update is called once per frame
    void Update()
    {
        //get info from other script
        CanvasAppear CanvasAppearScript = gameObContainingCanvasAppear.GetComponent<CanvasAppear>();
        EnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingEnableDisableSceneOverall.GetComponent<EnableDisableSceneOverall>();
        EnableDisableScene1 EnableDisableScene1Script = gameObContainingEnableDisableScene1.GetComponent<EnableDisableScene1>();

        //

        RenderSettings.ambientIntensity = renderIntensity;

        //lighting increasing over time
        if (CanvasAppearScript.startDecreasing == true || Input.GetKey(KeyCode.DownArrow))
        {
            startDecrease();
        }

        else if (EnableDisableSceneOverallScript.scene1BOn == true)
        {
            turnOffLight();
        }

        else if ((EnableDisableSceneOverallScript.timePassed > 22 && EnableDisableSceneOverallScript.timePassed < 25))
        {
            startDecreaseFast();
            fadeOutScene1C = true;
            //EnableDisableScene1Script.pointLight2.SetActive(false);
        }

        else if (EnableDisableSceneOverallScript.scene2On == true && !((EnableDisableSceneOverallScript.timePassed > 34 && EnableDisableSceneOverallScript.timePassed < 35)))
        {
            startIncreaseFast();
        }

        else if ((EnableDisableSceneOverallScript.timePassed > 34 && EnableDisableSceneOverallScript.timePassed < 35))
        {
            startDecreaseFastHard();
        }

        else if (EnableDisableSceneOverallScript.scene3On == true && !((EnableDisableSceneOverallScript.timePassed > 44 && EnableDisableSceneOverallScript.timePassed < 45)))
        {
            startIncreaseFast2();
        }

        else if ((EnableDisableSceneOverallScript.timePassed > 44 && EnableDisableSceneOverallScript.timePassed < 45))
        {
            startDecreaseFastHard2();
        }

        else if (EnableDisableSceneOverallScript.scene4AOn == true)
        {
            startIncreaseFast3();
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
