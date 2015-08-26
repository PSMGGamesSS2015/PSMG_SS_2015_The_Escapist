using UnityEngine;
using System.Collections;

public class EnemyKnightAudioController : MonoBehaviour {

    public AudioClip attackSound;
    public AudioClip chasingSound1;
    public AudioClip chasingSound2;
    public AudioClip chasingSound3;
    public AudioClip chasingSound4;
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
                int index = Random.Range(1, 4);

                switch (index)
                {
                    case 1:
                        audioSrc.clip = chasingSound1;
                        break;
                    case 2:
                        audioSrc.clip = chasingSound2;
                        break;
                    case 3:
                        audioSrc.clip = chasingSound3;
                        break;
                    case 4:
                        audioSrc.clip = chasingSound4;
                        break;
                }
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
