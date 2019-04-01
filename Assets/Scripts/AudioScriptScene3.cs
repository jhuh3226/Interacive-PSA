using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScriptScene3 : MonoBehaviour
{
    public AudioClip clipScene3Typing;
    public AudioClip clipScene3MeetingBg;
    public AudioClip clipScene3BrightBg;
    public AudioClip clipScene3GloomyBg;
    public AudioClip clipScene3Sigh;


    public AudioSource audioScene3Typing;
    public AudioSource audioScene3MeetingBg;
    public AudioSource audioScene3BrightBg;
    public AudioSource audioScene3GloomyBg;
    public AudioSource audioScene3Sigh;

    public GameObject gameObContainingEnableDisableSceneOverallScript;

    bool turnOn1st = false;

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
        audioScene3Typing = AddAudio(clipScene3Typing, false, true, 0.5f);
        audioScene3MeetingBg = AddAudio(clipScene3MeetingBg, false, true, 0.3f);
        audioScene3BrightBg = AddAudio(clipScene3BrightBg, true, true, 0.2f);
        audioScene3GloomyBg = AddAudio(clipScene3GloomyBg, false, true, 1.0f);
        audioScene3Sigh = AddAudio(clipScene3Sigh, false, true, 1.0f);

    }

    void PlayAudioScene1Sigh()
    {
        audioScene3Sigh.Play();
    }


    void Update()
    {
        EnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingEnableDisableSceneOverallScript.GetComponent<EnableDisableSceneOverall>();

        if (EnableDisableSceneOverallScript.scene3On == true)
        {
            if (turnOn1st == false)
            {
                //audioScene3BrightBg.Play();
                audioScene3Typing.Play();
                audioScene3MeetingBg.Play();

                turnOn1st = true;
            }
        }

        else if (EnableDisableSceneOverallScript.scene4AOn == true)
        {
            audioScene3Typing.Stop();
            audioScene3MeetingBg.Stop();
        }


        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    audioScene3GloomyBg.Play();
        //    Invoke("PlayAudioScene1Sigh", 2.0f);

        //    audioScene3BrightBg.Stop();
        //    audioScene3Typing.Stop();
        //    audioScene3MeetingBg.Stop();
        //}

        //CollisionScene2 CollisionScene2Script = gameObContainingScript.GetComponent<CollisionScene2>();
        //if (CollisionScene2Script.bookFallen == true)
        //{
        //    audioScene2BookFall.Play();
        //    CollisionScene2Script.bookFallen = false;
        //}
    }
}
