using UnityEngine;
using System.Collections;

public class ThrowItem : MonoBehaviour {

    public float forceUp = 300;
    public float forceForward = 300;
    public ParticleSystem impactFx;

    private GameObject player;
    private Rigidbody rb;
    private Transform prevParent;
    private bool pickedUp = false;
    private bool flying = false;
    public bool impact = false;

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
            rb.AddForce((player.transform.forward * forceForward) + (player.transform.up * forceUp));
            flying = true;
            pickedUp = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player && Input.GetButtonUp("Use") && !pickedUp)
        {
            StartCoroutine("stickToPlayer");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(flying && other.tag == "GROUND")
        {
            flying = false;
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            impactFx.Play();
            impact = true;
        }
    }

    IEnumerator stickToPlayer()
    {
        rb.isKinematic = true;
        transform.position = player.transform.position + (player.transform.forward * 0.8f) + (player.transform.right * 0.5f) + Vector3.up;
        transform.parent = player.transform;
        yield return new WaitForEndOfFrame();
        pickedUp = true;
    }

    public bool wasThrown()
    {
        return impact;
    }
}
