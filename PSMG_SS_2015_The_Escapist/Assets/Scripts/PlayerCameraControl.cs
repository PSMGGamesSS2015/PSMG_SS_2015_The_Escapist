using UnityEngine;
using System.Collections;

public class PlayerCameraControl : MonoBehaviour
{

    private float rotationSpeed = Constants.WALKING_ROTATION;
    Camera camera;
    private float leanAngle = 35f;
    private int counter = 0;
    private bool leftLeaning = false;
    private bool rightLeaning = false;
    private bool upAndDownAllowed = true;
    private float moveSidewards = 1;
    private int updatesForLeaning = 15;
    private float leaningSpeed = 5f;

    private Vector3 startPos;
    private float startTime = 0;

    private int maxLookUp = 60;
    private int maxLookDown = 300;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        camera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        leanLeftAndRight();
        lookUpAndDown();
        Debug.Log(GameObject.Find("Player").GetComponent<PlayerMovement>().playerIsGrounded());
        if (leftLeaning == true || rightLeaning == true)
        {
            freezeMovement();
        }

    }

    private void freezeMovement()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().disableMovement(true);

    }


    private void leanLeftAndRight()
    {

        if ((Input.GetButton("Lean Left") == true) && (rightLeaning == false))
        {

            if (GameObject.Find("Player").GetComponent<PlayerMovement>().playerIsGrounded() == true)
            {
                upAndDownAllowed = false;
                if (counter == 0 && leftLeaning == false)
                {
                    startPos = camera.transform.position;
                }

                leftLeaning = true;

                if (counter < updatesForLeaning)
                {
                    moveCameraLeft();
                    counter++;
                }

                leanLeft();
                startTime = Time.time;
            }



        }
        else if ((Input.GetButton("Lean Right") == true) && (leftLeaning == false))
        {
            
            if (GameObject.Find("Player").GetComponent<PlayerMovement>().playerIsGrounded() == true)
            {

                upAndDownAllowed = false;
                if (counter == 0 && rightLeaning == false)
                {
                    startPos = camera.transform.position;
                }

                rightLeaning = true;
                if (counter < updatesForLeaning)
                {
                    moveCameraRight();
                    counter++;
                }

                leanRight();
                startTime = Time.time;
            }
        }
        else
        {
            resetCamera();

        }
    }

    private void moveCameraRight()
    {
        transform.Translate(Vector3.right * Time.deltaTime);
    }

    private void moveCameraLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime);
    }

    private void resetCamera()
    {
        resetCameraPosition();

        float currentAngle = transform.rotation.eulerAngles.z;
        float targetAngle = 0f;
        float leanSpeed = leaningSpeed;

        if (currentAngle > 180)
        {
            targetAngle = 360;
        }
        float angle = Mathf.Lerp(currentAngle, targetAngle, leanSpeed * Time.deltaTime);
        Quaternion rotAngle = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
        transform.rotation = rotAngle;





    }

    private void resetCameraPosition()
    {
        if (rightLeaning == true || leftLeaning == true)
        {
            Vector3 velocity = Vector3.zero;
            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, startPos, ref velocity, 0.05f);

            if ((Time.time - startTime) > 0.75)
            {
                leftLeaning = false;
                rightLeaning = false;
                counter = 0;
                upAndDownAllowed = true;

                GameObject.Find("Player").GetComponent<PlayerMovement>().disableMovement(false);
                
            }

        }

    }

    private void leanRight()
    {
        float currentAngle = transform.rotation.eulerAngles.z;
        float targetAngle = leanAngle - 360;
        float leanSpeed = leaningSpeed;

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
        float leanSpeed = leaningSpeed;

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
        if (upAndDownAllowed == true)
        {


            if (transform.localEulerAngles.x < maxLookUp || transform.localEulerAngles.x > maxLookDown)
            {
                GetComponent<Camera>().transform.Rotate(-(Input.GetAxis("Mouse Y") * rotationSpeed), 0, 0);
            }

            if (transform.localEulerAngles.x >= maxLookUp && transform.localEulerAngles.x < maxLookDown)
            {
                if (transform.localEulerAngles.x < 180)
                {
                    if (Input.GetAxis("Mouse Y") > 0)
                    {
                        GetComponent<Camera>().transform.Rotate(-(Input.GetAxis("Mouse Y") * rotationSpeed), 0, 0);
                    }

                }
                else
                {
                    if (Input.GetAxis("Mouse Y") < 0)
                    {
                        GetComponent<Camera>().transform.Rotate(-(Input.GetAxis("Mouse Y") * rotationSpeed), 0, 0);
                    }

                }
            }

            
        }


    }

}
