using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour
{

    public GameObject player;

    public GameObject frontReflector;
    public GameObject midReflector;
    public GameObject backReflector;

    private float visiblePercentage;

    public bool isPlayerInLight;
    public bool safe = false;

    public bool frontVisible;
    public bool midVisible;
    public bool backVisible;



    void Update()
    {
       
            if (Visibility(frontReflector)) frontVisible = true;
            else frontVisible = false;

            if (Visibility(midReflector)) midVisible = true;
            else midVisible = false;

            if (Visibility(backReflector)) backVisible = true;
            else backVisible = false;

            if (frontVisible)
            {
                visiblePercentage = Constants.FULL_VISIBLE; safe = false;
            }
            else if (!frontVisible && midVisible)
            {
                visiblePercentage = Constants.SLIGHTLY_COVERED; safe = false;
            }
            else if (!frontVisible && !midVisible && backVisible)
            {
                visiblePercentage = Constants.MOSTLY_COVERED; safe = true;
            }
            else
            {
                visiblePercentage = Constants.FULL_COVERED; safe = true;
            }

    }

    void OnGUI()
    {
            GUI.Label(new Rect(10, 10, 100, 256), "Versteckt: " + visiblePercentage + "%");
            GUI.Label(new Rect(10, 30, 100, 256), "Sicher: " + safe.ToString());
    }

    bool Visibility(GameObject gameObject)
    {
        RaycastHit hit;
        Vector3 rayDirection = gameObject.transform.position - transform.position;

        if (Physics.Raycast(transform.position, rayDirection, out hit))
        {
            if (hit.collider.tag == "Player")
            {

                return true;
            }
            else if (hit.transform.tag == "Enemy")
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
                    return true;
                }
                else if (hit.transform.tag == "Enemy")
                {
                    return false;
                }

            }
        }


        return false;

    }
}
