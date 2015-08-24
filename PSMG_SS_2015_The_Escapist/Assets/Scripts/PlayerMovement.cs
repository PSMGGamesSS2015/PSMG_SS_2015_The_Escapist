using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody rb;
    private float movementSpeed;
    private float rotationSpeed;
    public bool sneaking = false;
    public bool running = false;
    private bool movementDisabled = false;
    private float turn;
    private float moveVertical;
    public bool playerGrounded = true;

    private Animator anim;

    private float rotationFactor = Constants.MOUSE_SENSITIVITY;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 1F;
    public float sensitivityY = 1F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;

    Quaternion rotation;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        

    }



    void FixedUpdate()
    {
        turn = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        sneaking = checkSneakMode();
        running = checkRunningMode();

        // All movement will be done here.
        if (movementDisabled == false)
        {
            manageMovement(turn, moveVertical, sneaking);
        }

        
    }



    /// <summary>
    /// This method checks if the running button has been pressed. Depending on which button has been pressed, it will return true or false. It also modifiys the size of the player.
    /// </summary>
    /// <returns>Bool: Running mode active or not.</returns>
    private bool checkRunningMode()
    {

        if ((Input.GetButton("Run") == true) && (Input.GetButton("Vertical") == true))
        {
            sneaking = false;
            return true;
        }
        else
        {
            anim.SetBool("Running", false);
            return false;
        }
        
    }

    /// <summary>
    /// This method checks if the sneaking button has been pressed. Depending on which button has been pressed, it will return true or false. It also modifiys the size of the player.
    /// </summary>
    /// <returns>Bool: Sneaking mode active or not.</returns>
    private bool checkSneakMode()
    {

        if ((Input.GetButton("Sneak") == true) && (Input.GetButton("Vertical") == true) )
        {
            running = false;
            return true;
        }
        else
        {
            anim.SetBool("Sneaking", false);
            return false;
        }
    }


    /// <summary>
    /// This method checks via RayCast if the player is on the ground or not.
    /// </summary>
    /// <returns>Bool: Player on ground or not.</returns>
    public bool playerIsGrounded()
    {
        float maxDistanceToGround = 0.1f;
        Vector3 down = transform.TransformDirection(Vector3.down);
        if (Physics.Raycast(transform.position, down, maxDistanceToGround))
        {
            playerGrounded = true;
            return true;
        }
        else
        {
            playerGrounded = false;
            return false;
        }
    }

    /// <summary>
    /// This method handles the movement of the player depending on which movement mode is active (running, walking, sneaking). It also does the jumping.
    /// </summary>
    /// <param name="turn"></param>
    /// <param name="moveVertical"></param>
    /// <param name="sneaking"></param>
    private void manageMovement(float turn, float moveVertical, bool sneaking)
    {

        anim.SetFloat("Speed", rb.velocity.magnitude);

        if (moveVertical <= 0.1)
        {
            if ((sneaking == false) && (running == false)) {
                anim.SetBool("Walking", false);
                anim.SetBool("Running", false);
                anim.SetBool("Sneaking", false);
                anim.SetBool("Walking Backwards", false);
                anim.SetBool("Idling", true);

                movementSpeed = Constants.WALKING_SPEED;
                rotationSpeed = Constants.WALKING_ROTATION * rotationFactor;

                transform.Rotate(0, (Input.GetAxis("Mouse X") * rotationSpeed), 0);
                transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * turn);
            }
        }

        if (moveVertical < 0)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("Sneaking", false);
            anim.SetBool("Idling", false);
            anim.SetBool("Walking Backwards", true);

            rotationSpeed = Constants.WALKING_ROTATION * rotationFactor;
            transform.Rotate(0, (Input.GetAxis("Mouse X") * rotationSpeed), 0);
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * turn);

            if (axes == RotationAxes.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                //rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                //rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }

        }

        if (moveVertical > 0.1)
        {
            if ((sneaking == false) && (running == false)) {
            anim.SetBool("Walking", true);
            anim.SetBool("Idling", false);
            anim.SetBool("Walking Backwards", false);

            movementSpeed = Constants.WALKING_SPEED;
            rotationSpeed = Constants.WALKING_ROTATION * rotationFactor;

            transform.Rotate(0, (Input.GetAxis("Mouse X") * rotationSpeed), 0);
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * turn);


            if (axes == RotationAxes.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                //rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                //rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
            }
        }


        if (moveVertical > 0.1)
        {
            if ((sneaking == true) && (running == false))
            {
                movementSpeed = Constants.SNEAKING_SPEED;
                rotationSpeed = Constants.WALKING_ROTATION * rotationFactor;

                anim.SetBool("Walking", false);
                anim.SetBool("Running", false);
                anim.SetBool("Sneaking", true);
                anim.SetBool("Idling", false);
                anim.SetBool("Walking Backwards", false);

                //transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * moveVertical);
                transform.Rotate(0, (Input.GetAxis("Mouse X") * rotationSpeed), 0);
                transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * turn);

                if (axes == RotationAxes.MouseXAndY)
                {
                    float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                    //rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                    //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                    transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
                }
                else if (axes == RotationAxes.MouseX)
                {
                    transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
                }
                else
                {
                    //rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                    //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                    transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
                }
            }

        }
        else
        {
            if ((sneaking == true) && (running == false))
            {
                movementSpeed = Constants.SNEAKING_SPEED;
                rotationSpeed = Constants.WALKING_ROTATION * rotationFactor;

                anim.SetBool("Walking", false);
                anim.SetBool("Running", false);
                anim.SetBool("Sneaking", false);
                anim.SetBool("Idling", false);
                anim.SetBool("Walking Backwards", false);

                //transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * moveVertical);
                transform.Rotate(0, (Input.GetAxis("Mouse X") * rotationSpeed), 0);
                transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * turn);

                if (axes == RotationAxes.MouseXAndY)
                {
                    float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                    //rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                    //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                    transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
                }
                else if (axes == RotationAxes.MouseX)
                {
                    transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
                }
                else
                {
                    //rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                    //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                    transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
                }
            }
        }

        if (moveVertical > 0.1)
        {
            if ((sneaking == false) && (running == true)) {

                movementSpeed = Constants.RUNNING_SPEED;
                rotationSpeed = Constants.WALKING_ROTATION * rotationFactor;

                anim.SetBool("Walking", false);
                anim.SetBool("Running", true);
                anim.SetBool("Sneaking", false);
                anim.SetBool("Idling", false);

                //transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * moveVertical);
                transform.Rotate(0, (Input.GetAxis("Mouse X") * rotationSpeed), 0);
                transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * turn);

                if (axes == RotationAxes.MouseXAndY)
                {
                    float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                    //rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                    //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                    transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
                }
                else if (axes == RotationAxes.MouseX)
                {
                    transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
                }
                else
                {
                    //rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                    //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                    transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
                }

            }
        }


        if ((Input.GetButton("Jump")) == true && (playerIsGrounded() == true))
        {
            rb.velocity = new Vector3(0, Constants.JUMPING_SPEED, 0);
            anim.SetBool("Jumping", true);


        }
        else
        {
            anim.SetBool("Jumping", false);
        }
    }




    /// <summary>
    /// Returning the player's position (for GameController).
    /// </summary>
    /// <returns>Vector3: Player's Position</returns>
    public Vector3 getPlayerPosition()
    {
        return transform.position;
    }

    /// <summary>
    /// Returns if the sneakingMode is active or not (for GameController).
    /// </summary>
    /// <returns>Bool: Sneaking Mode active or not.</returns>
    public bool sneakingIsActive()
    {
        return Input.GetButton("Sneak");
    }

    /// <summary>
    /// Returns if the runningMode is active or not (for GameController).
    /// </summary>
    /// <returns>Bool: Running Mode active or not.</returns>
    public bool runningIsActive()
    {
        return running;
    }

    public bool isPlayerGrounded()
    {
        return playerGrounded;
    }

    public bool isMovementDisabled()
    {
        return movementDisabled;
    }

    /// <summary>
    /// This method disables all movement of the player.
    /// </summary>
    /// <param name="disable"></param>
    public void disableMovement(bool disable)
    {
        movementDisabled = disable;
    }



}