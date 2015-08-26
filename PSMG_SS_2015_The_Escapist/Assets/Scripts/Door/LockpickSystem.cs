using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LockpickSystem : MonoBehaviour 
{
    public int patternLength = 4;
    public int maxIdenticalRowLength = 3;
    public float wrongMoveCooldownTime = 1f;
    public bool deactivated = false;

    private Door[] doorControls;
    private DoorAudioController doorAudio;

    private List<Directions> lockPattern;
    private enum Directions { Left, Right }

    private int actualPos = 0;
    private bool locked = true;
    private bool focused = false;
    private bool cooldownActive = false;

	void Awake() 
    {
        doorControls = GetComponentsInChildren<Door>();
        doorAudio = GetComponentInParent<DoorAudioController>();

        lockPattern = createNewPattern();
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
        if (deactivated || !focused || !locked || cooldownActive) { return; }

        if (Input.GetButtonDown("Move Hairpin Left")) { newMove(Directions.Left); }
        if (Input.GetButtonDown("Move Hairpin Right")) { newMove(Directions.Right); }
    }


    private void newMove(Directions dir)
    {
        if (dir.Equals(lockPattern[actualPos]))
        {
            actualPos++;
            doorAudio.playLockPickingSuccesSound();
            //Debug.Log("Succes");
        }
        else
        {
            actualPos = 0;
            doorAudio.playLockPickingFailSound();
            StartCoroutine("startCooldown");
            //Debug.Log("Fail");
        }

        if (actualPos == patternLength)
        {
            locked = false;
            setAllChildDoorsLocked(false);

            doorAudio.playDoorUnlockedSound();
            //Debug.Log("Unlocked");
        }
    }

    private List<Directions> createNewPattern()
    {
        List<Directions> pattern = new List<Directions>();

        Directions startDir = getRandomDirection();
        pattern.Add(startDir);

        int parityCounter = 1;

        for (int i = 1; i < patternLength; i++)
        {
            Directions newDir = getRandomDirection();
            pattern.Add(newDir);

            if (newDir == startDir) parityCounter++;
            if (parityCounter >= maxIdenticalRowLength + 1)
            {
                pattern[i] = invertDirection(pattern[i]);
                parityCounter = 0;
                startDir = pattern[i];
            }
        }

        return pattern;
    }

    IEnumerator startCooldown()
    {
        cooldownActive = true;
        yield return new WaitForSeconds (wrongMoveCooldownTime);
        cooldownActive = false;
    }

    private Directions getRandomDirection()
    {
        return (Random.value < 0.5) ? Directions.Left : Directions.Right;
    }

    private Directions invertDirection(Directions direction)
    {
        return (direction == Directions.Left) ? Directions.Right : Directions.Left;
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

    public void deactivate()
    {
        deactivated = true;
    }

    // PUBLIC GETTER METHODS
    public bool isLocked()
    {
        return locked;
    }

    public int getTotalLayerNum()
    {
        return patternLength;
    }

    public int getUnlockedLayerNum()
    {
        return actualPos;
    }

    public bool isActive()
    {
        return !deactivated;
    }
}
