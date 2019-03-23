using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLight : MonoBehaviour
{
    public Material darkSetting;
    float duration = 1.0f;
    Light lt;
    Color color0 = Color.red;
    Color color1 = Color.blue;

    // Use this for initialization
    void Start()
    {
        lt = GetComponent<Light>();
        // RenderSettings.skybox = darkSetting;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        lt.color = Color.Lerp(color0, color1, t);
    }
}
