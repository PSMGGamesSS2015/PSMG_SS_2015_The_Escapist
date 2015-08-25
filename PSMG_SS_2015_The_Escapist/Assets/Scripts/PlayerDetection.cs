using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerDetection : MonoBehaviour {

    public String layerMaskName = "Interactive";

    private Camera firstPersonCam;
    private Renderer playerRenderer;
    private float detectionRadius;
    private LayerMask interactiveObjects;

    private List<GameObject> detectedObjects;
    private GameObject focusedObj = null;
    private GameObject bestFOVMatchObj = null;

    void Awake()
    {
        firstPersonCam = GetComponentInChildren<Camera>();
        playerRenderer = GetComponentInChildren<Renderer>();

        detectionRadius = GetComponent<SphereCollider>().radius;
        interactiveObjects = (1 << LayerMask.NameToLayer(layerMaskName));

        detectedObjects = new List<GameObject>();
    }

    void Update()
    {
        unfocusAllObj();
        checkFocusPoint();

        if (focusedObj) { bestFOVMatchObj = focusedObj; return; }

        checkFOV();
    }

    private void unfocusAllObj()
    {
        if (focusedObj) 
        { 
            setFocused(focusedObj, false);
            focusedObj = null;
        }

        bestFOVMatchObj = null;
    }

    private void checkFocusPoint()
    {
        GameObject hitObj = raycastAtFocusPoint();

        if (hitObj)
        {
            focusedObj = hitObj;
            setFocused(focusedObj, true);
            return;
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

    private void checkFOV()
    {
        if (detectedObjects.Count == 0) { return; }

        GameObject tempBestMatch = getBestMatch();
        
        if (getAngleTo(tempBestMatch) < Constants.ITEM_FOCUS_ANGLE * 0.5f)
        {
            bestFOVMatchObj = tempBestMatch;
        }
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


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerMaskName))
        {
            detectedObjects.Add(other.gameObject);
            getAngleTo(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerMaskName))
        {
            setFocused(other.gameObject, false);
            detectedObjects.Remove(other.gameObject);
        }
    }


    private float getAngleTo(GameObject target)
    {
        Vector3 targetPos = target.transform.GetComponent<Renderer>().bounds.center;
        Vector3 playerPos = playerRenderer.bounds.center;
        targetPos.y = firstPersonCam.transform.position.y;
        playerPos.y = firstPersonCam.transform.position.y;
        playerPos.z -= 1;

        Vector3 direction = targetPos - playerPos;
        float angle = Vector3.Angle(direction, firstPersonCam.transform.forward);

        return angle;
    }

    private void setFocused(GameObject obj, bool b)
    {
        if (obj.tag == "Door")
        {
            obj.GetComponent<Door>().setFocused(b);
        }
    }
   

    public GameObject getObjAtFocusPoint()
    {
        return focusedObj;
    }

    public GameObject getBestMatchObjInFOV()
    {
        return bestFOVMatchObj;
    }
}