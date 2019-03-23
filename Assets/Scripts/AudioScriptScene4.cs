﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScriptScene4 : MonoBehaviour
{
    public AudioClip clipScene4ChildRunning;
    public AudioClip clipScene4BabySound;
    public AudioClip clipScene4BrightBg;
    public AudioClip clipScene4GloomyBg;
    public AudioClip clipScene4DropDoll;


    public AudioSource audioScene4ChildRunning;
    public AudioSource audioScene4BabySound;
    public AudioSource audioScene4BrightBg;
    public AudioSource audioScene4GloomyBg;
    public AudioSource audioScene4DropDoll;

    // public GameObject gameObContainingScript;

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
        audioScene4ChildRunning = AddAudio(clipScene4ChildRunning, false, true, 0.5f);
        audioScene4BabySound = AddAudio(clipScene4BabySound, false, true, 0.3f);
        audioScene4BrightBg = AddAudio(clipScene4BrightBg, true, true, 0.2f);
        audioScene4GloomyBg = AddAudio(clipScene4GloomyBg, false, true, 1.0f);
        audioScene4DropDoll = AddAudio(clipScene4DropDoll, false, true, 1.0f);

    }

    void PlayAudioScene1BabySound()
    {
        audioScene4BabySound.Play();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioScene4ChildRunning.Play();
            audioScene4BrightBg.Play();
            Invoke("PlayAudioScene1Sigh", 2.0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            audioScene4GloomyBg.Play();
            audioScene4DropDoll.Play();

            audioScene4ChildRunning.Stop();
            audioScene4BabySound.Stop();
            audioScene4BrightBg.Stop();
        }

        //CollisionScene2 CollisionScene2Script = gameObContainingScript.GetComponent<CollisionScene2>();
        //if (CollisionScene2Script.bookFallen == true)
        //{
        //    audioScene2BookFall.Play();
        //    CollisionScene2Script.bookFallen = false;
        //}
    }
}
