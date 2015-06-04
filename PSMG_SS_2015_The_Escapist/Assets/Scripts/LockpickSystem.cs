using UnityEngine;
using System.Collections.Generic;

public class LockpickSystem : MonoBehaviour {

    public int patternLength = 4;
    public AudioClip successSound;
    public AudioClip failSound;

    private Door door;
    private List<int> pattern;
    private AudioSource audioSource;
    private int LEFT = 0;
    private int RIGHT = 1;
    private int actualPos = 0;

	void Awake() {
        door = GetComponent<Door>();

	    pattern = new List<int>();
        for(int i = 0; i < patternLength; i++) {
            pattern.Add((int) (Random.value + 0.5));
        }

        audioSource = GetComponent<AudioSource>();
	}

    public void newMove(string direction)
    {
        if ((direction.Equals("left") && pattern[actualPos] == LEFT) || (direction.Equals("right") && pattern[actualPos] == RIGHT))
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
}
