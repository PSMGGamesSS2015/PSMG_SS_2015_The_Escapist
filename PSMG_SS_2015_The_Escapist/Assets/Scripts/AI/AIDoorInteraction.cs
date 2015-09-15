using UnityEngine;
using System.Collections;

public class AIDoorInteraction : MonoBehaviour {

    public float closeSpeed = 4f;

    private float lastDoorDefaultRotation;
    private GameObject lastDoor;
    private bool freezedRotation;
    private bool passedDoor = false;

    private bool pushedOpen = true;
    private float defaultRotation;
	
	void Update () 
    {
	    if (passedDoor && freezedRotation)
        {
            float angle = (pushedOpen) ? -closeSpeed : closeSpeed;

            if ((lastDoor.transform.rotation.eulerAngles.y + angle > defaultRotation && pushedOpen) || (lastDoor.transform.rotation.eulerAngles.y + angle < defaultRotation && !pushedOpen))
            {
                lastDoor.transform.Rotate(Vector3.up, angle);
            }
            else
            {
                angle = (pushedOpen) ? (defaultRotation - lastDoor.transform.rotation.eulerAngles.y) : (lastDoor.transform.rotation.eulerAngles.y - defaultRotation);
                lastDoor.transform.Rotate(Vector3.up, angle);
                passedDoor = false;
            }
        }
	}

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "Door")
        {
            Rigidbody rb = coll.gameObject.GetComponent<Rigidbody>();

            if (rb.constraints == RigidbodyConstraints.FreezeRotationY)
            {
                freezedRotation = true;
                rb.constraints = RigidbodyConstraints.None;
            }
            else
            {
                freezedRotation = false;
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Door")
        {
            if(freezedRotation)
            {
                coll.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
            }

            lastDoor = coll.gameObject;

            defaultRotation = lastDoor.GetComponent<Door>().getDefaultRotation();
            float currentRotation = lastDoor.transform.rotation.eulerAngles.y;

            if (currentRotation > defaultRotation) { pushedOpen = true; }

            passedDoor = true;
        }
    }
}
