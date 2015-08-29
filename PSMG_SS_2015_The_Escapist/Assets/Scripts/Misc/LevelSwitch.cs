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

    // As soon as the player collides with it load the next level
    void OnCollisionEnter(Collision col)
    {
        GetComponent<AudioSource>().Play();
        Application.LoadLevel(Application.loadedLevel+1);
    }
}