using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{

    public AudioClip clipScene1Laugh;
    public AudioClip clipScene1BrightBg;
    public AudioClip clipScene1GloomyBg;
    public AudioClip clipScene1frameFall;

    public AudioSource audioScene1Laugh;
    public AudioSource audioScene1BrightBg;
    public AudioSource audioScene1GloomyBg;
    public AudioSource audioScene1frameFall;

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
        audioScene1Laugh = AddAudio(clipScene1Laugh, false, true, 0.5f);
        audioScene1BrightBg = AddAudio(clipScene1BrightBg, false, true, 0.4f);
        audioScene1GloomyBg = AddAudio(clipScene1GloomyBg, false, true, 1.0f);
        audioScene1frameFall = AddAudio(clipScene1frameFall, false, true, 0.2f);

    }

    void PlayAudioScene1Laugh()
    {
        audioScene1Laugh.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))   
        {
            audioScene1BrightBg.Play();
            Invoke("PlayAudioScene1Laugh", 2.0f);
            Invoke("PlayAudioScene1Laugh", 5.0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            audioScene1GloomyBg.Play();

            audioScene1BrightBg.Stop();
            audioScene1Laugh.Stop();
        }

        CollisionScene1 CollisionScene1Script = gameObContainingScript.GetComponent<CollisionScene1>();
        if (CollisionScene1Script.frameFallen == true)
        {
            audioScene1frameFall.Play();
            CollisionScene1Script.frameFallen = false;
        }
    }
}
