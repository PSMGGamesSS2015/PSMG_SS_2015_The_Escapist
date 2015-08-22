using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerDetection : MonoBehaviour {

    public List<GameObject> detectedObjects;
    public String layerMaskName = "Interactive";

    private GameObject bestMatch;

    void Awake()
    {
        detectedObjects = new List<GameObject>();
    }

    void Update()
    {
        if (detectedObjects.Count == 0) { return; }

        if (bestMatch) { setFocused(bestMatch, false); }

        float lowestAngle = float.MaxValue;

        foreach (GameObject obj in detectedObjects)
        {
            float tempAngle;
            if ((tempAngle = getAngleTo(obj)) < lowestAngle)
            {
                bestMatch = obj;
                lowestAngle = tempAngle;
            }
        }

        if (lowestAngle < Constants.ITEM_FOCUS_ANGLE * 0.5f) 
        {
            setFocused(bestMatch, true);
        }
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
        Debug.Log(LayerMask.NameToLayer(layerMaskName));
        if (other.gameObject.layer == LayerMask.NameToLayer(layerMaskName))
        {
            setFocused(other.gameObject, false);
            detectedObjects.Remove(other.gameObject);
        }
    }


    private void setFocused(GameObject obj, bool b)
    {
        if (obj.tag == "Door")
        {
            obj.GetComponent<Door>().setFocused(b);
        }
    }

    private float getAngleTo(GameObject target)
    {
        Vector3 targetPos = target.transform.GetComponent<Renderer>().bounds.center;
        Vector3 playerPos = transform.GetComponentInChildren<Renderer>().bounds.center;
        targetPos.y = 1;
        playerPos.y = 1;

        Vector3 direction = targetPos - playerPos;
        float angle = Vector3.Angle(direction, transform.forward);

        return angle;
    }


    public GameObject getFocusedObj()
    {
        return bestMatch;
    }
}