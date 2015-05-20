using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

    public bool closeAutomatic = false;

    private GameObject player;
    private Animation openAnim;
    private float openAnimTime = 1f;
    private float waitForCloseTime = 1f;
    private float nextTriggerTime;
    private bool doorOpen = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        openAnim = GetComponent<Animation>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if (closeAutomatic) StopCoroutine("triggerCloseDoor");

            if (Input.GetButton("Use"))
            {
                if (!doorOpen) openDoor(); else closeDoor();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player && doorOpen && closeAutomatic)
        {
            StartCoroutine("triggerCloseDoor");
        }
    }

    void openDoor()
    {
        if (Time.time > nextTriggerTime)
        {
            openAnim["openDoor"].speed = 1;
            openAnim["openDoor"].time = 0;

            openAnim.Play();

            doorOpen = true;
            nextTriggerTime = Time.time + openAnimTime;
        }
    }

    void closeDoor()
    {
        if (Time.time > nextTriggerTime)
        {
            openAnim["openDoor"].speed = -1;
            openAnim["openDoor"].time = openAnim["openDoor"].length;

            GetComponent<Animation>().Play();

            doorOpen = false;
            nextTriggerTime = Time.time + openAnimTime;
        }
    }

    IEnumerator triggerCloseDoor()
    {
        float timeToWait = (Time.time < nextTriggerTime) ? (nextTriggerTime - Time.time) + waitForCloseTime : waitForCloseTime;
        yield return new WaitForSeconds(timeToWait);

        closeDoor();
    }
}
