using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAudioScript1 : MonoBehaviour
{
    public AudioClip clipMonkeyHappy;
    public AudioClip clipScene1BrightBg;

    public AudioClip clipScene1GloomyBg;
    public AudioClip clipScene1LabSound;
    public AudioClip clipCage1;
    public AudioClip clipMonkeySad;
    public AudioClip clipBeepSound;

    public AudioSource audioMonkeyHappy;
    public AudioSource audioScene1BrightBg;

    public AudioSource audioScene1GloomyBg;
    public AudioSource audioScene1LabSound;
    public AudioSource audioCage1;
    public AudioSource audioMonkeySad;
    public AudioSource audioBeepSound;

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
        audioMonkeyHappy = AddAudio(clipMonkeyHappy, false, true, 0.7f);
        audioScene1BrightBg = AddAudio(clipScene1BrightBg, false, true, 0.6f);

        audioScene1GloomyBg = AddAudio(clipScene1GloomyBg, false, true, 1.0f);
        audioScene1LabSound = AddAudio(clipScene1LabSound, false, true, 0.6f);
        audioCage1 = AddAudio(clipCage1, false, true, 0.6f);
        audioMonkeySad = AddAudio(clipMonkeySad, false, true, 0.2f);
        audioBeepSound = AddAudio(clipBeepSound, false, true, 0.08f);
    }

    void PlayAudioMonkeyHappy()
    {
        audioMonkeyHappy.Play();
    }

    void PlayAudioMonkeySad()
    {
        audioMonkeySad.Play();
    }

    void PlayAudioCageHitting()
    {
        audioCage1.Play();
    }

    void PlayBeepSound()
    {
        audioBeepSound.Play();
    }

    void Update()
    {
        AnimalEnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingEnableDisableSceneOverallScript.GetComponent<AnimalEnableDisableSceneOverall>();

        //1st
        if (EnableDisableSceneOverallScript.scene1AOn == true)
        {
            if (turnOn1st == false)
            {
                audioScene1BrightBg.Play();
                Invoke("PlayAudioMonkeyHappy", 2.0f);

                turnOn1st = true;
            }
        }

        //2nd
        else if (EnableDisableSceneOverallScript.scene1BOn == true)
        {
            if (turnOn2nd == false)
            {
                audioScene1GloomyBg.Play();
                audioScene1LabSound.Play();
                Invoke("PlayAudioMonkeySad", 2.0f);
                Invoke("PlayAudioCageHitting", 5.0f);
                Invoke("PlayBeepSound", 7.0f);

                audioScene1BrightBg.Stop();
                audioMonkeyHappy.Stop();

                turnOn2nd = true;
            }
        }


        CollisionScene1 CollisionScene1Script = gameObContainingScript.GetComponent<CollisionScene1>();
        if (CollisionScene1Script.frameFallen == true)
        {
            //audioScene1frameFall.Play();
            CollisionScene1Script.frameFallen = false;
        }
    }
}
