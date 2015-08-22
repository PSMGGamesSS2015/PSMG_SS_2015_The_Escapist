using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Door : MonoBehaviour 
{
    public bool pullToOpen = false;
    public bool deactivated = false;
    public bool inverted = false;
    public float maxOpenAngle = 90f;

    private LockpickSystem lockSystem;
    private DoorAudioController doorAudio;

    private bool locked = false;
    private bool keyNeeded = false;
    private bool hasLockSystem = false;
    private bool unlockSoundPlayed = false;

    private float defaultRotation = 0f;
    private bool inPlayersFocus = false;

    
    void Awake()
    {
        doorAudio = GetComponentInParent<DoorAudioController>();

        if (lockSystem = GetComponentInParent<LockpickSystem>()) 
        { 
            hasLockSystem = true;
            keyNeeded = lockSystem.isKeyNeeded();
        }

        defaultRotation = transform.eulerAngles.y;
    }

    void Update()
    {
        if (deactivated) { return; }

        if (hasLockSystem) { locked = lockSystem.isLocked(); }

        if (inPlayersFocus && locked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hasLockSystem)
                {
                    if(!keyNeeded)
                    {
                        //check players inventroy for key

                        if (lockSystem.moveLeft()) { doorAudio.playLockSystemSuccesSound(); }
                        else { doorAudio.playLockSystemFailSound(); }
                    }
                    else
                    {
                        Debug.Log("Key Needed!!");
                    }
                }

                else
                {
                    doorAudio.playDoorLockedSound();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (hasLockSystem && !keyNeeded)
                {
                    if (lockSystem.moveRight()) { doorAudio.playLockSystemSuccesSound(); }
                    else { doorAudio.playLockSystemFailSound(); }
                }
            }
        }

        if (!locked && hasLockSystem && !unlockSoundPlayed)
        {
            doorAudio.playDoorUnlockedSound();
        }
    }


    //PUBLIC SETTER METHODS
    public void setFocused(bool b)
    {
        inPlayersFocus = b;
    }

    // PUBLIC GETTER METHODS
    public bool isLocked()
    {
        return locked;
    }

    public bool isDeactivated()
    {
        return deactivated;
    }

    public bool hasLockPickSystem()
    {
        return hasLockSystem;
    }

    public bool isKeyNeeded()
    {
        return keyNeeded;
    }

    public float getMaxOpenAngle()
    {
        return maxOpenAngle;
    }

    public float getDefaultRotation()
    {
        return defaultRotation;
    }

    public bool isPulledOpen()
    {
        return pullToOpen;
    }

    public bool isInverted()
    {
        return inverted;
    }
}