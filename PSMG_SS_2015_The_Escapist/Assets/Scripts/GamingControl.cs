using UnityEngine;
using System.Collections;

public class GamingControl : MonoBehaviour {

    public bool hairpinActive = false;

    private GameObject player;
    private Vector3 currentPosition = new Vector3(0f, 0f, 0f);
    private bool sneakingActive = false;
    private bool runningActive = false;
    private bool playerGrounded = true;
    private bool movementDisabled = false;

    private int playerHiddenPercentage;

    // Initialize the GameObject that shall deliver values.
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        
        //For Debugging! Finds all Audiolisteners.
        AudioListener[] myListeners = GameObject.FindObjectsOfType<AudioListener>();
        foreach (AudioListener hidden in myListeners)
        {
            //Debug.Log("Found:  " + hidden.gameObject);
        }
    }

    // All variables get their value here.
    void FixedUpdate()
    {
        currentPosition = player.GetComponent<PlayerMovement>().getPlayerPosition();
        sneakingActive = player.GetComponent<PlayerMovement>().sneakingIsActive();
        runningActive = player.GetComponent<PlayerMovement>().runningIsActive();
        playerGrounded = player.GetComponent<PlayerMovement>().isPlayerGrounded();
        movementDisabled = player.GetComponent<PlayerMovement>().isMovementDisabled();
    }

    void Update()
    {
        playerHiddenPercentage = player.GetComponent<ShadowDetection>().getHiddenPercentage();
    }


    //Returns the current position of the player.
    public Vector3 getPlayerPosition() {
        return currentPosition;
    }

    public bool isSneakingActive()
    {
        return sneakingActive;
    }

    public bool isRunningActive()
    {
        return runningActive;
    }

    public bool isPlayerGrounded()
    {
        return playerGrounded;
    }

    public bool isMovementDisabled()
    {
        return movementDisabled;
    }

    public int getPlayerHiddenPercentage()
    {
        return playerHiddenPercentage;
    }
}
