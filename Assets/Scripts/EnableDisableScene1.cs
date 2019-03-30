using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableScene1 : MonoBehaviour
{
    //game object turn on and off
    public GameObject directionalLight;
    public GameObject pointLight1;
    public GameObject pointLight2;
    public GameObject pointLightLamp;
    public GameObject childBreathing;
    public GameObject directLightFlicker;

    //isKinemetic turn on and off
    public GameObject frame;
    public GameObject ballHittingBear;

    //
    public GameObject gameObContainingRenderLightScript;
    public GameObject gameObContainingAvatarControllerScript;


    //light
    //public float intensity;
    //public float intensityMax = 0f;
    //public float intensityMin = 1f;
    //public bool startDecreasing = false;
    //static float t = 0.0f;

    void Update()
    {
        //print(intensity);
        //lighting increasing over time
        RenderLight RenderLightScript = gameObContainingRenderLightScript.GetComponent<RenderLight>();

        //interpolation
        AvatarController AvatarControllerScript = gameObContainingAvatarControllerScript.GetComponent<AvatarController>();

        //if (startDecreasing == true)
        //{
        //    intensity = Mathf.Lerp(intensityMax, intensityMin, t);
        //    t += 0.7f * Time.deltaTime;
        //    RenderLightScript.renderIntensity = intensity;

        //    if(intensity >=1f)
        //    {
        //        startDecreasing = false;
        //    }
        //}
        //

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
           // RenderLightScript.renderIntensity = 0;

            Destroy(GameObject.FindWithTag("girl4yearsHappy"));

            directionalLight.SetActive(false);
            directLightFlicker.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //interpolation
            AvatarControllerScript.interpolateValue = 0.2f;

            //startDecreasing = true;
            RenderLightScript.turnOff = true;

            directLightFlicker.SetActive(false);
            pointLight1.SetActive(false);
            pointLight2.SetActive(true);
            pointLightLamp.SetActive(true);
            //childBreathing.SetActive(true);

            //destory

            //isKinemetic
            Rigidbody frameRigidbody = frame.GetComponent<Rigidbody>();
            frameRigidbody.isKinematic = false;

            Rigidbody ballHittingBearRigidbody = ballHittingBear.GetComponent<Rigidbody>();
            ballHittingBearRigidbody.isKinematic = false;
        }
    }

}
