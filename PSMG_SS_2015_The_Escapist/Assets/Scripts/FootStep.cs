using UnityEngine;
using System.Collections;

public class FootStep : MonoBehaviour {

    private GameObject player;
    private GameObject sound;
    private SphereCollider scol;
    private GamingControl gc;
    private AudioSource audioSrc;
    private Rigidbody rb;

    private Vector3 lastPos;

	// Use this for initialization
	void Start () {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
        player = GameObject.FindGameObjectWithTag("Player");
        sound = GameObject.Find("Sound");
        rb = player.GetComponent<Rigidbody>();
        scol = sound.GetComponent<SphereCollider>();
        audioSrc = sound.GetComponent<AudioSource>();

        lastPos = gc.getPlayerPosition();
	}
	
	// Update is called once per frame
	void Update () {


        if (gc.isSneakingActive())
        {

            scol.radius = 0.36f;
        }
        else if (gc.isRunningActive())
        {
            scol.radius = 4.0f;
        }
        else
        {

            scol.radius = 2.0f;
        }

      
            if (gc.isSneakingActive() && gc.isPlayerGrounded() && audioSrc.isPlaying == false && rb.velocity.magnitude > 0.4f && rb.velocity.magnitude < 1.0f)
            {
                audioSrc.volume = Random.Range(0.02f, 0.04f);
                audioSrc.pitch = 1f;
                audioSrc.Play();
            }


            else if (gc.isPlayerGrounded() && audioSrc.isPlaying == false && rb.velocity.magnitude > 1.0f && rb.velocity.magnitude < 1.5f)
            {
                audioSrc.volume = Random.Range(0.04f, 0.06f);
                audioSrc.pitch = 1f;
                audioSrc.Play();
            }

            else if (gc.isRunningActive() && gc.isPlayerGrounded() && audioSrc.isPlaying == false && rb.velocity.magnitude > 1.5f)
            {
                audioSrc.volume = Random.Range(0.07f, 0.1f);
                audioSrc.pitch = 1.3f;
                audioSrc.Play();
            }
        
	}

    
}
