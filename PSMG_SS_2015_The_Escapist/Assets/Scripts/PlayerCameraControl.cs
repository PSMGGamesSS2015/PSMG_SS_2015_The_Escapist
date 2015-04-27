using UnityEngine;
using System.Collections;

public class PlayerCameraControl : MonoBehaviour {

    private float rotationSpeed = Constants.WALKING_ROTATION;
    Camera camera;
    private float leanAngle = 35f;

	// Use this for initialization
	void Start () {

        Cursor.visible = false;
        camera = gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        lookUpAndDown();
        leanLeftAndRight();
        


	}

    private void leanLeftAndRight()
    {


        if (Input.GetButton("Lean Left") == true)
        {
            leanLeft();
        }
        else if (Input.GetButton("Lean Right") == true)
        {
            leanRight();
        }
        else
        {
            resetCamera();
        }
    }

    private void resetCamera()
    {
        float currentAngle = transform.rotation.eulerAngles.z;
        float targetAngle = 0f;
        float leanSpeed = 5f;

        if (currentAngle > 180)
        {
            targetAngle = 360;
        }
        float angle = Mathf.Lerp(currentAngle, targetAngle, leanSpeed * Time.deltaTime);
        Quaternion rotAngle = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
        transform.rotation = rotAngle;

    }

    private void leanRight()
    {
        float currentAngle = transform.rotation.eulerAngles.z;
        float targetAngle = leanAngle - 360;
        float leanSpeed = 3;

        if (currentAngle > 180)
        {
            targetAngle = 360 - leanAngle;
        }
        float angle = Mathf.Lerp(currentAngle, targetAngle, leanSpeed * Time.deltaTime);
        Quaternion rotAngle = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
        transform.rotation = rotAngle;

    }

    private void leanLeft()
    {
        float currentAngle = transform.rotation.eulerAngles.z;
        float targetAngle = leanAngle;
        float leanSpeed = 3;

        if (currentAngle > 180.0)
        {
            currentAngle = 360 - currentAngle;
        }
        float angle = Mathf.Lerp(currentAngle, targetAngle, leanSpeed * Time.deltaTime);
        Quaternion rotAngle = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
        transform.rotation = rotAngle;
    }

    private void lookUpAndDown()
    {
        bool upAndDownAllowed = true;
        GetComponent<Camera>().transform.Rotate(-(Input.GetAxis("Mouse Y") * rotationSpeed), 0, 0);
        
    }
}
