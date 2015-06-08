using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody rb;
    private float movementSpeed;
    private float rotationSpeed;
    public bool sneaking = false;
    private bool running = false;
    private bool firstPersonActive = true;
    private bool movementDisabled = false;
    private float turn;
    private float moveVertical;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }



    void FixedUpdate()
    {
        turn = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        sneaking = checkSneakMode();
        running = checkRunningMode();
        firstPersonActive = isCameraFirstPerson();

        // All movement will be done here.
        if (firstPersonActive == true)
        {
            if (movementDisabled == false)
            {
                manageMovement(turn, moveVertical, sneaking);
            }

        }
        else
        {
            //Only used in debug-mode (third-person).
            debugMovement(turn, moveVertical, sneaking);
        }

        
    }


    /// <summary>
    /// This method checks if the running button has been pressed. Depending on which button has been pressed, it will return true or false. It also modifiys the size of the player.
    /// </summary>
    /// <returns>Bool: Running mode active or not.</returns>
    private bool checkRunningMode()
    {
        if ((Input.GetButtonDown("Run") == true) && (running == true))
        {
            sneaking = false;
            return false;

        } else if ((Input.GetButtonDown("Run") == true) && (running == false)){

            sneaking = false;
            return true;
        }
        else
        {
            return running;
        }

        
    }

    /// <summary>
    /// This method checks if the sneaking button has been pressed. Depending on which button has been pressed, it will return true or false. It also modifiys the size of the player.
    /// </summary>
    /// <returns>Bool: Sneaking mode active or not.</returns>
    private bool checkSneakMode()
    {
        if ((Input.GetButtonDown("Sneak") == true) && (sneaking == true))
        {
            running = false;
            return false;

        } else if ((Input.GetButtonDown("Sneak")) && (sneaking == false)) {
            running = false;
            return true;

        }
        else
        {
            return sneaking;
        }
    }

    /// <summary>
    /// This method modifies the size of the player. The method will maybe not needed in the future developing process.
    /// </summary>
    /// <param name="sneaking"></param>
    private void modifySize(bool sneaking)
    {
        if (sneaking == true)
        {
            transform.Find("player_avatar").localScale += new Vector3(0, 0.5f, 0);
            transform.GetComponent<BoxCollider>().size += new Vector3(0, 0.5f, 0);
        }
        else
        {
            transform.Find("player_avatar").localScale -= new Vector3(0, 0.5f, 0);
            transform.GetComponent<BoxCollider>().size -= new Vector3(0, 0.5f, 0);
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
            return true;
        }
        else
        {
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
        if (sneaking == true)
        {
            movementSpeed = Constants.SNEAKING_SPEED;
            rotationSpeed = Constants.SNEAKING_ROTATION;

            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * moveVertical);
            transform.Rotate(0, (Input.GetAxis("Mouse X") * rotationSpeed), 0);
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * turn);



        } else if (running == true){
            movementSpeed = Constants.RUNNING_SPEED;
            rotationSpeed = Constants.RUNNING_ROTATION;

            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * moveVertical);
            transform.Rotate(0, (Input.GetAxis("Mouse X") * rotationSpeed), 0);
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * turn);
        }

        else
        {
            movementSpeed = Constants.WALKING_SPEED;
            rotationSpeed = Constants.WALKING_ROTATION;

            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * moveVertical);
            transform.Rotate(0, (Input.GetAxis("Mouse X") * rotationSpeed), 0);
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * turn);


        }

        if ((Input.GetButtonDown("Jump")) == true && (playerIsGrounded() == true))
        {
            rb.velocity = new Vector3(0, Constants.JUMPING_SPEED, 0);
            
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
        return sneaking;
    }

    /// <summary>
    /// This method disables all movement of the player.
    /// </summary>
    /// <param name="disable"></param>
    public void disableMovement(bool disable)
    {
        movementDisabled = disable;
    }

    // CAUTION: This part contains only methods for debugging the levels. This part will be removed when the game is finished.

    private bool isCameraFirstPerson()
    {
        if (Input.GetKey(KeyCode.Alpha2))
        {
            return false;
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            return true;
        }
        else
        {
            return firstPersonActive;
        }
    }

    // ONLY FOR DEBUGGING!!! Do not change.
    private void debugMovement(float turn, float moveVertical, bool sneaking)
    {
        if (sneaking == true)
        {
            movementSpeed = Constants.SNEAKING_SPEED;
            rotationSpeed = Constants.DEBUG_SNEAKING_ROTATION;


            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * moveVertical);

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }


        }
        else if (running == true)
        {
            movementSpeed = Constants.RUNNING_SPEED;
            rotationSpeed = Constants.DEBUG_RUNNING_ROTATION;

            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * moveVertical);

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }

        else
        {
            movementSpeed = Constants.WALKING_SPEED;
            rotationSpeed = Constants.DEBUG_WALKING_ROTATION;

            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * moveVertical);

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }

        }

        if ((Input.GetButtonDown("Jump")) == true && (playerIsGrounded() == true))
        {
            rb.velocity = new Vector3(0, Constants.JUMPING_SPEED, 0);
        }


    }



}