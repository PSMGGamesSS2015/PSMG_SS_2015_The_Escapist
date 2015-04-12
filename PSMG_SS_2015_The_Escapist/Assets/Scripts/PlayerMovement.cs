using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody rb;
    private float movementSpeed = 5f;
    private float rotationSpeed = 10f;
    private bool sneaking = false;

    void FixedUpdate()
    {

        rb = GetComponent<Rigidbody>();

        float turn = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        sneaking = checkSneakButton();

        manageMovement(turn, moveVertical, sneaking);




        

        
    }

    private bool checkSneakButton()
    {
        if ((Input.GetButtonDown("Sneak") == true) && (sneaking == true))
        {
            return false;

        } else if ((Input.GetButtonDown("Sneak")) && (sneaking == false)) {
            return true;

        }
        else
        {
            return sneaking;
        }
    }

    private void manageMovement(float turn, float moveVertical, bool sneaking)
    {
        if (sneaking == true)
        {
            movementSpeed = 2f;
            rotationSpeed = 3f;

            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * moveVertical);
            transform.Rotate(0, (rotationSpeed * turn), 0);

        }
        else
        {
            movementSpeed = 5f;
            rotationSpeed = 6f;

            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * moveVertical);
            transform.Rotate(0, (rotationSpeed * turn), 0);

        }




    }

}
