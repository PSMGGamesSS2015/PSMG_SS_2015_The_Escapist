using UnityEngine;

public class DragDoor : MonoBehaviour
{
    public LayerMask layerMask;
    public float maxDragDistance = 2f;
    public int numOfRays = 5;
    public float fieldOfFocus = 30;
    public float minDoorAngle = 0;
    public float maxDoorAngle = 90;
    public float dragAcceleration = 2f;
    public float xSensitivity = 1f;
    public float ySensitivity = 0.5f;
    public float playerRotationCorrection = 90f;

    private GameObject player;
    private Camera firstPersonCam;
    private float offsetAngle = 0;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        firstPersonCam = player.GetComponentInChildren<Camera>();
    }

    void FixedUpdate()
    {
        if (!Input.GetButton("Use")) { return; }

        GameObject focusedDoor = LookForDoorInPlayersFocus();
        if (focusedDoor) Drag(focusedDoor);
    }

    private GameObject LookForDoorInPlayersFocus()
    {
        // RAYS POINTING AROUND PLAYER'S FOCUSPOINT WITHIN THE FOCUS FIELD
        float focusFieldDiameter = fieldOfFocus / firstPersonCam.fieldOfView;
        float stepSize = focusFieldDiameter / (numOfRays - 1);
        float leftBorder = 0.5f - (focusFieldDiameter / 2);
        
        for (int rayNum = 0; rayNum < numOfRays; rayNum++)
        {
            Ray ray = firstPersonCam.ViewportPointToRay(new Vector3(leftBorder + (rayNum * stepSize), 0.5f, 0));
            
            // MAKE RAY VISIBLE FOR DEBUGING
            Debug.DrawRay(ray.origin, ray.direction * maxDragDistance, Color.magenta);

            GameObject focusedDoor;
            if (focusedDoor = LookForDoor(ray)) { return focusedDoor; }
        }

        return null;
    }

    private GameObject LookForDoor(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDragDistance, layerMask))
        {
            // GET RENDERER OF OBJECT HIT
            Renderer hitRenderer = hit.collider.GetComponent<Renderer>();

            // RETURN NULL IF RENDERER NOT TAGGED AS DOOR
            if (hitRenderer.tag != "Door") { return null; }
            
            GameObject focusedDoor = hit.collider.gameObject;
            return focusedDoor;
        }

        // RETURN NULL IF NO OBJECT FOUND
        return null;
    }

    private void Drag(GameObject focusedDoor)
    {
        // CALC ANGLE FROM MOUSE MOVEMENT AND PLAYER ORIENTATION
        float mouseAcceleration = ((Input.GetAxis("Mouse X") * xSensitivity) + (Input.GetAxis("Mouse Y") * ySensitivity));
        float angle = mouseAcceleration * dragAcceleration * calcDragDirectionFactor(focusedDoor);

        // CHECK IF ROTATION IS INSIDE DEFINED BOUNDARIES
        float doorRot = focusedDoor.transform.localEulerAngles.y;
        if (doorRot + angle <= minDoorAngle || doorRot + angle >= maxDoorAngle) { return; }

        focusedDoor.transform.Rotate(Vector3.up, angle);
    }

    private int calcDragDirectionFactor(GameObject focusedDoor)
    {
        // THE IDEA OF THIS METHOD IS TO INTRODUCE A REVERSE FACTOR FOR THE MOUSE DRAG DIRECTION. 
        // THE DRAG DIRECTION IS BASED ON THE ORIENTATION OF THE PLAYER TOWARDS THE AXIS OF THE OPEN DOOR.
        // THE SWITCH POINT IS MARKED BY THE ROTATION THE PLAYER HAS, WHEN HE STANDS PARALLEL TO THE 90° OPENED DOOR.

        float doorwayRot = focusedDoor.transform.parent.eulerAngles.y;
        float playerRot = player.transform.eulerAngles.y;

        // ORIENTATION OF THE PLAYER TOWARDS THE OPEN DOOR AXIS. THE RANGE OF VALUES OF THIS VARIABLE REACH FROM -180° TO 180°.
        float relativePlayerOrientation = 180 - NormalizeAngle((playerRot - doorwayRot) + 180);

        // FACTOR TO REVERSE DRAG ROTATION IF PLAYER LOOKS IN NEGATIVE DIRECTION OF THE OPEN DOOR AXIS
        int dragDirectionFactor = (relativePlayerOrientation < 0) ? -1 : 1;

        return dragDirectionFactor;
    }

    private float NormalizeAngle(float angle)
    {
        return angle % 360;
    }
}