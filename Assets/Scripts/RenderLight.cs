using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLight : MonoBehaviour
{
    public float renderIntensity;

    // Update is called once per frame
    void Update()
    {
        RenderSettings.ambientIntensity = renderIntensity;
    }
}
