using UnityEngine;
using System.Collections;

public class GuardTrigger : MonoBehaviour {

    private GameObject player;
    private GameObject enemy;
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == player)
        {
            enemy.GetComponent<FollowAI>().startGoing = true;
        }

    }
}
