using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody rb;
    private float movementSpeed;
    private float rotationSpeed;
    public bool sneaking = false;
    public bool running = false;
    private bool firstPersonActive = true;
    private float jumpingSpeed = 500;




    //TO DO: ADD: - Running only available when in danger

    void FixedUpdate()
    {

        rb = GetComponent<Rigidbody>();

        float turn = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        sneaking = checkSneakButton();
        running = checkRunningButton();
        firstPersonActive = isCameraFirstPerson();

        // All movement will be done here.
        if (firstPersonActive == true)
        {
            manageMovement(turn, moveVertical, sneaking);
        }
        else
        {
            //Only used in debug-mode (third-person).
            debugMovement(turn, moveVertical, sneaking);
        }

        
    }

    private void debugMovement(float turn, float moveVertical, bool sneaking)
    {
        if (sneaking == true)
        {
            movementSpeed = Constants.SNEAKING_SPEED;
            rotationSpeed = Constants.SNEAKING_ROTATION;


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
            rotationSpeed = Constants.RUNNING_ROTATION;

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
            rotationSpeed = Constants.WALKING_ROTATION;

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
            GetComponent<Rigidbody>().AddForce(transform.up * jumpingSpeed);
        }


    }

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
            transform.localScale += new Vector3(0, 0.5f, 0);
        }
        else
        {
            transform.localScale -= new Vector3(0, 0.5f, 0);
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
            GetComponent<Rigidbody>().AddForce(transform.up * jumpingSpeed);
        }



    }

    // Checks, if the player is on the ground for jumping (to prevent double jump etc.).
    private bool playerIsGrounded()
    {
        if (transform.position.y < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public Vector3 getPlayerPosition()
    {
        return transform.position;
    }

    public bool sneakingIsActive()
    {
        return sneaking;
    }

}
