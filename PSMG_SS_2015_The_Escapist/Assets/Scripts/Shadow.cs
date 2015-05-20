using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour
{

    public GameObject player;

    public GameObject faceLeftReflector;
    public GameObject faceRightReflector;
    public GameObject headTopReflector;
    public GameObject shoulderLeftReflector;
    public GameObject shoulderRightReflector;
    public GameObject buttReflector;
    public GameObject leftBreastReflector;
    public GameObject rightBreastReflector;
    // public GameObject leftArmReflector;
    // public GameObject rightArmReflector;
    public GameObject waistReflector;
    public GameObject leftFootReflector;
    public GameObject rightFootReflector;

    public float visiblePercentage;

    public bool safe = false;


    public bool headInShadow;
    public bool frontInShadow;
    public bool leftSideInShadow;
    public bool rightSideInShadow;
    public bool backInShadow;


    /// <summary>
    /// Assign the actual visible status of the player using body reflectors
    /// </summary>
    void Update()
    {

        if (Covered(faceLeftReflector) && Covered(faceRightReflector) && Covered(headTopReflector)) headInShadow = true;
        else headInShadow = false;

        if (Covered(leftFootReflector) && Covered(rightFootReflector) && Covered(leftBreastReflector) &&
            Covered(rightFootReflector) && Covered(waistReflector)) frontInShadow = true;
        else frontInShadow = false;

        // if (Covered(leftArmReflector)) leftSideInShadow = true;
        //else leftSideInShadow = false;

        //            if (Covered(rightArmReflector)) rightSideInShadow = true;
        //          else rightSideInShadow = false;

        if (Covered(shoulderLeftReflector) && Covered(shoulderRightReflector) && Covered(buttReflector)) backInShadow = true;
        else backInShadow = false;

        //
        // Large InShadow Check - trying to shorten the code 
        //

        // HeadInShadow
        if (headInShadow && !frontInShadow && !backInShadow)
        {
            visiblePercentage = Constants.HEAD_COVERED; safe = false;
        }

        else if (headInShadow && frontInShadow && !backInShadow)
        {
            visiblePercentage = Constants.HEAD_COVERED + Constants.FRONT_COVERED; safe = true;
        }

        else if (headInShadow && !frontInShadow && backInShadow)
        {
            visiblePercentage = Constants.HEAD_COVERED + Constants.BACK_COVERED; safe = true;
        }

        else if (!headInShadow && frontInShadow && !backInShadow)
        {
            visiblePercentage = Constants.FRONT_COVERED; safe = false;
        }
        else if (!headInShadow && frontInShadow && backInShadow)
        {
            visiblePercentage = Constants.FRONT_COVERED + Constants.BACK_COVERED; safe = true;
        }
        else if (!headInShadow && !frontInShadow && backInShadow)
        {
            visiblePercentage = Constants.BACK_COVERED; safe = false;
        }


        // All Reflectors combined
        else if (frontInShadow && headInShadow && backInShadow)
        {
            visiblePercentage = Constants.FULL_COVERED; safe = true;
        }

        else
        {
            visiblePercentage = Constants.FULL_VISIBLE; safe = false;
        }

    }
    /// <summary>
    /// Method for GUI drawing
    /// </summary>
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 120, 256), "Versteckt: " + visiblePercentage + "%");
        GUI.Label(new Rect(10, 30, 100, 256), "Sicher: " + safe.ToString());
    }

    /// <summary>
    /// Checks whether the ray hits the committed reflector or an enemy 
    /// </summary>
    bool Covered(GameObject gameObject)
    {
        RaycastHit hit;
        Vector3 rayDirection = gameObject.transform.position - transform.position;

        if (Physics.Raycast(transform.position, rayDirection, out hit))
        {
            if (hit.collider.tag == "Player")
            {

                return false;
            }
        }

        if ((Vector3.Angle(rayDirection, transform.forward)) < Constants.FIELD_OF_VIEW_RANGE)
        {
            if (Physics.Raycast(transform.position, rayDirection, out hit, Constants.RAY_RANGE))
            {
                if (hit.collider.tag == "Player")
                {
                    return false;
                }


            }
        }


        return true;

    }
}
