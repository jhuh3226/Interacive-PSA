using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEnableDisableSceneOverall : MonoBehaviour
{
    //game object turn on and off
    public GameObject scene1;
    //public GameObject scene2;
    //public GameObject scene3;
    //public GameObject scene4;

    public bool scene1AOn = false;
    public bool scene1BOn = false;
    public bool scene1BSmoking = false;
    public bool scene1BWalkOn = false;
    public bool scene1COn = false;
    public bool scene1CSmokeAroundMouthOn = false;
    public bool scene1CSmokeAroundNeckOn = false;
    public bool scene1CTopLungOn = false;
    public bool scene1COverallSmokeOn = false;
    public bool canvasOn = false;

    //public bool scene2On = false;
    //public bool scene3On = false;
    //public bool scene4AOn = false;
    //public bool scene4BOn = false;
    //public bool canvasOn = false;

    //timer
    public float timePassed = 0;
    float timePassedOnSpace;
    bool startTimer = false;

    void Start()
    {
        Screen.SetResolution(1280, 1920, true);
    }

    void Update()
    {        //time original
        if (Input.GetKeyUp(KeyCode.Space))
        {
            startTimer = true;
            //print("Time.deltaTime: " + Time.deltaTime);
        }

        if (startTimer == true)
        {
            timePassed += Time.deltaTime;
            print("timePassed: " + timePassed);
        }

        //scene1-a
        if ((timePassed > 0 && timePassed < 0.3))
        {
            scene1AOn = true;
        }

        //scene1-b
        else if ((timePassed > 10 && timePassed < 10.3) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            scene1AOn = false;
            scene1BOn = true;
        }

        else if ((timePassed > 11.5 && timePassed < 11.8) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            scene1AOn = false;
            scene1BOn = false;
            scene1BSmoking = true;
        }

        else if ((timePassed > 23.5 && timePassed < 23.7) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            scene1AOn = false;
            scene1BOn = false;
            scene1BSmoking = false;
            scene1BWalkOn = true;
        }

        //scene1-c
        else if ((timePassed > 24 && timePassed < 24.3) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            scene1AOn = false;
            scene1BOn = false;
            scene1COn = true;
            scene1COverallSmokeOn = true;
        }

        else if ((timePassed > 25 && timePassed < 25.3) || Input.GetKeyUp(KeyCode.M))
        {
            scene1CSmokeAroundMouthOn = true;
        }

        else if((timePassed > 33 && timePassed < 33.3) || Input.GetKeyUp(KeyCode.N))
        {
            scene1CSmokeAroundNeckOn = true;
        }

        else if((timePassed > 30 && timePassed < 30.3) || Input.GetKeyUp(KeyCode.L))
        {
            scene1CTopLungOn = true;
        }

        //canvas
        else if ((timePassed > 40 && timePassed < 40.3))
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
            //Destroy(GameObject.FindWithTag("girl4yearsHappy"));
        }


        //else if (scene2On == true)
        //{
        //    scene2.SetActive(true);

        //    //destory
        //    Destroy(GameObject.FindWithTag("scene1"));
        //    Destroy(GameObject.FindWithTag("girl4yearsSad"));

        //    //camera position
        //    GameObject.Find("Main Camera2").transform.position = new Vector3(0, 1.281f, -0.8f);
        //    GameObject.Find("Main Camera2").transform.rotation = Quaternion.Euler(0.0f, 0f, 0f);
        //    GameObject.Find("WallBackLeft").transform.rotation = Quaternion.Euler(-74.0f, 0f, 0f);
        //    GameObject.Find("WallBackRight").transform.rotation = Quaternion.Euler(-75.0f, 0f, 0f);

        //}

        //else if (scene3On == true)
        //{
        //    scene3.SetActive(true);

        //    //destory
        //    Destroy(GameObject.FindWithTag("scene2"));
        //    Destroy(GameObject.FindWithTag("girl12yearsHappy"));

        //    //camera position
        //    GameObject.Find("Main Camera2").transform.position = new Vector3(0, 1.44f, -0.8f);
        //    GameObject.Find("Main Camera2").transform.rotation = Quaternion.Euler(0.0f, 0f, 0f);
        //    GameObject.Find("WallBackLeft").transform.rotation = Quaternion.Euler(-90.0f, 0f, 0f);
        //    GameObject.Find("WallBackRight").transform.rotation = Quaternion.Euler(-90.0f, 0f, 0f);
        //}

        //else if (scene4AOn == true)
        //{
        //    scene4.SetActive(true);

        //    //destory
        //    Destroy(GameObject.FindWithTag("scene3"));
        //    Destroy(GameObject.FindWithTag("girlMiddleAgedHappy"));

        //    //camera position
        //    GameObject.Find("Main Camera2").transform.position = new Vector3(0, 1.44f, -0.8f);
        //    GameObject.Find("Main Camera2").transform.rotation = Quaternion.Euler(0.0f, 0f, 0f);
        //}

        //else if (scene4BOn == true)
        //{
        //    //destory
        //    Destroy(GameObject.FindWithTag("girlAgedHappy"));

        //    //camera position
        //}
    }
}
