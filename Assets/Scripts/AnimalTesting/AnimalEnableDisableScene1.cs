using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalEnableDisableScene1 : MonoBehaviour
{
    //game object turn on and off
    //public GameObject directionalLight;
    //public GameObject pointLight1;
    //public GameObject pointLight2;
    //public GameObject pointLightLamp;
    public GameObject blood;
    public GameObject bruise1, bruise2, bruise3, bruise4, bruise5, bruise6, bruise7, bruise8, bruise9, bruise10, bruise11, bruise12, bruise13, bruise14, bruise15, bruise16, bruise17;
    public bool bruiseAllDone = false;

    //public GameObject childBreathing;
    //public GameObject directLightFlicker;

    //isKinemetic turn on and off
    //public GameObject frame;
    //public GameObject ballHittingBear;

    float lerpTime = 100f;
    float currentLerpTime = 0;

    float timeBase = 26;

    //
    public GameObject gameObContainingEnableDisableSceneOverallScript;
    public GameObject gameObContainingAnimalChangeBodyTexturetScript;

    void Update()
    {

        AnimalEnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingEnableDisableSceneOverallScript.GetComponent<AnimalEnableDisableSceneOverall>();
        AnimalChangeBodyTexture AnimalChangeBodyTextureScript = gameObContainingAnimalChangeBodyTexturetScript.GetComponent<AnimalChangeBodyTexture>();


        //1B transition camera view
        if (EnableDisableSceneOverallScript.scene1BOn == true || Input.GetKeyUp(KeyCode.C))
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float step = currentLerpTime / lerpTime;

            //camera position
            GameObject.Find("Main Camera2").transform.position = Vector3.Lerp(GameObject.Find("Main Camera2").transform.position, new Vector3(0f, 0.76f, -1.04f), step);
            GameObject.Find("Main Camera2").transform.rotation = Quaternion.Lerp(GameObject.Find("Main Camera2").transform.rotation, Quaternion.Euler(0f, 0f, 0f), step);

            //directionalLight.SetActive(false);
            //pointLight1.SetActive(false);
            //directLightFlicker.SetActive(true);
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase - 1f && EnableDisableSceneOverallScript.timePassed > timeBase - 0.8f)
        {

            bruise1.SetActive(true);
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase  + 1 && EnableDisableSceneOverallScript.timePassed > timeBase + 1.2f)
        {
            //if (AnimalChangeBodyTextureScript.m2TurnOn == true)
            //{
            bruise2.SetActive(true);
            //}
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase + 3f && EnableDisableSceneOverallScript.timePassed > timeBase + 3.2f)
        {
            //if (AnimalChangeBodyTextureScript.m3TurnOn == true)
            //{
            bruise3.SetActive(true);
            //}
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase + 5f && EnableDisableSceneOverallScript.timePassed > timeBase + 5.2f)
        {
            //if (AnimalChangeBodyTextureScript.m4TurnOn == true)
            //{
            bruise4.SetActive(true);
            //}
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase + 7f && EnableDisableSceneOverallScript.timePassed > timeBase + 7.2f)
        {
            //if (AnimalChangeBodyTextureScript.m5TurnOn == true)
            //{
            bruise5.SetActive(true);
            //}
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase + 9f && EnableDisableSceneOverallScript.timePassed > timeBase + 9.2f)
        {
            //if (AnimalChangeBodyTextureScript.m6TurnOn == true)
            // {
            bruise6.SetActive(true);
            //}
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase + 5f && EnableDisableSceneOverallScript.timePassed > timeBase + 5.2f)
        {
            //if (AnimalChangeBodyTextureScript.m7TurnOn == true)
            //{
            bruise7.SetActive(true);
            // }
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase + 6f && EnableDisableSceneOverallScript.timePassed > timeBase + 6.2f)
        {
            //if (AnimalChangeBodyTextureScript.m8TurnOn == true)
            //{
            bruise8.SetActive(true);
            //}
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase + 11f && EnableDisableSceneOverallScript.timePassed > timeBase + 11.2f)
        {
            //if (AnimalChangeBodyTextureScript.m9TurnOn == true)
            //{
            bruise9.SetActive(true);
            //}
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase + 13f && EnableDisableSceneOverallScript.timePassed > timeBase + 13.2f)
        {
            // if (AnimalChangeBodyTextureScript.m10TurnOn == true)
            // {
            bruise10.SetActive(true);
            //}
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase + 15f && EnableDisableSceneOverallScript.timePassed > timeBase + 15.2f)
        {
            // if (AnimalChangeBodyTextureScript.m11TurnOn == true)
            //{
            bruise11.SetActive(true);
            //}
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase + 17f && EnableDisableSceneOverallScript.timePassed > timeBase + 17.2f)
        {
            //if (AnimalChangeBodyTextureScript.m12TurnOn == true)
            //{
            bruise12.SetActive(true);
            //}
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase + 19f && EnableDisableSceneOverallScript.timePassed > timeBase + 19.2f)
        {
            //if (AnimalChangeBodyTextureScript.m13TurnOn == true)
            //{
            bruise13.SetActive(true);
            //}
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase + 21f && EnableDisableSceneOverallScript.timePassed > timeBase + 21.2f)
        {
            //if (AnimalChangeBodyTextureScript.m14TurnOn == true)
            //{
            bruise14.SetActive(true);
            //}
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase + 23f && EnableDisableSceneOverallScript.timePassed > timeBase + 23.2f)
        {
            //if (AnimalChangeBodyTextureScript.m15TurnOn == true)
            //{
            bruise15.SetActive(true);
            //}
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase + 25f && EnableDisableSceneOverallScript.timePassed > timeBase + 25.2f)
        {
            // if (AnimalChangeBodyTextureScript.m16TurnOn == true)
            //{
            bruise16.SetActive(true);
            // }
        }

        if (EnableDisableSceneOverallScript.timePassed > timeBase + 27f && EnableDisableSceneOverallScript.timePassed > timeBase + 27.2f)
        {
            //if (AnimalChangeBodyTextureScript.m17TurnOn == true)
            //{
            bruise17.SetActive(true);
            bruiseAllDone = true;
            // }
        }

        if (EnableDisableSceneOverallScript.scene1CBlood == true)
        {
            Debug.Log("turn on blood at animalEnableDisableScene1");
            blood.SetActive(true);
        }


    }
}
