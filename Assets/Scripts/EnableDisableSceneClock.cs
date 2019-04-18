using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableSceneClock : MonoBehaviour
{
    //game object turn on and off
    public GameObject directionalLight;
    public GameObject pointLight1;
    public GameObject hour;
    public GameObject minute;
    public GameObject second;
    //
    public GameObject gameObContainingEnableDisableSceneOverallScript;
    public GameObject gameObContainingRenderLightScript;

    public float hourRotation;
    public float minuteRotation;
    public float secondRotation;

    public bool turnOnClock;

    float newAlpha = 0;
    float lerpTime = 4f;

    float currentLerpTime = 0;
    float timeTaken;

    void Update()
    {
        //print(intensity);
        //lighting increasing over time
        RenderLight RenderLightScript = gameObContainingRenderLightScript.GetComponent<RenderLight>();
        EnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingEnableDisableSceneOverallScript.GetComponent<EnableDisableSceneOverall>();

        if (turnOnClock == true)
        {
            currentLerpTime += Time.deltaTime;

            timeTaken = currentLerpTime / lerpTime;
        }

        hourRotation = Mathf.Lerp(100, 0, timeTaken);
        minuteRotation = Mathf.Lerp(800, 0, timeTaken);
        secondRotation = Mathf.Lerp(4800, 0, timeTaken);

        if (EnableDisableSceneOverallScript.sceneClockOn == true)
        {
            turnOnClock = true;
        }

        if (turnOnClock == true)
        {
            hour.transform.rotation = Quaternion.Euler(hour.transform.rotation.x, hour.transform.rotation.y, hourRotation);
            minute.transform.rotation = Quaternion.Euler(minute.transform.rotation.x, minute.transform.rotation.y, minuteRotation);
            second.transform.rotation = Quaternion.Euler(second.transform.rotation.x, second.transform.rotation.y, secondRotation);
        }
    }
}
