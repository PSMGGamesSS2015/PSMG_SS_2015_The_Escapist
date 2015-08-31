using UnityEngine;
using System.Collections;

public class PlayerNoise : MonoBehaviour {

    public float sneakingFactor = 0.07f;
    public float walkingFactor = 0.57f;
    public float runningFactor = 1f;

    private GamingControl gameController;
    private GameObject player;
    private Rigidbody playerRb;

    private float motionFactor;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        motionFactor = 0f;

        //Sneaking
        if (gameController.isSneakingActive() && playerRb.velocity.magnitude > 0.4f && playerRb.velocity.magnitude < 1.0f)
        {
            motionFactor = sneakingFactor;
        }

        // Walking
        else if (playerRb.velocity.magnitude > 1.0f && playerRb.velocity.magnitude < 1.5f)
        {
            motionFactor = walkingFactor;
        }

        //Running
        else if (gameController.isRunningActive() && playerRb.velocity.magnitude > 1.5f)
        {
            motionFactor = runningFactor;
        }
    }

    public float getNoiseFactor()
    {
        return motionFactor;
    }
}
