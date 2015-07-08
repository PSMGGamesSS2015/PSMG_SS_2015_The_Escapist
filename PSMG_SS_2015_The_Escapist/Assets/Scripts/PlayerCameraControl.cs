using UnityEngine;
using System.Collections;

public class PlayerCameraControl : MonoBehaviour
{

    private float rotationSpeed = Constants.WALKING_ROTATION;
    Camera playerCamera;
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

    /// <summary>
    /// The cursor will be set to invisible and the camera of the player will be found.
    /// </summary>
    void Start()
    {
        Cursor.visible = false;
        playerCamera = gameObject.GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        leanLeftAndRight();
        lookUpAndDown();
        if (leftLeaning == true || rightLeaning == true)
        {
            freezeMovement();
        }

    }

    /// <summary>
    /// All movement of the player will be freezed for about a second.
    /// </summary>
    private void freezeMovement()
    {
        GameObject.Find("Player").GetComponent<PlayerMovement>().disableMovement(true);

    }

    /// <summary>
    /// This method handles the leaning of the player by pressing the Key "q" or "e". That will also restricts the movement of the camera during leaning.
    /// </summary>
    private void leanLeftAndRight()
    {

        // This part is for leaning left.
        if (Input.GetButton("Lean Left") == true)
        {

            if (GameObject.Find("Player").GetComponent<PlayerMovement>().playerIsGrounded() == true)
            {
                upAndDownAllowed = false;
                if (counter == 0 && leftLeaning == false)
                {
                    startPos = playerCamera.transform.position;
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


        // This part is for leaning right.
        } else if (Input.GetButton("Lean Right") == true)
        {
            
            if (GameObject.Find("Player").GetComponent<PlayerMovement>().playerIsGrounded() == true)
            {

                upAndDownAllowed = false;
                if (counter == 0 && rightLeaning == false)
                {
                    startPos = playerCamera.transform.position;
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


         //This method resets the position of the camera to its origin before the leaning.
        } else
        {
            resetCamera();

        }
    }

    /// <summary>
    /// Moves the camera to the right side. (Does not handles the rotation!)
    /// </summary>
    private void moveCameraRight()
    {
        transform.Translate(Vector3.right * Time.deltaTime);
    }

    /// <summary>
    /// Moves the camera to the left side. (Does not handles the rotation!)
    /// </summary>
    private void moveCameraLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime);
    }


    /// <summary>
    /// This method handles the leaning of the player (rotation, not the position) to the right.
    /// </summary>
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

    /// <summary>
    /// This method handles the leaning of the player (rotation, not the position) to the left.
    /// </summary>
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

    /// <summary>
    /// This method resets the position and the rotation of the camera to the starting position before the leaning.
    /// </summary>
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

    /// <summary>
    /// The position (not the rotation) of the camera will be reset.
    /// </summary>
    private void resetCameraPosition()
    {
        if (rightLeaning == true || leftLeaning == true)
        {
            Vector3 velocity = Vector3.zero;
            playerCamera.transform.position = Vector3.SmoothDamp(playerCamera.transform.position, startPos, ref velocity, 0.05f);

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



    /// <summary>
    /// The camera movement up and down will be handled with this method. It also limits the angle up and down.
    /// </summary>
    private void lookUpAndDown()
    {
        if (upAndDownAllowed == true)
        {
            // This part handles the normal case of the camera, no limits for the movement are reached.
            if (transform.localEulerAngles.x < maxLookUp || transform.localEulerAngles.x > maxLookDown)
            {
                GetComponent<Camera>().transform.Rotate(-(Input.GetAxis("Mouse Y") * rotationSpeed), 0, 0);
            }

            // This part handles the case that the camera is reaching one of the boundaries.
            if (transform.localEulerAngles.x >= maxLookUp && transform.localEulerAngles.x < maxLookDown)
            {
                // This part is for the case the camera has reached the limit to look down.
                if (transform.localEulerAngles.x < 180)
                {
                    if (Input.GetAxis("Mouse Y") > 0)
                    {
                        GetComponent<Camera>().transform.Rotate(-(Input.GetAxis("Mouse Y") * rotationSpeed), 0, 0);
                    }

                }
                // This part is for the case the camera has reached the limit to look up.
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
