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
    private GameObject headSensor;
    private GameObject breastSensor;
    private Vector3 sneakingVector;
    private Vector3 walkingVector;

	void FixedUpdate ()
	{

        getPositions();
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

    /// <summary>
    /// This method returns the current position of the breast sensor and of the head sensor.
    /// </summary>
    private void getPositions()
    {
        sneakingVector = breastSensor.transform.position;
        walkingVector = headSensor.transform.position;

    }

    /// <summary>
    /// When the game starts, the player gets the head sensor and the breast sensor
    /// as a reference of the positon of the camera. This will be needed when the player is crouching.
    /// </summary>
	void Start ()
	{
        headSensor = GameObject.FindGameObjectWithTag("Head Sensor");
        breastSensor = GameObject.FindGameObjectWithTag("Breast Sensor");
        control = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}

    void Awake()
    {


    }

    void LateUpdate()
    {

    }

    /// <summary>
    /// In case of switching the state walking to sneaking, the positon of the camera will be changed (and vice versa).
    /// </summary>
    private void setPosition()
    {

        if ((control.isSneakingActive()) && (transform.position.y >= sneakingVector.y))
        {
            transform.position = Vector3.Lerp(transform.position, sneakingVector, Time.deltaTime * 2);

        }

        if (!(control.isSneakingActive()) && (transform.position.y <= walkingVector.y))
        {
            transform.position = Vector3.Lerp(transform.position, walkingVector, Time.deltaTime * 2);

        }

    }
}