using UnityEngine;
using System.Collections.Generic;

public class LockpickSystem : MonoBehaviour {

    public int patternLength = 4;
    public AudioClip successSound;
    public AudioClip failSound;

    private Door door;
    private AudioSource audioSource;
    private enum Directions { Left = "left", Right = "right" }
    private List<Directions> pattern;
    private int actualPos = 0;
    private bool locked;

	void Awake() 
    {
        door = GetComponent<Door>();
        audioSource = GetComponent<AudioSource>();
	    pattern = new List<Directions>();

        for(int i = 0; i < patternLength; i++) {
            Directions dir = (Random.value < 0.5) ? Directions.Left : Directions.Right;
            pattern.Add(dir);
        }
	}

    public void newMove(string direction)
    {
        if (direction.Equals(pattern[actualPos]))
        {
            actualPos++;
            audioSource.clip = successSound;
            audioSource.Play();

            Debug.Log("Click");

            if (actualPos == patternLength)
            {
                door.locked = false;
                door.open();
                actualPos = 0;
            }
        }
        else
        {
            actualPos = 0;
            audioSource.clip = failSound;
            audioSource.Play();

            Debug.Log("Wrong Move!");
        }
    }

    public bool isLocked()
    {
        return locked;
    }

}
