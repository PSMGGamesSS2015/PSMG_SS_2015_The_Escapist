using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Camera firstPerson;
    public Camera thirdPerson;

    /// <summary>
    /// Initalisation
    /// </summary>
    void Start () {
        firstPerson = GameObject.Find("Main Camera").GetComponent<Camera>();
        thirdPerson = GameObject.Find("Camera").GetComponent<Camera>();
        firstPerson.enabled = false;
        thirdPerson.enabled = true;
	}
	
	/// <summary>
	/// Changing the camera view by pressing "1" and "2"
	/// </summary>
	void Update () {
        if (Input.GetKeyDown("2"))
        {
            firstPerson.enabled = true;
            thirdPerson.enabled = false;
        }
        else if (Input.GetKeyDown("1"))
        {
            firstPerson.enabled = false;
            thirdPerson.enabled = true;
        }
	}
}
