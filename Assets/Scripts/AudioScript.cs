using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip clipScene1Laugh;
    public AudioClip clipScene1BrightBg;
    public AudioClip clipScene1GloomyBg;
    public AudioClip clipScene1frameFall;

    public AudioClip clipScene1Struggle;
    public AudioClip clipScene1Tradegy;
    public AudioClip clipScene1menVoice;
    public AudioClip clipScene1Thunder;

    public AudioSource audioScene1Laugh;
    public AudioSource audioScene1BrightBg;
    public AudioSource audioScene1GloomyBg;
    public AudioSource audioScene1frameFall;

    public AudioSource audioScene1Struggle;
    public AudioSource audioScene1Tradegy;
    public AudioSource audioScene1menVoice;
    public AudioSource audioScene1Thunder;

    public GameObject gameObContainingScript;
    public GameObject gameObContainingEnableDisableSceneOverallScript;

    bool turnOn1st = false;
    bool turnOn2nd = false;
    bool turnOn3rd = false;

    public void Start()
    {
    }

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();

        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;

        return newAudio;
    }

    public void Awake()
    {
        // add the necessary AudioSources:
        audioScene1Laugh = AddAudio(clipScene1Laugh, false, true, 0.5f);
        audioScene1BrightBg = AddAudio(clipScene1BrightBg, false, true, 0.4f);
        audioScene1GloomyBg = AddAudio(clipScene1GloomyBg, false, true, 1.0f);
        audioScene1frameFall = AddAudio(clipScene1frameFall, false, true, 0.2f);

        audioScene1Struggle = AddAudio(clipScene1Struggle, false, true, 0.8f);
        audioScene1Tradegy = AddAudio(clipScene1Tradegy, false, true, 1.0f);
        audioScene1menVoice = AddAudio(clipScene1menVoice, false, true, 1.0f);
        audioScene1Thunder = AddAudio(clipScene1Thunder, false, true, 1.0f);
    }

    void PlayAudioScene1Laugh()
    {
        audioScene1Laugh.Play();
    }

    void PlayAudioScene1ManVoice()
    {
        audioScene1menVoice.Play();
    }

    void PlayAudioScene1Thunder()
    {
        audioScene1Thunder.Play();
    }

    void Update()
    {
        EnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingEnableDisableSceneOverallScript.GetComponent<EnableDisableSceneOverall>();

        //1st
        if (EnableDisableSceneOverallScript.scene1AOn == true)
        {
            if (turnOn1st == false)
            {
                audioScene1BrightBg.Play();
                Invoke("PlayAudioScene1Laugh", 2.0f);
                //Invoke("PlayAudioScene1Laugh", 7.0f);

                turnOn1st = true;
            }
        }

        //2nd
        else if (EnableDisableSceneOverallScript.scene1BOn == true)
        {
            if (turnOn2nd == false)
            {
                audioScene1Struggle.Play();
                audioScene1Tradegy.Play();
                Invoke("PlayAudioScene1ManVoice", 1.0f);
                Invoke("PlayAudioScene1Thunder", 3.0f);

                audioScene1BrightBg.Stop();
                audioScene1Laugh.Stop();

                turnOn2nd = true;
            }
        }

        //3rd
        else if (EnableDisableSceneOverallScript.scene1COn == true)
        {
            if (turnOn3rd == false)
            {
                audioScene1GloomyBg.Play();

                audioScene1Struggle.Stop();
                audioScene1Tradegy.Stop();
                audioScene1menVoice.Stop();

                turnOn3rd = true;
            }
        }

        CollisionScene1 CollisionScene1Script = gameObContainingScript.GetComponent<CollisionScene1>();
        if (CollisionScene1Script.frameFallen == true)
        {
            audioScene1frameFall.Play();
            CollisionScene1Script.frameFallen = false;
        }
    }
}
