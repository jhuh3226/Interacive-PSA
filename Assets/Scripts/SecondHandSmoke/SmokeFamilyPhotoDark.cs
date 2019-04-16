using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeFamilyPhotoDark : MonoBehaviour
{
    //public Material topLung;
    Renderer m_Renderer;

    float newAlpha;
    float lerpTime = 3f;
    float currentLerpTime = 0;
    float timeTaken;

    float alphaValue;
    float scrollSpeed = 0.3f;

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
        //Debug.Log(timeTaken);

        currentLerpTime += Time.deltaTime;

        timeTaken = currentLerpTime / lerpTime;

        newAlpha = Mathf.Lerp(255, 150, timeTaken);

        //m_Renderer.material.color = new Color(255, 255, 255);

        //Set the main Color of the Material to green
        //m_Renderer.material.shader = Shader.Find("_Color");
        m_Renderer.material.SetColor("_Color", new Color(newAlpha, newAlpha, newAlpha));

 
    }

    void changeAlpha()
    {

    }
}
