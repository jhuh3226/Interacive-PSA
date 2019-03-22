using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableScene2 : MonoBehaviour
{
    //game object turn on and off
    public GameObject directionalLight;
    public GameObject pointLight1;
    public GameObject pointLight2;
    public GameObject pointLightLamp;
    public GameObject childLeaning;

    //isKinemetic turn on and off
    public GameObject frame;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            directionalLight.SetActive(false);
            pointLight1.SetActive(false);
            pointLight2.SetActive(true);
            pointLightLamp.SetActive(true);
            childLeaning.SetActive(true);

            //destory
            Destroy(GameObject.FindWithTag("girl12yearsHappy"));

            //isKinemetic
            Rigidbody frameRigidbody = frame.GetComponent<Rigidbody>();
            frameRigidbody.isKinematic = false;
        }
    }
}
