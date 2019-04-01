using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableSceneOverall : MonoBehaviour
{
    //game object turn on and off
    public GameObject scene1;
    public GameObject scene2;
    public GameObject scene3;
    public GameObject scene4;

    public bool scene1AOn = false;
    public bool scene1BOn = false;
    public bool scene1COn = false;
    public bool scene2On = false;
    public bool scene3On = false;
    public bool scene4AOn = false;
    public bool scene4BOn = false;
    public bool canvasOn = false;

    //timer
    public float timePassed = 0;

    void Start()
    {
        Screen.SetResolution(1280, 1920, true);
    }

    void Update()
    {
        //timer
        timePassed += Time.deltaTime;
        print("timePassed: " + timePassed);

        //scene1-a
        if ((timePassed > 0 && timePassed < 0.3))
        {
            scene1AOn = true;
        }

        //scene1-b
        else if ((timePassed > 10 && timePassed < 10.3))
        {
            scene1AOn = false;
            scene1BOn = true;
        }

        //scene1-c
        else if ((timePassed > 15 && timePassed < 15.3))
        {
            scene1AOn = false;
            scene1BOn = false;
            scene1COn = true;
        }

        //scene2
        else if ((timePassed > 25 && timePassed < 25.3))
        {
            scene1AOn = false;
            scene1BOn = false;
            scene1COn = false;
            scene2On = true;
        }

        //scene3
        else if ((timePassed > 35 && timePassed < 35.3))
        {
            scene1AOn = false;
            scene1BOn = false;
            scene1COn = false;
            scene2On = false;
            scene3On = true;
        }

        //scene4-a
        else if ((timePassed > 45 && timePassed < 45.3))
        {
            scene1AOn = false;
            scene1BOn = false;
            scene1COn = false;
            scene2On = false;
            scene3On = false;
            scene4AOn = true;
        }

        //scene4-b
        else if ((timePassed > 55 && timePassed < 55.3))
        {
            scene1AOn = false;
            scene1BOn = false;
            scene1COn = false;
            scene2On = false;
            scene3On = false;
            scene4AOn = false;
            scene4BOn = true;
        }

        //canvas
        else if ((timePassed > 65 && timePassed < 65.3))
        {
            canvasOn = true;
        }
        //-----------------------------------------------------------------

        if (scene1AOn == true)
        {
            scene1.SetActive(true);
        }

        else if (scene1BOn == true)
        {
            Destroy(GameObject.FindWithTag("girl4yearsHappy"));
        }


        else if (scene2On == true)
        {
            scene2.SetActive(true);

            //destory
            Destroy(GameObject.FindWithTag("scene1"));
            Destroy(GameObject.FindWithTag("girl4yearsSad"));

            //camera position
            GameObject.Find("Main Camera2").transform.position = new Vector3(0, 1.5f, -0.8f);
            GameObject.Find("Main Camera2").transform.rotation = Quaternion.Euler(16.0f, 0f, 0f);
            GameObject.Find("WallBackLeft").transform.rotation = Quaternion.Euler(-74.0f, 0f, 0f);
            GameObject.Find("WallBackRight").transform.rotation = Quaternion.Euler(-75.0f, 0f, 0f);

        }

        else if (scene3On == true)
        {
            scene3.SetActive(true);

            //destory
            Destroy(GameObject.FindWithTag("scene2"));
            Destroy(GameObject.FindWithTag("girl12yearsHappy"));

            //camera position
            GameObject.Find("Main Camera2").transform.position = new Vector3(0, 1.3f, -0.8f);
            GameObject.Find("Main Camera2").transform.rotation = Quaternion.Euler(10.0f, 0f, 0f);
            GameObject.Find("WallBackLeft").transform.rotation = Quaternion.Euler(-90.0f, 0f, 0f);
            GameObject.Find("WallBackRight").transform.rotation = Quaternion.Euler(-90.0f, 0f, 0f);
        }

        else if (scene4AOn == true)
        {
            scene4.SetActive(true);

            //destory
            Destroy(GameObject.FindWithTag("scene3"));
            Destroy(GameObject.FindWithTag("girlMiddleAgedHappy"));

            //camera position
            GameObject.Find("Main Camera2").transform.position = new Vector3(0, 1.27f, -0.72f);
        }

        else if (scene4BOn == true)
        {
            //destory
            Destroy(GameObject.FindWithTag("girlAgedHappy"));

            //camera position
            GameObject.Find("Main Camera2").transform.position = new Vector3(0, 1.27f, -0.72f);
        }
    }
}
