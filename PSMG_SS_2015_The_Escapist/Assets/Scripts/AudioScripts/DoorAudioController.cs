using UnityEngine;
using System.Collections;

public class DoorAudioController : MonoBehaviour 
{
    public AudioClip lockedSound;
    public AudioClip unlockSound;
    public AudioClip lockSystemSuccesSound;
    public AudioClip lockSystemFailSound;

    private AudioSource audioSource;

    private bool unlockSoundPlayed = false;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void playLockSystemSuccesSound()
    {
        audioSource.clip = lockSystemSuccesSound;
        audioSource.Play();
    }

    public void playLockSystemFailSound()
    {
        audioSource.clip = lockSystemFailSound;
        audioSource.Play();
    }

    public void playDoorLockedSound()
    {
        audioSource.clip = lockedSound;
        audioSource.Play();
    }

    public void playDoorUnlockedSound()
    {
        if (!unlockSoundPlayed)
        {
            audioSource.clip = unlockSound;
            audioSource.Play();
            unlockSoundPlayed = true;
        }
    }
}