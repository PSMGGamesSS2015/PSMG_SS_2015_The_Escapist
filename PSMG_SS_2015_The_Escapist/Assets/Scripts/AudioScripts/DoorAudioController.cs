using UnityEngine;
using System.Collections;

public class DoorAudioController : MonoBehaviour 
{
    public AudioClip lockedSound;
    public AudioClip unlockSound;
    public AudioClip lockPickingSuccesSound;
    public AudioClip lockPickingFailSound;

    private AudioSource audioSource;

    private bool unlockSoundPlayed = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void playLockPickingSuccesSound()
    {
        audioSource.clip = lockPickingSuccesSound;
        audioSource.Play();
    }

    public void playLockPickingFailSound()
    {
        audioSource.clip = lockPickingFailSound;
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