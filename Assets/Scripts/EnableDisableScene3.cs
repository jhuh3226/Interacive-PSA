using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableScene3 : MonoBehaviour
{
    //game object turn on and off
    public GameObject directionalLight;
    public GameObject pointLight1;
    public GameObject pointLight2;
    //public GameObject pointLightLamp;
    //public GameObject middleAgedSitting;

    //isKinemetic turn on and off
    //public GameObject book;

    public GameObject gameObContainingEnableDisableSceneOverallScript;
    public GameObject gameObContainingRenderLightScript;

    void Update()
    {
        RenderLight RenderLightScript = gameObContainingRenderLightScript.GetComponent<RenderLight>();
        EnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingEnableDisableSceneOverallScript.GetComponent<EnableDisableSceneOverall>();

        if (EnableDisableSceneOverallScript.scene3On == true)
        {
            //RenderLightScript.renderIntensity = 1;
        }
    }
}
