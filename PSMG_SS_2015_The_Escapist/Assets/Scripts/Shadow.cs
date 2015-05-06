using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour
{

    public GameObject player;

    public GameObject reflectorOne;
    public GameObject reflectorTwo;
    public GameObject reflectorThree;

    private int visiblePercentage;
    float fieldOfViewRange = 60;
    float rayRange = 180f;

    public bool isPlayerInLight;
    public bool safe = false;

    public bool einsSichtbar;
    public bool zweiSichtbar;
    public bool dreiSichtbar;



    void Update()
    {
       
            if (Visibility(reflectorOne)) einsSichtbar = true;
            else einsSichtbar = false;
            if (Visibility(reflectorTwo)) zweiSichtbar = true;
            else zweiSichtbar = false;
            if (Visibility(reflectorThree)) dreiSichtbar = true;
            else dreiSichtbar = false;

            if (einsSichtbar)
            {
                visiblePercentage = 0; safe = false;
            }
            else if (!einsSichtbar && zweiSichtbar)
            {
                visiblePercentage = 33; safe = false;
            }
            else if (!einsSichtbar && !zweiSichtbar && dreiSichtbar)
            {
                visiblePercentage = 66; safe = true;
            }
            else
            {
                visiblePercentage = 100; safe = true;
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

        if ((Vector3.Angle(rayDirection, transform.forward)) < fieldOfViewRange)
        {
            if (Physics.Raycast(transform.position, rayDirection, out hit, rayRange))
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
