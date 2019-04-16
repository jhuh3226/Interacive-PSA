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
    public GameObject manBeforeSmokingAnimation;
    public GameObject manSmokingAnimation;
    public GameObject manWalkingAnimation;

    public GameObject smokeOverall;
    public GameObject smokeAroundMouth;
    public GameObject smokeAroundBody;
    public GameObject topLung;
    public GameObject smokeAroundNeck;

    //public GameObject childBreathing;
    //public GameObject directLightFlicker;

    //isKinemetic turn on and off
    //public GameObject frame;
    //public GameObject ballHittingBear;

    float lerpTimeA = 8f;
    float lerpTimeB = 130f;
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
            if (currentLerpTime >= lerpTimeA)
            {
                currentLerpTime = lerpTimeA;
            }

            float step = currentLerpTime / lerpTimeA;
            //turn the script on and move the men
            Debug.Log("turn on man animation");
            manBeforeSmokingAnimation.SetActive(true);
            manBeforeSmokingAnimation.transform.position = Vector3.Lerp(manBeforeSmokingAnimation.transform.position, new Vector3(0.98f, 0.36f, -0.43f), step);

            //manAnimation.transform.position = Vector3.Lerp(manAnimation.transform.position, new Vector3(-3.744f, 0.36f, -0.43f), step);

            //directionalLight.SetActive(false);
            //pointLight1.SetActive(false);
            //directLightFlicker.SetActive(true);
        }

        if (EnableDisableSceneOverallScript.scene1BSmoking == true)
        {
            manBeforeSmokingAnimation.SetActive(false);
            manSmokingAnimation.SetActive(true);
        }

        if (EnableDisableSceneOverallScript.scene1BWalkOn == true)
        {
            manSmokingAnimation.SetActive(false);

            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTimeB)
            {
                currentLerpTime = lerpTimeB;
            }

            float step = currentLerpTime / lerpTimeB;
            //turn the script on and move the men
            Debug.Log("turn on man animation");
            manWalkingAnimation.SetActive(true);
            manWalkingAnimation.transform.position = Vector3.Lerp(manWalkingAnimation.transform.position, new Vector3(-2.88f, 0.36f, -0.43f), step);

            //manAnimation.transform.position = Vector3.Lerp(manAnimation.transform.position, new Vector3(-3.744f, 0.36f, -0.43f), step);

            //directionalLight.SetActive(false);
            //pointLight1.SetActive(false);
            //directLightFlicker.SetActive(true);
        }

        if (EnableDisableSceneOverallScript.scene1COn == true)
        {
            //manWalkingAnimation.SetActive(false);
        }

        if (EnableDisableSceneOverallScript.scene1COverallSmokeOn == true)
        {
            Debug.Log("turn on smokeOverall");
            smokeOverall.SetActive(true);
        }

        if (EnableDisableSceneOverallScript.scene1CSmokeAroundMouthOn == true)
        {
            smokeAroundMouth.SetActive(true);
            smokeAroundBody.SetActive(true);
        }

        if (EnableDisableSceneOverallScript.scene1CTopLungOn == true)
        {
            topLung.SetActive(true);

        }

        if (EnableDisableSceneOverallScript.scene1CSmokeAroundNeckOn == true)
        {
            smokeAroundNeck.SetActive(true);
            manWalkingAnimation.SetActive(false);
        }
    }
}
