using UnityEngine;
using System.Collections;

public class FootStep : MonoBehaviour 
{
    private GamingControl gameController;
    private GameObject player;
    private Rigidbody playerRb;
    private AudioSource audioSrc;

	void Start () 
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody>();

        audioSrc = GameObject.Find("player_noise").GetComponent<AudioSource>();
	}
	
	void Update () 
    {
       if (!gameController.isPlayerGrounded() || audioSrc.isPlaying) return;

        float minVolume = 0f;
        float maxVolume = 0f;
        float pitch = 1f;

        //Sneaking
        if (gameController.isSneakingActive() && playerRb.velocity.magnitude > 0.4f && playerRb.velocity.magnitude < 1.0f)
        {
            minVolume = 0.01f;
            maxVolume = 0.03f;
            pitch = 1f;
        }

        // Walking
        else if (playerRb.velocity.magnitude > 1.0f && playerRb.velocity.magnitude < 1.5f)
        {
            minVolume = 0.04f;
            maxVolume = 0.06f;
            pitch = 1f;
        }
        
        //Running
        else if (gameController.isRunningActive() && playerRb.velocity.magnitude > 1.5f)
        {
            minVolume = 0.07f;
            maxVolume = 0.1f;
            pitch = 1.3f;
        }

        if(minVolume > 0) adjustAudioSrc(minVolume, maxVolume, pitch);
    }
        
    private void adjustAudioSrc(float minVolume, float maxVolume, float pitch)
    {
        audioSrc.volume = Random.Range(minVolume, maxVolume);
        audioSrc.pitch = pitch;
        audioSrc.Play();
    }
}
