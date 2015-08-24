using UnityEngine;
using System.Collections;

public class LevelSwitch : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        //Whenever you have finished Level 1 go to Level 2
        if (Application.loadedLevelName == "Level_1_Prison")
        {
            //Play a "finish level" sound
            GetComponent<AudioSource>().Play();
            //Load the next level
            Application.LoadLevel("Level_2_Canalisation");

            //Whenever you have finished level 2 go to the outro-screen
        }
        else if (Application.loadedLevelName == "Level_2_Canalisation")
        {
            //Play a "finish level" sound
            GetComponent<AudioSource>().Play();
            //Load the next level
            Application.LoadLevel("Outro");
        }
        else
        {
            //Play a "finish level" sound
            GetComponent<AudioSource>().Play();
            //Debug message, which is, hopefully, never needed
            Debug.Log("LevelSwitch: Ooops.. Normally I should not appear.. Please check the correct level order - because I wanted to load a level which does not exist (anymore?)");
        }
    }
}