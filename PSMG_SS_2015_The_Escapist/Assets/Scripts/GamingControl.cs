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

        
    }

    void Update()
    {
        playerHiddenPercentage = player.GetComponent<ShadowDetection>().getHiddenPercentage();

        GameObject focusedObj = player.GetComponent<PlayerDetection>().getFocusedObj();

        if (focusedObj && focusedObj.tag == "Door")
        {
            Door doorControl = focusedObj.GetComponent<Door>();
            if (doorControl.hasLockPickSystem() && doorControl.isLocked() && !doorControl.isKeyNeeded())
            {
                showLockPickingHud = true;
                lockPickingTotalLayerNum = focusedObj.GetComponentInParent<LockpickSystem>().getTotalLayerNum();
                lockPickingUnlockedLayerNum = focusedObj.GetComponentInParent<LockpickSystem>().getUnlockedLayerNum();
                Debug.Log(showLockPickingHud + " " + lockPickingTotalLayerNum + " " + lockPickingUnlockedLayerNum);
            }
            else
            {
                showLockPickingHud = false;
            }
        }

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

    public bool isSlowMovementActive()
    {
        return slowlyMovement;
    }

    public void setSlowMovement(bool active)
    {
        slowlyMovement = active;
    }
}
