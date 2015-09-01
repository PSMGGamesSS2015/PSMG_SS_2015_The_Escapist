using UnityEngine;
using System.Collections;

public class GamingControl : MonoBehaviour {

    public bool hairpinActive = false;

    private GameObject player;
    private PlayerInventory inventory;
    private Vector3 currentPosition = new Vector3(0f, 0f, 0f);
    private bool sneakingActive = false;
    private bool runningActive = false;
    private bool playerGrounded = true;
    private bool movementDisabled = false;
    private bool slowlyMovement = false;

    private int lockPickingTotalLayerNum;
    private int lockPickingUnlockedLayerNum;
    private bool showLockPickingHud = false;

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

        Debug.Log(playerGrounded);
    }

    void Update()
    {
        playerHiddenPercentage = player.GetComponent<ShadowDetection>().getHiddenPercentage();

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        GameObject focusedObj = player.GetComponent<PlayerDetection>().getObjAtFocusPoint();

        if (focusedObj && focusedObj.tag == "Door")
        {
            Door doorControl = focusedObj.GetComponent<Door>();

            lockPickingTotalLayerNum = doorControl.getLockPickSystem().getTotalLayerNum();
            lockPickingUnlockedLayerNum = doorControl.getLockPickSystem().getUnlockedLayerNum();

            if (doorControl.isActive() && doorControl.isLockPickSystemActive() && doorControl.isLocked() && inventory.isHairpinActive()) 
            { 
                showLockPickingHud = true; 
            }
            else 
            { 
                showLockPickingHud = false;
            }
        }
        else 
        { 
            showLockPickingHud = false;
        }

        Debug.Log(showLockPickingHud + " " + lockPickingTotalLayerNum + " " + lockPickingUnlockedLayerNum);
    }


    /// <summary>
    /// Returns the current position of the player.
    /// </summary>
    /// <returns></returns>
    public Vector3 getPlayerPosition() {
        return currentPosition;
    }

    /// <summary>
    /// Gets if the player is sneaking at the moment.
    /// </summary>
    /// <returns></returns>
    public bool isSneakingActive()
    {
        return sneakingActive;
    }

    /// <summary>
    /// Gets if the player is running at the moment.
    /// </summary>
    /// <returns></returns>
    public bool isRunningActive()
    {
        return runningActive;
    }

    /// <summary>
    /// Gets if the player is grounded at the moment.
    /// </summary>
    /// <returns></returns>
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

    public float getPlayerVisibilityFactor()
    {
        return (1 - (playerHiddenPercentage / 100));
    }

    public int getTotalLayerNumOfFocusedDoorLock()
    {
        return lockPickingTotalLayerNum;
    }

    public int getUnlockedLayerNumOfFocusedDoorLock()
    {
        return lockPickingUnlockedLayerNum;
    }

    public bool isLockPickingHudNeeded()
    {
        return showLockPickingHud;
    }

    /// <summary>
    /// Gets if the player is carrying something heavy.
    /// </summary>
    /// <returns></returns>
    public bool isSlowMovementActive()
    {
        return slowlyMovement;
    }

    /// <summary>
    /// Sets that the player is carrying something heavy.
    /// </summary>
    /// <returns></returns>
    public void setSlowMovement(bool active)
    {
        slowlyMovement = active;
    }
}
