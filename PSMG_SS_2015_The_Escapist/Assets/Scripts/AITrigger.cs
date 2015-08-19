using UnityEngine;
using System.Collections;

public class AITrigger : MonoBehaviour {

    public bool singleUse = true;

    private GameObject player;
    private bool playerEntered = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject == player) { playerEntered = true;  }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject == player && !singleUse) { playerEntered = false; }
    }

    public bool playerTriggered()
    {
        return playerEntered;
    }
}
