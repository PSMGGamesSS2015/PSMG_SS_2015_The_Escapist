using UnityEngine;
using System.Collections.Generic;

public class LockpickSystem : MonoBehaviour {

    public int patternLength = 4;
    public AudioClip successSound;
    public AudioClip failSound;


    private Door door;
    private AudioSource audioSource;
    private List<Directions> pattern;
    private enum Directions { Left, Right }
    private int actualPos = 0;
    private bool locked;

	void Awake() 
    {
        Rigidbody rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>(); 
        audioSource = GetComponent<AudioSource>();
	    pattern = new List<Directions>();

        Directions startDir = (Random.value < 0.5) ? Directions.Left : Directions.Right;
        pattern.Add(startDir);
        int parityCounter = 1;

        for(int i = 1; i <= patternLength-2; i++) {
            Directions dir = (Random.value < 0.5) ? Directions.Left : Directions.Right;
            pattern.Add(dir);
            if (dir == startDir) parityCounter++;
        }

        if (parityCounter == patternLength - 2)
        {
            pattern[patternLength - 1] = (pattern[patternLength - 2] == Directions.Left) ? Directions.Right : Directions.Left;
        }
	}

    void Update()
    {
        if (Input.GetMouseButton(0)) newMove(Directions.Left);
        if (Input.GetMouseButton(1)) newMove(Directions.Right);
    }

    private void newMove(Directions dir)
    {
        if (dir.Equals(pattern[actualPos]))
        {
            actualPos++;
            audioSource.clip = successSound;
            audioSource.Play();

            //Debug.Log("Click");

            if (actualPos == patternLength)
            {
                locked = false;
                actualPos = 0;
            }
        }
        else
        {
            actualPos = 0;
            audioSource.clip = failSound;
            audioSource.Play();

            //Debug.Log("Wrong Move!");
        }
    }

    public bool isLocked()
    {
        return locked;
    }

    public int getLayerNum()
    {
        return patternLength;
    }

    public int getUnlockedLayerNum()
    {
        return actualPos + 1;
    }
}
