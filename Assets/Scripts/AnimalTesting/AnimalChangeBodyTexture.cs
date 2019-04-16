using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalChangeBodyTexture : MonoBehaviour
{
    //Set these Textures in the Inspector
    Renderer m_Renderer;
    public Texture m_MainTexture, m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, m13, m14, m15, m16, m17;
    public bool m2TurnOn, m3TurnOn, m4TurnOn, m5TurnOn, m6TurnOn, m7TurnOn, m8TurnOn, m9TurnOn, m10TurnOn, m11TurnOn, m12TurnOn, m13TurnOn, m14TurnOn, m15TurnOn, m16TurnOn, m17TurnOn = true;

    public GameObject gameObContainingEnableDisableSceneOverallScript;
    public GameObject gameObContainingAnimalEnableDisableScene1Script;

    float timeBase = 17f;


    void Start()
    {
        //Fetch the Renderer from the GameObject
        m_Renderer = GetComponent<Renderer>();
        m_Renderer.material.EnableKeyword("_MainTex");
    }

    private void Update()
    {
        AnimalEnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingEnableDisableSceneOverallScript.GetComponent<AnimalEnableDisableSceneOverall>();
        AnimalEnableDisableScene1 AnimalEnableDisableScene1Script = gameObContainingAnimalEnableDisableScene1Script.GetComponent<AnimalEnableDisableScene1>();


        //if (EnableDisableSceneOverallScript.timePassed > timeBase && EnableDisableSceneOverallScript.timePassed > timeBase + 0.2f)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m2);
        //    m2TurnOn = false;
        //    AnimalEnableDisableScene1Script.bruise2.SetActive(false);
        //}

        //else if (EnableDisableSceneOverallScript.timePassed > timeBase + 1f && EnableDisableSceneOverallScript.timePassed > timeBase + 1.2f)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m3);
        //    AnimalEnableDisableScene1Script.bruise3.SetActive(false);
        //}

        //else if (EnableDisableSceneOverallScript.timePassed > timeBase + 2f && EnableDisableSceneOverallScript.timePassed > timeBase + 2.2f)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m4);
        //    AnimalEnableDisableScene1Script.bruise4.SetActive(false);
        //}

        //else if (EnableDisableSceneOverallScript.timePassed > timeBase + 3f && EnableDisableSceneOverallScript.timePassed > timeBase + 3.2f)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m5);
        //    AnimalEnableDisableScene1Script.bruise5.SetActive(false);
        //}

        //else if (EnableDisableSceneOverallScript.timePassed > timeBase + 4f && EnableDisableSceneOverallScript.timePassed > timeBase + 4.2f)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m6);
        //    AnimalEnableDisableScene1Script.bruise6.SetActive(false);
        //}

        //else if (EnableDisableSceneOverallScript.timePassed > timeBase + 5f && EnableDisableSceneOverallScript.timePassed > timeBase + 5.2f)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m7);
        //    AnimalEnableDisableScene1Script.bruise7.SetActive(false);
        //}

        //else if (EnableDisableSceneOverallScript.timePassed > timeBase + 6f && EnableDisableSceneOverallScript.timePassed > timeBase + 6.2f)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m8);
        //    AnimalEnableDisableScene1Script.bruise8.SetActive(false);
        //}

        //else if (EnableDisableSceneOverallScript.timePassed > timeBase + 7f && EnableDisableSceneOverallScript.timePassed > timeBase + 7.2f)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m9);
        //    AnimalEnableDisableScene1Script.bruise9.SetActive(false);
        //}

        //else if (EnableDisableSceneOverallScript.timePassed > timeBase + 8f && EnableDisableSceneOverallScript.timePassed > timeBase + 8.2f)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m10);
        //    AnimalEnableDisableScene1Script.bruise10.SetActive(false);
        //}

        //else if (EnableDisableSceneOverallScript.timePassed > timeBase + 9f && EnableDisableSceneOverallScript.timePassed > timeBase + 9.2f)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m11);
        //    AnimalEnableDisableScene1Script.bruise11.SetActive(false);
        //}

        //else if (EnableDisableSceneOverallScript.timePassed > timeBase + 10f && EnableDisableSceneOverallScript.timePassed > timeBase + 10.2f)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m12);
        //    AnimalEnableDisableScene1Script.bruise12.SetActive(false);
        //}

        //else if (EnableDisableSceneOverallScript.timePassed > timeBase + 11f && EnableDisableSceneOverallScript.timePassed > timeBase + 11.2f)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m13);
        //    AnimalEnableDisableScene1Script.bruise13.SetActive(false);
        //}

        //else if (EnableDisableSceneOverallScript.timePassed > timeBase + 12f && EnableDisableSceneOverallScript.timePassed > timeBase + 12.2f)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m14);
        //    AnimalEnableDisableScene1Script.bruise14.SetActive(false);
        //}

        //else if (EnableDisableSceneOverallScript.timePassed > timeBase + 13f && EnableDisableSceneOverallScript.timePassed > timeBase + 13.2f)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m15);
        //    AnimalEnableDisableScene1Script.bruise15.SetActive(false);
        //}

        //else if (EnableDisableSceneOverallScript.timePassed > timeBase + 14f && EnableDisableSceneOverallScript.timePassed > timeBase + 14.2f)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m16);
        //    AnimalEnableDisableScene1Script.bruise16.SetActive(false);
        //}

        //else if (EnableDisableSceneOverallScript.timePassed > timeBase + 15f && EnableDisableSceneOverallScript.timePassed > timeBase + 15.2f)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m17);
        //    AnimalEnableDisableScene1Script.bruise17.SetActive(false);
        //}

       if (AnimalEnableDisableScene1Script.bruiseAllDone == true)
        {
            m_Renderer.material.SetTexture("_MainTex", m17);
            //AnimalEnableDisableScene1Script.bruise17.SetActive(false);
            Destroy(GameObject.FindWithTag("AnimalBruise"));
        }

        //if (Time.fixedTime - currentTime >= 1 && currentTime > 0)
        //{
        //    m_Renderer.material.SetTexture("_MainTex", m_MainTexture);
        //    textureChanged = true;
        //}
    }
}
