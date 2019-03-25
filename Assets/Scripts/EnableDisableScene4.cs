using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableScene4 : MonoBehaviour
{
    //game object turn on and off
    public GameObject directionalLight;
    public GameObject pointLight1;
    public GameObject pointLight2;
    //public GameObject pointLightLamp;
    public GameObject AgedSitting;

    //isKinemetic turn on and off
    public GameObject doll;

    public GameObject gameObContainingScript;
    public GameObject gameObContainingRenderLightScript;

    void Update()
    {
        RenderLight RenderLightScript = gameObContainingRenderLightScript.GetComponent<RenderLight>();
        EnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingScript.GetComponent<EnableDisableSceneOverall>();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (EnableDisableSceneOverallScript.scene4On == true)
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
            if (EnableDisableSceneOverallScript.scene4On == true)
            {
                directionalLight.SetActive(false);
                pointLight1.SetActive(false);
                pointLight2.SetActive(true);
                //pointLightLamp.SetActive(true);
                //AgedSitting.SetActive(true);

                //destory
                Destroy(GameObject.FindWithTag("girlAgedHappy"));

                //isKinemetic
                Rigidbody frameRigidbody = doll.GetComponent<Rigidbody>();
                frameRigidbody.isKinematic = false;
            }
        }
    }
}
