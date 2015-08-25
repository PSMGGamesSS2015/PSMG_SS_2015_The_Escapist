using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorLock : MonoBehaviour {

	public GameObject key;
    public bool keyNeeded = false;
    public bool deactivated = false;
    
    private DoorAudioController doorAudio;
    private Door[] doorControls;

    private bool locked = true;
    private bool focused = false;

	void Awake()
    {
        doorControls = GetComponentsInChildren<Door>();
        doorAudio = GetComponentInParent<DoorAudioController>();
	}

    void Start()
    {
        if (!deactivated)
        {
            setAllChildDoorsLocked(true);
        }
    }

    void Update()
    {
        if (deactivated || !focused || !locked) { return; }

        if(Input.GetButtonDown("Use"))
        {
            if (hasPlayerKey())
            {
                locked = false;
                setAllChildDoorsLocked(false);

                doorAudio.playDoorUnlockedSound();
            }
            else if (keyNeeded)
            {
                doorAudio.playDoorLockedSound();
            }
        }
    }

    private bool hasPlayerKey()
    {
        // Lukki, lukki
        return false;
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

    private void setAllChildDoorsLocked(bool b)
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
        return !deactivated;
    }
}
