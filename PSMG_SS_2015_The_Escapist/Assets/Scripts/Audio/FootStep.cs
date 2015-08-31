using UnityEngine;
using System.Collections;

public class FootStep : MonoBehaviour 
{
    public AudioClip footStepOnStone;
    private float jumpDuration = 2f;

    private GamingControl gameController;
    private GameObject player;
    private Rigidbody playerRb;
    private AudioSource audioSrc;

    private bool jumping = false;

	void Start () 
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody>();

        audioSrc = GetComponent<AudioSource>();
	}
	
	void Update () 
    {
        if (Input.GetButtonDown("Jump") && !jumping)
        {
            StartCoroutine("startJumpTimer");
        }

        if(jumping|| audioSrc.isPlaying) { return; }

        float minVolume = 0f;
        float maxVolume = 0f;
        float pitch = 1f;

        //Sneaking
        if (gameController.isSneakingActive() && playerRb.velocity.magnitude > 0.4f && playerRb.velocity.magnitude < 1.0f)
        {
            minVolume = 0.07f;
            maxVolume = 0.09f;
            pitch = 1f;
        }

        // Walking
        else if (playerRb.velocity.magnitude > 1.0f && playerRb.velocity.magnitude < 1.5f)
        {
            minVolume = 0.1f;
            maxVolume = 0.2f;
            pitch = 1f;
        }
        
        //Running
        else if (gameController.isRunningActive() && playerRb.velocity.magnitude > 1.5f)
        {
            minVolume = 0.4f;
            maxVolume = 0.6f;
            pitch = 1.3f;
        }

        if(minVolume > 0) adjustAudioSrc(minVolume, maxVolume, pitch);
    }
        
    private void adjustAudioSrc(float minVolume, float maxVolume, float pitch)
    {
        audioSrc.clip = footStepOnStone;
        audioSrc.volume = Random.Range(minVolume, maxVolume);
        audioSrc.pitch = pitch;
        audioSrc.Play();
    }

    IEnumerator startJumpTimer()
    {
        jumping = true;
        yield return new WaitForSeconds(jumpDuration);
        jumping = false;
    }
}
