using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
    private float sensitivityX = Constants.MOUSE_SENSITIVITY;
    private float sensitivityY = Constants.MOUSE_SENSITIVITY;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;

    Quaternion rotation;

    GamingControl control;
    private Vector3 sneakingHeight = new Vector3(0, 0.3f, 0);
    private Vector3 sneakingVector;
    private Vector3 walkingVector;
    private Vector3 targetPosition;

	void FixedUpdate ()
	{
        setPosition();

		if (axes == RotationAxes.MouseXAndY)
		{
			//float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
            float rotationX = 0;

			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}


	void Start ()
	{
        control = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}

    void Awake()
    {
        //rotation = transform.rotation;
        //transform.parent.rotation = rotation;

        walkingVector = transform.position;
        sneakingVector = transform.position - sneakingHeight;

    }

    void LateUpdate()
    {
        //transform.rotation = rotation;
    }

    private void setPosition()
    {

        if ((control.isSneakingActive()) && (transform.position.y >= sneakingVector.y))
        {
            targetPosition = transform.position - sneakingHeight;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 1);

        }

        if (!(control.isSneakingActive()) && (transform.position.y <= walkingVector.y))
        {
            targetPosition = transform.position + sneakingHeight;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 1);

        }

    }
}