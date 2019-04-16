using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCanvasAppear : MonoBehaviour
{
    public Canvas UICanvas;

    //public float intensity;
    //public float intensityMax = 1.0f;
    //public float intensityMin = 0.4f;

    public bool startDecreasing = false;

    //static float t = 0.0f;
    //public GameObject gameObContainingRenderLightScript;
    public GameObject gameObContainingEnableDisableSceneOverallScript;

    bool canvasTurnedOn = false;

    // Use this for initialization
    void Start()
    {
        UICanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //RenderLight RenderLightScript = gameObContainingRenderLightScript.GetComponent<RenderLight>();
        AnimalEnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingEnableDisableSceneOverallScript.GetComponent<AnimalEnableDisableSceneOverall>();

        if (EnableDisableSceneOverallScript.canvasOn == true)
        {
            if (canvasTurnedOn == false)
            {
                //startDecreasing = true;
                UICanvas.enabled = true;
                print(UICanvas.enabled);

                canvasTurnedOn = true;
            }
        }

        //if (startDecreasing == true)
        //{
        //    intensity = Mathf.Lerp(intensityMax, intensityMin, t);
        //    t += 0.5f * Time.deltaTime;

        //    RenderLightScript.renderIntensity = intensity;
        //}
    }

}
