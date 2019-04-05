using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLight : MonoBehaviour
{
    public float renderIntensity = 1;

    public float intensity;
    public float intensityMax = 1f;
    public float intensityMin = 0.4f;

    public bool startDecreasing = false;
    public bool turnOff = false;
    public bool turnOn = false;

    static float t = 0.0f;

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

        else if ((EnableDisableSceneOverallScript.timePassed > 22 && EnableDisableSceneOverallScript.timePassed <25) || Input.GetKey(KeyCode.UpArrow))
        {
            startDecreaseFast();
            EnableDisableScene1Script.pointLight2.SetActive(false);
        }

        else
        {
            turnOnLight();
        }

    }

    void startDecrease()
    {
        renderIntensity = Mathf.Lerp(intensityMin, intensityMax, t);
        t += 0.5f * Time.deltaTime;
    }

    void startDecreaseFast()
    {
        renderIntensity = Mathf.Lerp(intensityMin, intensityMax, t);
        t += 0.8f * Time.deltaTime;
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
