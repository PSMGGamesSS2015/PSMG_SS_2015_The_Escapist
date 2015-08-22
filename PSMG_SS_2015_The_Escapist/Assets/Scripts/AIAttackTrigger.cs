using UnityEngine;
using System.Collections;

public class AIAttackTrigger : MonoBehaviour
{
    private GameObject player;
    private bool playerEntered = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == player) { playerEntered = true; }
    }

    public bool playerTriggered()
    {
        return playerEntered;
    }
}
