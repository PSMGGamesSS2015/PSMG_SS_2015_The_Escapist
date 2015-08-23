using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Door : MonoBehaviour 
{
    public bool pullToOpen = false;
    public bool inverted = false;
    public bool deactivated = false;
    public float maxOpenAngle = 90f;

    private DoorLock doorLock;
    private LockpickSystem lockPickSystem;

    private bool locked = false;
    private bool lockPickSystemActive = false;
    private bool doorLockActive = false;

    private float defaultRotation = 0f;
    private bool focused = false;

    
    void Awake()
    {
        doorLock = GetComponentInParent<DoorLock>();
        lockPickSystem = GetComponentInParent<LockpickSystem>();

        defaultRotation = transform.eulerAngles.y;
    }

    void Start()
    {
        if (lockPickSystem && lockPickSystem.isActive())
        {
            lockPickSystemActive = true;
        }

        if (doorLock && doorLock.isActive())
        {
            doorLockActive = true;

            if (doorLock.isKeyNeeded())
            {
                lockPickSystem.deactivate();
                lockPickSystemActive = false;
            }

            else if (!lockPickSystemActive)
            {
                doorLock.setKeyNeeded(true);
            }
        }
    }


    //PUBLIC SETTER METHODS
    public void setFocused(bool b)
    {
        focused = b;

        if (deactivated) { return; }

        if (lockPickSystemActive) { lockPickSystem.setFocused(focused); }
        if (doorLockActive) { doorLock.setFocused(focused); }
    }

    public void setLocked(bool b)
    {
        locked = b;
    }

    // PUBLIC GETTER METHODS
    public bool isLocked()
    {
        return locked;
    }

    public bool isActive()
    {
        return !deactivated;
    }

    internal bool isLockPickSystemActive()
    {
        return lockPickSystemActive;
    }

    public LockpickSystem getLockPickSystem()
    {
        return lockPickSystem;
    }

    //FOR DRAG DOOR
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