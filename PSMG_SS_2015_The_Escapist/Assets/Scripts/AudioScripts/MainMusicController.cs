using UnityEngine;
using System.Collections;

public class MainMusicController : MonoBehaviour {

    public AudioClip theme1;
    private AudioSource audioSrc;


	// Use this for initialization
	void Start () {

        audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!audioSrc.isPlaying)
        {
            audioSrc.clip = theme1;
            audioSrc.Play();

        }


	}
}
