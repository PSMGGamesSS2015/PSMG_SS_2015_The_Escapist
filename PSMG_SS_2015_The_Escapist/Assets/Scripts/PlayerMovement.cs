using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody rb;


    void FixedUpdate()
    {

        rb = GetComponent<Rigidbody>();

        float turn = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        move(moveVertical);

        rotate(turn);





        

        
    }

    private void rotate(float turn)
    {
        float rotationSpeed = 80f;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    private void move(float input)
    {
        float movementSpeed = 5f;

        rb.MovePosition(transform.position + transform.forward * movementSpeed * Time.deltaTime * input);
    }
}
