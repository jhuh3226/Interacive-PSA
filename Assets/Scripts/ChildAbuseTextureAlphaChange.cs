using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAbuseTextureAlphaChange : MonoBehaviour
{
    //public Material topLung;
    Renderer m_Renderer;

    float newAlpha = 0;
    float lerpTime = 10f;
    float currentLerpTime = 0;
    float timeTaken;

    //public float alphaValue = 1;
    float scrollSpeed = 0.3f;


    public GameObject gameObContainingEnableDisableSceneOverallScript;

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
            print("bruise changing");
            currentLerpTime += Time.deltaTime;

            timeTaken = currentLerpTime / lerpTime;

            newAlpha = Mathf.Lerp(0, 1, timeTaken);

            m_Renderer.material.color = new Color(m_Renderer.material.color.r, m_Renderer.material.color.g, m_Renderer.material.color.b, newAlpha);
    }

    void changeAlpha()
    {

    }
}
