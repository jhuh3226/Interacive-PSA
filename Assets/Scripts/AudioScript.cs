using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

    public AudioClip musicClip1;
    public AudioSource musicSource1;

	// Use this for initialization
	void Start () {
        musicSource1.clip = musicClip1;
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.Space))
        {
            musicSource1.Play();
        }
		
	}
}
