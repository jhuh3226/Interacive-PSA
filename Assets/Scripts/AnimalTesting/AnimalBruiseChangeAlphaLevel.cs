using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBruiseChangeAlphaLevel : MonoBehaviour
{
    //public Material topLung;
    Renderer m_Renderer;

    float newAlpha = 0;
    float lerpTime = 1f;
    float currentLerpTime = 0;
    float timeTaken;

    float alphaValue = 1f;
    float scrollSpeed = 0.3f;

    bool gettingTransparent = false;

    // Use this for initialization  
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_Renderer.material.EnableKeyword("_MainTex");
        m_Renderer.material.color = new Color(m_Renderer.material.color.r, m_Renderer.material.color.g, m_Renderer.material.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(timeTaken);

        currentLerpTime += Time.deltaTime;

        timeTaken = currentLerpTime / lerpTime;

        newAlpha = Mathf.Lerp(0, alphaValue, timeTaken);

        m_Renderer.material.color = new Color(m_Renderer.material.color.r, m_Renderer.material.color.g, m_Renderer.material.color.b, newAlpha);

        //offset value
        //float offset = Time.time * scrollSpeed;
        //m_Renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));

    }

    void changeAlpha()
    {

    }
}
