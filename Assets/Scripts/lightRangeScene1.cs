using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightRangeScene1 : MonoBehaviour
{
    // Pulse light's range between original range
    // and half of the original one
    float originalRange;
    float t = 0.0f;

    bool fadeOut = false;
    public GameObject gameObContainingRenderLight;

    Light lt;

    void Start()
    {
        lt = GetComponent<Light>();
        originalRange = lt.range;
    }

    void Update()
    {
        RenderLight RenderLightrScript = gameObContainingRenderLight.GetComponent<RenderLight>();

        // Set light range.
        if (RenderLightrScript.fadeOutScene1C == true)
        {
            //if (fadeOut == false)
            //{
                lt.range = Mathf.Lerp(10, 0, t);
                t += 0.15f * Time.deltaTime;
                fadeOut = true;
            //}
        }
    }
}
