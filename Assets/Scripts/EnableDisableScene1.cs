using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableScene1 : MonoBehaviour
{
    //game object turn on and off
    public GameObject directionalLight;
    public GameObject pointLight1;
    public GameObject pointLight2;
    public GameObject pointLightLamp;
    public GameObject childBreathing;

    //isKinemetic turn on and off
    public GameObject frame;
    public GameObject ballHittingBear;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            directionalLight.SetActive(false);
            pointLight1.SetActive(false);
            pointLight2.SetActive(true);
            pointLightLamp.SetActive(true);
            childBreathing.SetActive(true);

            //destory
            Destroy(GameObject.FindWithTag("girl4yearsHappy"));

            //isKinemetic
            Rigidbody frameRigidbody = frame.GetComponent<Rigidbody>();
            frameRigidbody.isKinematic = false;

            Rigidbody ballHittingBearRigidbody = ballHittingBear.GetComponent<Rigidbody>();
            ballHittingBearRigidbody.isKinematic = false;
        }
    }

}
