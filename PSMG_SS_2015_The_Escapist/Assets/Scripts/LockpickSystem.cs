using UnityEngine;
using System.Collections.Generic;

public class LockpickSystem : MonoBehaviour {

    public GameObject key;
    public bool keyNeeded = false;
    public int patternLength = 4;
    public int maxIdenticalRowLength = 3;

    private MainMusicController musicController;
    private AudioSource audioSource;

    private List<Directions> lockPattern;
    private enum Directions { Left, Right }

    private int actualPos = 0;
    private bool locked = true;
    private bool inPlayersRange = false;

	void Awake() 
    {
        musicController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainMusicController>();
        audioSource = GetComponent<AudioSource>();

        if (!keyNeeded) { lockPattern = createNewPattern(); }
	}

    private bool newMove(Directions dir)
    {
        bool succes = false;

        if (keyNeeded) { return false; }

        if (dir.Equals(lockPattern[actualPos]))
        {
            actualPos++;
            succes = true;
            Debug.Log("Succes");
        }
        else
        {
            actualPos = 0;
            Debug.Log("Fail");
        }

        if (actualPos == patternLength)
        {
            locked = false;
            Debug.Log("Unlocked");
        }

        return succes;
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

    private Directions getRandomDirection()
    {
        return (Random.value < 0.5) ? Directions.Left : Directions.Right;
    }

    private Directions invertDirection(Directions direction)
    {
        return (direction == Directions.Left) ? Directions.Right : Directions.Left;
    }


    // PUBLIC METHODS

    public bool moveLeft()
    {
        return newMove(Directions.Left);
    }

    public bool moveRight()
    {
        return newMove(Directions.Right);
    }

    public bool checkKey(GameObject insertedKey)
    {
        if (key == insertedKey)
        {
            locked = false;
            return true;
        }

        return false;
    }

    public bool isLocked()
    {
        return locked;
    }

    public bool isKeyNeeded()
    {
        return keyNeeded;
    }

    public bool isInPlayersRange()
    {
        return inPlayersRange;
    }

    public int getTotalLayerNum()
    {
        return patternLength;
    }

    public int getUnlockedLayerNum()
    {
        return actualPos;
    }
}
