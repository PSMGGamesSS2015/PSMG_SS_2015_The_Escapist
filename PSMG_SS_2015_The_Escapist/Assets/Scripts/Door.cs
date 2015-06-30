using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Door : MonoBehaviour {

    public bool locked = false;
    public AudioClip lockedSound;
    public AudioClip openSound;
    public AudioClip closeSound;

    private GameObject player;
    private GamingControl gamingControl;
    private LockpickSystem lockpickSystem;
    private Animation openAnim;
    private AudioSource audioSource;
    private float openAnimTime = 1f;
    private float nextTriggerTime;
    private bool doorInRange = false;
    private bool doorOpen = false;
    private bool hasLockpickSystem;

    void Awake()
    {
        openAnim = GetComponent<Animation>();
        gamingControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
        lockpickSystem = GetComponent<LockpickSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (doorInRange)
        {
            if (!locked && Input.GetButtonDown("Use"))
            {
                if (!doorOpen) open(); else close();
            }

            if (locked)
            {
                if (lockpickSystem)
                {
                    if (gamingControl.hairpinActive)
                    {
                        if (Input.GetButtonDown("Move Hairpin Right")) lockpickSystem.newMove("right");
                        if (Input.GetButtonDown("Move Hairpin Left")) lockpickSystem.newMove("left");
                    }

                    else if (Input.GetButtonDown("Use"))
                    {
                        Debug.Log("This Door is locked! Press F to use your hairpin!");
                        audioSource.clip = lockedSound;
                        audioSource.Play();
                    }
                }
                else if (Input.GetButtonDown("Use"))
                {
                    Debug.Log("You need a Key for this door!");
                    audioSource.clip = lockedSound;
                    audioSource.Play();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ("Player".Equals(other.gameObject.tag))
        {
            doorInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ("Player".Equals(other.gameObject.tag))
        {
            doorInRange = false;
        }
    }

    public void open()
    {
        if (Time.time > nextTriggerTime)
        {
            foreach(AnimationState state in openAnim)
            {
                state.speed = 1;
                state.time = 0;
            }

            openAnim.Play();

            audioSource.clip = openSound;
            audioSource.Play();

            doorOpen = true;
            nextTriggerTime = Time.time + openAnimTime;
        }
    }

    public void close()
    {
        if (Time.time > nextTriggerTime)
        {
            foreach(AnimationState state in openAnim) {
	            state.speed = -1;
                state.time = state.length;
            }

            openAnim.Play();

            audioSource.clip = closeSound;
            audioSource.Play();

            doorOpen = false;
            nextTriggerTime = Time.time + openAnimTime;
        }
    }
}
