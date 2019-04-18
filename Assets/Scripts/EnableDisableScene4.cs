using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableScene4 : MonoBehaviour
{
    //game object turn on and off
    //public GameObject directionalLight;
    //public GameObject pointLight1;
    //public GameObject pointLight2;
    public GameObject bruise;
    //public GameObject pointLightLamp;
    //public GameObject AgedSitting;

    //isKinemetic turn on and off
    public GameObject doll;

    public bool bruiseStart = false;

    public GameObject gameObContainingEnableDisableSceneOverallScript;
    public GameObject gameObContainingRenderLightScript;

    void Update()
    {
        RenderLight RenderLightScript = gameObContainingRenderLightScript.GetComponent<RenderLight>();
        EnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingEnableDisableSceneOverallScript.GetComponent<EnableDisableSceneOverall>();

        if (EnableDisableSceneOverallScript.scene4AOn == true)
        {
                //RenderLightScript.renderIntensity = 1;
        }

        else if (EnableDisableSceneOverallScript.scene4BOn == true)
        {
            //destory
            //Destroy(GameObject.FindWithTag("girlAgedHappy"));

            //directionalLight.SetActive(false);
            //pointLight1.SetActive(false);
            //pointLight2.SetActive(true);

            //isKinemetic
            Rigidbody frameRigidbody = doll.GetComponent<Rigidbody>();
            frameRigidbody.isKinematic = false;
        }

        if ((EnableDisableSceneOverallScript.timePassed > 43.5 && EnableDisableSceneOverallScript.timePassed < 43.8) || Input.GetKeyDown("F"))
        {
            print("bruise start");
            bruiseStart = true;
        }
    }
}
