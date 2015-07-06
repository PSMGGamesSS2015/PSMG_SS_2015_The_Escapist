using UnityEngine;
using System.Collections;

public class FootStep : MonoBehaviour {

    private GameObject player;
    private GameObject sound;
    private SphereCollider scol;
    private GamingControl gc;
    private AudioSource audio;
    private Rigidbody rb;

    private Vector3 lastPos;

	// Use this for initialization
	void Start () {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
        player = GameObject.FindGameObjectWithTag("Player");
        sound = GameObject.Find("Sound");
        rb = player.GetComponent<Rigidbody>();
        scol = sound.GetComponent<SphereCollider>();
        audio = sound.GetComponent<AudioSource>();

        lastPos = gc.getPlayerPosition();
	}
	
	// Update is called once per frame
	void Update () {
   

        if (gc.isSneakingActive())
        {
            scol.radius = 0.36f;
        }
        else
        {
            scol.radius = 2.0f;
        }

        if (gc.isSneakingActive())
        {
            if (gc.isPlayerGrounded() && audio.isPlaying == false && rb.velocity.magnitude > 0.4f)
            {
                audio.volume = Random.Range(0.02f, 0.03f);
                audio.pitch = Random.Range(0.8f, 1.1f);
                audio.Play();
            }
        }
{
            if (gc.isPlayerGrounded() && audio.isPlaying == false && rb.velocity.magnitude > 1.0f)
            {
                audio.volume = Random.Range(0.04f, 0.06f);
                audio.pitch = Random.Range(0.8f, 1.1f);
                audio.Play();
            }
        }
	}

    
}
