using UnityEngine;
using System.Collections;

public class EnemyKnightAudioController : MonoBehaviour {

    public AudioClip attackSound;
    public AudioClip chasingSound;
    public AudioClip detectedSound;

    private AudioSource audioSrc;
    private Animator anim;

	// Use this for initialization
	void Start () {

        audioSrc = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {


        if (!audioSrc.isPlaying)
        {

            if (anim.GetBool("IsChasing") == true)
            {

                audioSrc.clip = chasingSound;
                audioSrc.Play();

            }
            else if (anim.GetBool("IsAttacking") == true)
            {

                audioSrc.clip = attackSound;
                audioSrc.Play();

            }
        }
	}
}
