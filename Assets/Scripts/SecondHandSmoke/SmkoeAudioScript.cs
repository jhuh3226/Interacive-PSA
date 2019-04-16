using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmkoeAudioScript : MonoBehaviour
{
    public AudioClip clipHeyDad;
    public AudioClip clipBrightBg;

    public AudioClip clipDadSayingHey;
    public AudioClip clipFootStep;
    public AudioClip clipSmokeTurnOn;
    public AudioClip clipDadSmoking;

    public AudioClip clipGloomyGb;
    public AudioClip clipCough;
    public AudioClip clipChildSmoke1;
    public AudioClip clipChildSmoke2;
    public AudioClip clipHouseSound;

    //source
    public AudioSource audioHeyDad;
    public AudioSource audioBrightBg;

    public AudioSource audioDadSayingHey;
    public AudioSource audioFootStep;
    public AudioSource audioSmokeTurnOn;
    public AudioSource audioDadSmoking
        ;
    public AudioSource audioGloomyGb;
    public AudioSource audioCough;
    public AudioSource audioChildSmoke1;
    public AudioSource audioChildSmoke2;
    public AudioSource audioHouseSound;

    public GameObject gameObContainingScript;
    public GameObject gameObContainingEnableDisableSceneOverallScript;

    bool turnOn1st = false;
    bool turnOn2nd = false;
    bool turnOn3rd, turnOn4th, turnOn5th, turnOn6th, turnOn7th = false;

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
        audioHeyDad = AddAudio(clipHeyDad, false, true, 0.2f);
        audioBrightBg = AddAudio(clipBrightBg, false, true, 0.4f);

        audioDadSayingHey = AddAudio(clipDadSayingHey, false, true, 0.3f);
        audioFootStep = AddAudio(clipFootStep, false, true, 1.0f);
        audioSmokeTurnOn = AddAudio(clipSmokeTurnOn, false, true, 0.8f);
        audioDadSmoking = AddAudio(clipDadSmoking, false, true, 1.0f);

        audioGloomyGb = AddAudio(clipGloomyGb, false, true, 1.0f);
        audioCough = AddAudio(clipCough, false, true, 1.3f);
        audioChildSmoke1 = AddAudio(clipChildSmoke1, false, true, 1.3f);
        audioChildSmoke2 = AddAudio(clipChildSmoke2, false, true, 1.3f);
        audioHouseSound = AddAudio(clipHouseSound, false, true, 0.5f);
    }

    void PlayAaudioHeyDadh()
    {
        audioHeyDad.Play();
    }

    void PlayAudioDadSayingHey()
    {
        audioDadSayingHey.Play();
    }

    void PlayAudioFootStep()
    {
        audioFootStep.Play();
    }

    void PlayAudioSmokeTurnOn()
    {
        audioSmokeTurnOn.Play();
    }

    void PlayaudioCough()
    {
        audioCough.Play();
    }

    void Update()
    {
        SmokeEnableDisableSceneOverall EnableDisableSceneOverallScript = gameObContainingEnableDisableSceneOverallScript.GetComponent<SmokeEnableDisableSceneOverall>();

        //1st
        if (EnableDisableSceneOverallScript.scene1AOn == true)
        {
            if (turnOn1st == false)
            {
                audioBrightBg.Play();
                audioHouseSound.Play();

                turnOn1st = true;
            }
        }

        //2nd
        else if (EnableDisableSceneOverallScript.scene1BOn == true)
        {
            if (turnOn2nd == false)
            {
                Invoke("PlayAudioFootStep", 0.0f);
                Invoke("PlayAaudioHeyDadh", 2.0f);
                turnOn2nd = true;
            }
        }

        else if (EnableDisableSceneOverallScript.scene1BSmoking == true)
        {
            if (turnOn3rd == false)
            {
                Invoke("PlayAudioDadSayingHey", 1.5f);
                Invoke("PlayAudioSmokeTurnOn", 7.5f);
                turnOn3rd = true;
            }
        }

        else if (EnableDisableSceneOverallScript.scene1BWalkOn == true)
        {
            if (turnOn4th == false)
            {
                Invoke("PlayAudioFootStep", 0.0f);
                turnOn4th = true;
            }
        }

        if (EnableDisableSceneOverallScript.scene1COn == true)
        {
            if (turnOn5th == false)
            {
                audioGloomyGb.Play();

                audioBrightBg.Stop();
                turnOn5th = true;
            }
        }

        if (EnableDisableSceneOverallScript.scene1CSmokeAroundMouthOn == true)
        {
            if (turnOn6th == false)
            {
                audioChildSmoke1.Play();

                turnOn6th = true;
            }
        }

        if (EnableDisableSceneOverallScript.scene1CTopLungOn == true)
        {
            if (turnOn7th == false)
            {
                Invoke("PlayaudioCough", 0.0f);
                Invoke("PlayaudioCough", 4f);
                Invoke("PlayaudioCough", 8f);

                turnOn7th = true;
            }
        }

        //CollisionScene1 CollisionScene1Script = gameObContainingScript.GetComponent<CollisionScene1>();
        //if (CollisionScene1Script.frameFallen == true)
        //{
        //    //audioScene1frameFall.Play();
        //    CollisionScene1Script.frameFallen = false;
        //}
    }
}
