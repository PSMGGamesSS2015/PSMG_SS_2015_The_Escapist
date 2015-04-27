using UnityEngine;
using System.Collections;

public class GamingControl : MonoBehaviour {

    private GameObject player;
    private Vector3 currentPosition = new Vector3(0f, 0f, 0f);
    private bool sneakingActive = false;


    // Initialize the GameObject that shall deliver values.
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // All variables get their value here.
    void FixedUpdate()
    {
        currentPosition = player.GetComponent<PlayerMovement>().getPlayerPosition();
        sneakingActive = player.GetComponent<PlayerMovement>().sneakingIsActive();
    }


    //Returns the current position of the player.
    public Vector3 getPlayerPosition() {
        return currentPosition;
    }

    public bool isSneakingActive()
    {
        return sneakingActive;
    }
}
