using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableScene2 : MonoBehaviour
{
    //game object turn on and off
    public GameObject directionalLight;
    public GameObject pointLight1;
    public GameObject pointLight2;
    public GameObject childLeaning;

    //isKinemetic turn on and off
    public GameObject book;

    public GameObject gameObContainingScript;
    public GameObject gameObjectContainingScriptToEnble; //drag the gameobject which have that script you want to disable, in the inspector.

    //    
    public GameObject gameObContainingRenderLightScript;



    void Update()
    {
        RenderLight RenderLightScript = gameObContainingRenderLightScript.GetComponent<RenderLight>();
        EnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingScript.GetComponent<EnableDisableSceneOverall>();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (EnableDisableSceneOverallScript.scene2On == true)
            {
                RenderLightScript.renderIntensity = 1;
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RenderLightScript.renderIntensity = 0;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (EnableDisableSceneOverallScript.scene2On == true)
            {
                RenderLightScript.renderIntensity = 1;

                directionalLight.SetActive(false);
                pointLight1.SetActive(false);
                pointLight2.SetActive(true);
                // childLeaning.SetActive(true);

                //destory
                Destroy(GameObject.FindWithTag("girl12yearsHappy"));

                //isKinemetic
                Rigidbody frameRigidbody = book.GetComponent<Rigidbody>();
                frameRigidbody.isKinematic = false;

                FlickerLightScene2 FlickerLightScene2Script;
                FlickerLightScene2Script = gameObject.GetComponent<FlickerLightScene2>();
                FlickerLightScene2Script.enabled = true;
            }
        }


    }
}
