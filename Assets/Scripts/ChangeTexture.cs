using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTexture : MonoBehaviour
{
    //Set these Textures in the Inspector
    Renderer m_Renderer;
    public Texture m_MainTexture, m_SecondTex, m_ThirdTex;

    private float decal2Alpha;

    //public bool textureChanged;
    //float currentTime;

    //public GameObject cameraRig;

    void Start()
    {
        //Fetch the Renderer from the GameObject
        m_Renderer = GetComponent<Renderer>();
        m_Renderer.material.EnableKeyword("_SecondTex");

        //textureChanged = false;
    }

    private void Update()
    {
        //Debug.Log("CurrentTime" + currentTime);
        //Debug.Log("DeltaTime" + Time.fixedTime);

        //Debug.Log(decal2Alpha);

        if (Input.GetKeyDown(KeyCode.O))
        {
            print("O key was pressed");
            m_Renderer.material.SetTexture("_SecondTex", m_SecondTex);
            //currentTime = Time.fixedTime;
        }

        else if (Input.GetKeyDown(KeyCode.T))
        {
            print("T key was pressed");
            m_Renderer.material.SetTexture("_ThirdTex", m_ThirdTex);
        }

        //if (Time.fixedTime - currentTime >= 1 && currentTime > 0)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m_MainTexture);
        //    textureChanged = true;
        //}
    }
}