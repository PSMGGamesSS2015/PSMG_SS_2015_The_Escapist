using UnityEngine;
using System.Collections;

public class PlayerNoise : MonoBehaviour {

    public float sneakingNoiseRange = 1f;
    public float walkingNoiseRange = 8.0f;
    public float runningNoiseRange = 14.0f;

    private GamingControl gameController;
    private GameObject player;
    private Rigidbody playerRb;
    private SphereCollider noiseColl;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody>();

        noiseColl = GameObject.Find("player_noise").GetComponent<SphereCollider>();
    }

    void Update()
    {
        if (!gameController.isPlayerGrounded()) return;

        float noiseRadius = 0f;

        //Sneaking
        if (gameController.isSneakingActive() && playerRb.velocity.magnitude > 0.4f && playerRb.velocity.magnitude < 1.0f)
        {
            noiseRadius = sneakingNoiseRange;
        }

        // Walking
        else if (playerRb.velocity.magnitude > 1.0f && playerRb.velocity.magnitude < 1.5f)
        {
            noiseRadius = walkingNoiseRange;
        }

        //Running
        else if (gameController.isRunningActive() && playerRb.velocity.magnitude > 1.5f)
        {
            noiseRadius = runningNoiseRange;
        }

        noiseColl.radius = noiseRadius;
    }
}
