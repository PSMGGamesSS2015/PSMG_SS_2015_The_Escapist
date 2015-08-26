using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {


    //
    // Script for displaying GameOver screen of player reaches collider
    //

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().jumpDead();
        }
    }
}
