﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScriptScene2 : MonoBehaviour {

    public AudioClip clipScene2Hum;
    public AudioClip clipScene2BrightBg;
    public AudioClip clipScene2GloomyBg;
    public AudioClip clipScene2BookFall;


    public AudioSource audioScene2Hum;
    public AudioSource audioScene2BrightBg;
    public AudioSource audioScene2GloomyBg;
    public AudioSource audioScene2BookFall;

    public GameObject gameObContainingScript;

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
        audioScene2Hum = AddAudio(clipScene2Hum, false, true, 1.0f);
        audioScene2BrightBg = AddAudio(clipScene2BrightBg, true, true, 0.2f);
        audioScene2GloomyBg = AddAudio(clipScene2GloomyBg, false, true, 1.0f);
        audioScene2BookFall = AddAudio(clipScene2BookFall, false, true, 0.5f);

    }

    void PlayAudioScene1Laugh()
    {
        audioScene2Hum.Play();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioScene2BrightBg.Play();
            Invoke("PlayAudioScene1Laugh", 4.0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            audioScene2GloomyBg.Play();
            audioScene2BrightBg.Stop();
            audioScene2Hum.Stop();
        }

        CollisionScene2 CollisionScene2Script = gameObContainingScript.GetComponent<CollisionScene2>();
        if (CollisionScene2Script.bookFallen == true)
        {
            audioScene2BookFall.Play();
            CollisionScene2Script.bookFallen = false;
        }
    }
}
