using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLightScene2 : MonoBehaviour {

    Light lamp2Light;
    public float minWaitTime;
    public float maxWaitTime;

    // Use this for initialization
    void Start()
    {
        lamp2Light = GetComponent<Light>();
        StartCoroutine(Flashing());

    }

    IEnumerator Flashing()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            lamp2Light.enabled = !lamp2Light.enabled;

        }
    }
}
