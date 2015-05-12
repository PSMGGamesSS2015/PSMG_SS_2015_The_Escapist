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
        sneaking = checkSneakButton();
        running = checkRunningButton();
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

   



    // Check if the left shift is pressed and resizes the player if the mode changes.
    private bool checkRunningButton()
    {
        if ((Input.GetButtonDown("Run") == true) && (running == true))
        {
            sneaking = false;
            return false;

        } else if ((Input.GetButtonDown("Run") == true) && (running == false)){

            if (sneaking == true)
            {
                modifySize(sneaking);
            }

            sneaking = false;
            return true;
        }
        else
        {
            return running;
        }

        
    }

    // Checks if the left ctrl is pressed and resizes the player if the mode changes.
    private bool checkSneakButton()
    {
        if ((Input.GetButtonDown("Sneak") == true) && (sneaking == true))
        {
            running = false;
            modifySize(sneaking);
            return false;

        } else if ((Input.GetButtonDown("Sneak")) && (sneaking == false)) {
            running = false;
            modifySize(sneaking);
            return true;

        }
        else
        {
            return sneaking;
        }
    }

    private void modifySize(bool sneaking)
    {
        if (sneaking == true)
        {
            transform.Find("player_body").localScale += new Vector3(0, 0.5f, 0);
            transform.GetComponent<BoxCollider>().size += new Vector3(0, 0.5f, 0);
        }
        else
        {
            transform.Find("player_body").localScale -= new Vector3(0, 0.5f, 0);
            transform.GetComponent<BoxCollider>().size -= new Vector3(0, 0.5f, 0);
        }
    }

    // Checks, if the player is on the ground for jumping (to prevent double jump etc.).
    public bool playerIsGrounded()
    {
        float maxDistanceToGround = 0.6f;
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




    //Returns the player's position (to the gameController).
    public Vector3 getPlayerPosition()
    {
        return transform.position;
    }

    //Returns if the sneakingmode is active or not (to the gameController).
    public bool sneakingIsActive()
    {
        return sneaking;
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
            //GetComponent<Rigidbody>().AddForce(transform.up * Constants.JUMPING_SPEED);
        }


    }


    public void disableMovement(bool disable)
    {
        movementDisabled = disable;
    }
}