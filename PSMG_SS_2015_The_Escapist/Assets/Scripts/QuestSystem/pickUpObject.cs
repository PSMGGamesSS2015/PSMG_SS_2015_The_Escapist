using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {

    private GameObject player;
    private Rigidbody rb;
    private Transform prevParent;
    private bool pickedUp = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        prevParent = transform.parent;
    }

    void Update()
    {
        if (pickedUp && Input.GetButtonUp("Use"))
        {
            rb.isKinematic = false;
            transform.parent = prevParent;
            rb.AddForce((player.transform.forward * 35) + (player.transform.up * 35));
            pickedUp = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player && Input.GetButtonUp("Use") && !pickedUp)
        {
            StartCoroutine("stickToPlayer");
        }
    }

    IEnumerator stickToPlayer()
    {
        rb.isKinematic = true;
        transform.position = player.transform.position + (player.transform.forward * 0.8f) + (player.transform.right * 0.5f);
        transform.parent = player.transform;
        yield return new WaitForEndOfFrame();
        pickedUp = true;
    }
}
