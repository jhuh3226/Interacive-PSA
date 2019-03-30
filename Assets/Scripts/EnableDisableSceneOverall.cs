using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableSceneOverall : MonoBehaviour {
    //game object turn on and off
    public GameObject scene1;
    public GameObject scene2;
    public GameObject scene3;
    public GameObject scene4;

    public bool scene1On = false;
    public bool scene2On = false;
    public bool scene3On = false;
    public bool scene4On = false;

    void Start()
    {
            Screen.SetResolution(1280, 1920, true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            scene1On = true;
            scene1.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.B))
        {
            scene2On = true;
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

        else if (Input.GetKeyDown(KeyCode.C))
        {
            scene3On = true;
            scene3.SetActive(true);

            //destory
            Destroy(GameObject.FindWithTag("scene2"));
            Destroy(GameObject.FindWithTag("girl12yearsSad"));

            //camera position
            GameObject.Find("Main Camera2").transform.position = new Vector3(0, 1.3f, -0.8f);
            GameObject.Find("Main Camera2").transform.rotation = Quaternion.Euler(10.0f, 0f, 0f);
            GameObject.Find("WallBackLeft").transform.rotation = Quaternion.Euler(-90.0f, 0f, 0f);
            GameObject.Find("WallBackRight").transform.rotation = Quaternion.Euler(-90.0f, 0f, 0f);
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            scene4On = true;
            scene4.SetActive(true);

            //destory
            Destroy(GameObject.FindWithTag("scene3"));
            Destroy(GameObject.FindWithTag("girlMiddleAgedSad"));

            //camera position
            GameObject.Find("Main Camera2").transform.position = new Vector3(0, 1.27f, -0.72f);
        }
    }
}
