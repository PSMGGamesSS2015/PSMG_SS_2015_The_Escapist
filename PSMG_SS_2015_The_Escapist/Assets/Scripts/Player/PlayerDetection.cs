using UnityEngine;
using System.Collections.Generic;

public class PlayerDetection : MonoBehaviour {

    public string layerMaskName = "Interactive";

    private Camera firstPersonCam;
    private Renderer playerRenderer;
    private float detectionRadius;
    private LayerMask interactiveObjects;

    private List<GameObject> detectedObjects;
    private GameObject centerRaycastObj = null;
    private GameObject bestFOVMatchObj = null;
    private GameObject actualFocusedObj = null;

    void Awake()
    {
        firstPersonCam = GetComponentInChildren<Camera>();
        playerRenderer = GetComponentInChildren<Renderer>();

        detectionRadius = GetComponent<SphereCollider>().radius;
        interactiveObjects = (1 << LayerMask.NameToLayer(layerMaskName));

        detectedObjects = new List<GameObject>();
    }

    void FixedUpdate()
    {
        unfocusAllObj();

        checkFocusPoint();

        if (centerRaycastObj)
        {
            Debug.Log(centerRaycastObj);
            actualFocusedObj = centerRaycastObj;
            setFocusedObject(true);
        }
        else
        {
            checkFOV();

            if (bestFOVMatchObj)
            {
                actualFocusedObj = bestFOVMatchObj;
                setFocusedObject(true);
            }
        }
        Debug.Log(actualFocusedObj);
    }


    private void checkFocusPoint()
    {
        GameObject hitObj = raycastAtFocusPoint();

        if (hitObj)
        {
            centerRaycastObj = hitObj;
            return;
        }
    }

    private void checkFOV()
    {
        if (detectedObjects.Count == 0) { return; }

        GameObject tempBestMatch = getBestMatch();

        float maxFocusAngle;
        if (tempBestMatch.tag == "Door") { maxFocusAngle = Constants.DRAG_DOOR_FOCUS_ANGLE; }
        else { maxFocusAngle = Constants.ITEM_FOCUS_ANGLE; }

        if (getAngleTo(tempBestMatch) < maxFocusAngle * 0.5f)
        {
            bestFOVMatchObj = tempBestMatch;
        }
    }

    private void setFocusedObject(bool b)
    {
        switch (actualFocusedObj.tag)
        {
            case "Door":
                Door door = actualFocusedObj.GetComponent<Door>();
                if(door) { door.setFocused(b); }
                break;

            case "Item":
                Item item = actualFocusedObj.GetComponent<Item>();
                if (item) { item.setFocused(b); }
                setHighlightPartsFocused(actualFocusedObj, b);
                break;

            case "Object":
                FocusTrigger trigger = actualFocusedObj.GetComponent<FocusTrigger>();
                if (trigger) { trigger.setFocused(b); }
                setHighlightPartsFocused(actualFocusedObj, b);
                break;

            default:
                setHighlightPartsFocused(actualFocusedObj, b);
                break;
        }
    }

    private void setHighlightPartsFocused(GameObject obj, bool b)
    {
        HighlightObject[] highlightScripts = obj.GetComponentsInChildren<HighlightObject>();
        foreach (HighlightObject highlightScript in highlightScripts)
        {
            highlightScript.setFocused(b);
        }
    }


    private GameObject raycastAtFocusPoint()
    {
        GameObject hitObj = null;

        Ray ray = firstPersonCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * detectionRadius, Color.magenta);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionRadius, interactiveObjects))
        {
            hitObj = hit.collider.gameObject;
        }

        return hitObj;
    }

    private GameObject getBestMatch()
    {
        GameObject bestMatch = null;
        float lowestAngle = 360;

        foreach (GameObject detectedObj in detectedObjects)
        {
            float tempAngle = getAngleTo(detectedObj);

            if (tempAngle < lowestAngle)
            {
                bestMatch = detectedObj;
                lowestAngle = tempAngle;
            }
        }

        return bestMatch;
    }

    private void unfocusAllObj()
    {
        if (actualFocusedObj)
        {
            setFocusedObject(false);
            actualFocusedObj = null;
        }

        centerRaycastObj = null;
        bestFOVMatchObj = null;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerMaskName))
        {
            detectedObjects.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerMaskName))
        {
            detectedObjects.Remove(other.gameObject);
        }
    }


    private float getAngleTo(GameObject target)
    {
        Vector3 targetPos = target.transform.position;
        Vector3 playerPos = transform.position;
        playerPos.y = firstPersonCam.transform.position.y;

        Vector3 direction = targetPos - playerPos;
        float angle = Vector3.Angle(direction, firstPersonCam.transform.forward);

        //Debug.Log(angle);
        return angle;
    }


    //PUBLIC GETTER METHODS
    public GameObject getObjAtFocusPoint()
    {
        return centerRaycastObj;
    }

    public GameObject getBestMatchObjInFOV()
    {
        return bestFOVMatchObj;
    }
}