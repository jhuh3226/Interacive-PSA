using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour {

    Light lamp1Light;
    public float minWaitTime;
    public float maxWaitTime;

	// Use this for initialization
	void Start () {
        lamp1Light = GetComponent<Light>();
        StartCoroutine(Flashing());
		
	}

    IEnumerator Flashing()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            lamp1Light.enabled = !lamp1Light.enabled;

        }
    }
}
