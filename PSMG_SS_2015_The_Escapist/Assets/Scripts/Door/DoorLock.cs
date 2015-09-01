using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorLock : MonoBehaviour {

	public Item key;
    public DoorBarricade[] barricades;

    public bool keyNeeded = false;
    public bool active = true;

    private PlayerInventory inventory;
    private LockpickSystem lockPickSystem;
    private DoorAudioController doorAudio;
    private Door[] doorControls;

    private bool locked = true;
    private bool focused = false;
    private bool blocked = false;

	void Awake()
    {
        doorControls = GetComponentsInChildren<Door>();
        doorAudio = GetComponent<DoorAudioController>();
        lockPickSystem = GetComponent<LockpickSystem>();

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
	}

    void Start()
    {
        if (active)
        {
            setLockedStatusOfChildDoors(true);
        }

        if ((keyNeeded || !inventory.isHairpinActive()) && lockPickSystem)
        {
            lockPickSystem.setActive(false);
        }

        if (!lockPickSystem)
        {
            keyNeeded = true;
        }
    }

    void Update()
    {
        if (!active || !focused || !locked) { return; }

        checkBarricades();

        if(Input.GetButtonDown("Use"))
        {
            if(blocked)
            {
                if (!lockPickSystem || !lockPickSystem.isLocked()) 
                { 
                    Debug.Log("Diese Tür scheint von irgendetwas blockiert zu werden.");
                    doorAudio.playDoorLockedSound();
                }
                return;
            }

            if (key && hasPlayerKey())
            {
                locked = false;
                setLockedStatusOfChildDoors(false);

                doorAudio.playDoorUnlockedSound();
            }

            else if (key && keyNeeded || !lockPickSystem || !lockPickSystem.isActive())
            {
                doorAudio.playDoorLockedSound();
            }
        }
    }

    private void checkBarricades()
    {
        if (barricades.Length == 0) { return; }

        foreach(DoorBarricade barricade in barricades)
        {
            if (barricade.getState() == 0)
            {
                lockPickSystem.setBlocked(true);
                blocked = true;
                return;
            }
        }

        lockPickSystem.setBlocked(false);
        blocked = false;
    }

    private bool hasPlayerKey()
    {
        if (inventory.getItemCount(key.name) > 0) { return true; }
        else { return false; }
    }

    private bool checkKey(GameObject insertedKey)
    {
        if (key == insertedKey)
        {
            locked = false;
            return true;
        }

        return false;
    }

    private void setLockedStatusOfChildDoors(bool b)
    {
        foreach (Door doorControl in doorControls)
        {
            doorControl.setLocked(b);
        }
    }


    // PUBLIC SETTER METHODS
    public void setFocused(bool b)
    {
        focused = b;
    }

    public void setKeyNeeded(bool b)
    {
        keyNeeded = b;
    }

    // PUBLIC GETTER METHODS
    public bool isLocked()
    {
        return locked;
    }

    public bool isKeyNeeded()
    {
        return keyNeeded;
    }

    public bool isActive()
    {
        return active;
    }
}
