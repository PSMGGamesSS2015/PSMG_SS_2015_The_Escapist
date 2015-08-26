using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	// Use this for initialization
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().jumpDead();
        }
    }
}
