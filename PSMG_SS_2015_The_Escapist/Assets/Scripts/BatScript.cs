using UnityEngine;
using System.Collections;

public class BatScript : MonoBehaviour {
    
    private GameObject bat1;
    private GameObject bat2;
    private GameObject bat3;
    private GameObject player;
    private GameObject endCollider;
    private AudioSource audioSrc;
	// Use this for initialization
    void Start()
    {
        bat1 = GameObject.Find("bat1");
        bat2 = GameObject.Find("bat2");
        bat3 = GameObject.Find("bat3");
        endCollider = GameObject.Find("Bat_Waypoint3");
        player = GameObject.FindGameObjectWithTag("Player");
        audioSrc = bat1.GetComponent<AudioSource>();

        bat1.GetComponent<FlyWayPoint>().enabled = false;
        bat2.GetComponent<FlyWayPoint>().enabled = false;
        bat3.GetComponent<FlyWayPoint>().enabled = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == player)
        {
            bat1.GetComponent<FlyWayPoint>().enabled = true;
            bat2.GetComponent<FlyWayPoint>().enabled = true;
            bat3.GetComponent<FlyWayPoint>().enabled = true;
            audioSrc.volume = 0.2f;
            audioSrc.Play();
        }


    }
	


}
