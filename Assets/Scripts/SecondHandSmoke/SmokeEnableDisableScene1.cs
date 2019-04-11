using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEnableDisableScene1 : MonoBehaviour
{
    //game object turn on and off
    //public GameObject directionalLight;
    //public GameObject pointLight1;
    //public GameObject pointLight2;
    //public GameObject pointLightLamp;
    public GameObject manAnimation;
    public GameObject smokeAroundMouth;
    public GameObject topLung;
    public GameObject smokeAroundNeck;
    
    //public GameObject childBreathing;
    //public GameObject directLightFlicker;

    //isKinemetic turn on and off
    //public GameObject frame;
    //public GameObject ballHittingBear;

    float lerpTime = 100f;
    float currentLerpTime = 0;

    //
    public GameObject gameObContainingEnableDisableSceneOverallScript;
    public GameObject gameObContainingRenderLightScript;

    void Update()
    {
        SmokeRenderLight RenderLightScript = gameObContainingRenderLightScript.GetComponent<SmokeRenderLight>();
        SmokeEnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingEnableDisableSceneOverallScript.GetComponent<SmokeEnableDisableSceneOverall>();

        //1B as the transition where there is men walking
        if (EnableDisableSceneOverallScript.scene1BOn == true)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float step = currentLerpTime / lerpTime;
            //turn the script on and move the men
            Debug.Log("turn on man animation");
            manAnimation.SetActive(true);
            manAnimation.transform.position = Vector3.Lerp(manAnimation.transform.position, new Vector3(-3.744f, 0.36f, -0.43f), step);


            //directionalLight.SetActive(false);
            //pointLight1.SetActive(false);
            //directLightFlicker.SetActive(true);
        }

        else if (EnableDisableSceneOverallScript.scene1COn == true)
        {
            //startDecreasing = true;
            //RenderLightScript.turnOff = true;

            //directLightFlicker.SetActive(false);
            //pointLight1.SetActive(false);
            //pointLight2.SetActive(true);
            //pointLightLamp.SetActive(true);
            manAnimation.SetActive(false);
            smokeAroundMouth.SetActive(true);
            smokeAroundNeck.SetActive(true);
            topLung.SetActive(true);
            
            //childBreathing.SetActive(true);

            //isKinemetic
            //Rigidbody frameRigidbody = frame.GetComponent<Rigidbody>();
            //frameRigidbody.isKinematic = false;

            //Rigidbody ballHittingBearRigidbody = ballHittingBear.GetComponent<Rigidbody>();
            //ballHittingBearRigidbody.isKinematic = false;
        }
    }
}
