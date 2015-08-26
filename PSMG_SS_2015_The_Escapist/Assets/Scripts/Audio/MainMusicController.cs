using UnityEngine;
using System.Collections;

public class MainMusicController : MonoBehaviour {

    public AudioClip theme1;
    public AudioClip lockpickingSuccessSound;
    public AudioClip lockpickingFailSound;
    public AudioClip doorUnlockedSound;
    public AudioClip doorLockedSound;
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

    public AudioClip getLockpickingSuccesSound()
    {
        return lockpickingSuccessSound;
    }

    public AudioClip getLockpickingFailSound()
    {
        return lockpickingFailSound;
    }

    public AudioClip getDoorUnlockedSound()
    {
        return doorUnlockedSound;
    }

    public AudioClip getDoorLockedSound()
    {
        return doorLockedSound;
    }
}
