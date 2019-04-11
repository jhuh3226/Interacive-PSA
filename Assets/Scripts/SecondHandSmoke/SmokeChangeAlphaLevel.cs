using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeChangeAlphaLevel : MonoBehaviour
{
    //public Material topLung;
    Renderer m_Renderer;

    float newAlpha;
    float lerpTime = 2f;
    float currentLerpTime = 0;
    float timeTaken;

    bool gettingTransparent = false;

    // Use this for initialization  
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_Renderer.material.EnableKeyword("_MainTex");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    Color color = topLung.color;
        //    color.a = 0;
        //    //color.a += 1f * Time.deltaTime;
        //    topLung.color = color;
        //    print(color.a);
        //}

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //print("key was pressed");
        //m_Renderer.material.SetTexture("_MainTex", m_15);
        //currentTime = Time.fixedTime;

        //Debug.Log("1: " + currentLerpTime + " 2: " + Time.deltaTime + " 3: " + timeTaken);

        currentLerpTime += Time.deltaTime;
        if (currentLerpTime >= lerpTime)
        {
            currentLerpTime = 0;
            //currentLerpTime = lerpTime;
        }


        timeTaken = currentLerpTime / lerpTime;

        if (gettingTransparent == false)
        {
            newAlpha = Mathf.Lerp(0, 1, timeTaken);
            if(currentLerpTime == 0)
            {
                gettingTransparent = true;
            }
        }

        else if (gettingTransparent == true)
        {
            newAlpha = Mathf.Lerp(1, 0, timeTaken);
            if (currentLerpTime == 0)
            {
                gettingTransparent = false;
            }
        }
        //else if (newAlpha == 1)
        //{
        //    gettingTransparent = true;
        //}

        //else if(gettingTransparent == true)
        //{
        //    newAlpha = Mathf.Lerp(1, 0, timeTaken);
        //}

        m_Renderer.material.color = new Color(m_Renderer.material.color.r, m_Renderer.material.color.g, m_Renderer.material.color.b, newAlpha);
        //}

    }

    void changeAlpha()
    {

    }
}
