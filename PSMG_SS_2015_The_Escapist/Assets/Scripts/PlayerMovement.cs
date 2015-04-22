using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody rb;
    private float movementSpeed = 5f;
    private float rotationSpeed = 10f;
    public bool sneaking = false;
    private float jumpingSpeed = 500;




    //TO DO: ADD: - Running only available when in danger

    void FixedUpdate()
    {

        rb = GetComponent<Rigidbody>();

        float turn = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        sneaking = checkSneakButton();

        // All movement will be done here.
        manageMovement(turn, moveVertical, sneaking);
        
    }

    // Checks if the left ctrl is pressed an resizes the player if the mode changes.
    private bool checkSneakButton()
    {
        if ((Input.GetButtonDown("Sneak") == true) && (sneaking == true))
        {
            modifySize(sneaking);
            return false;

        } else if ((Input.GetButtonDown("Sneak")) && (sneaking == false)) {
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
            movementSpeed = 2f;
            rotationSpeed = 3f;

            
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * moveVertical);
            transform.Rotate(0, (Input.GetAxis("Mouse X") * rotationSpeed), 0);
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * turn);

        }
        else
        {
            movementSpeed = 5f;
            rotationSpeed = 6f;

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

}
