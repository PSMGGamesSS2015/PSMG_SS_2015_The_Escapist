using System;
using UnityEngine;

public class DragDoor : MonoBehaviour
{
    public String layerMaskName = "Interactive";
    public float boundaryLimiterDampingFactor = 0.5f;
    public float dragAcceleration = 8f;
    public float dragSpeedLimit = 5f;
    public float xSensitivity = 1f;
    public float ySensitivity = 0.5f;
    public float fieldOfFocus = 30f;
    public float maxDragDistance = 2f;
    public int numOfRays = 5;

    private GameObject player;
    private Camera firstPersonCam;
    private GameObject focusedDoor = null;
    private GameObject lastFocusedDoor = null;
    private Door focusedDoorControl;
    private PlayerDetection playerDetection;
    private LayerMask dragableObjects;

    private bool pullToOpen;
    private float maxOpenAngle;
    private bool doorMirrored;
    private bool zAxisInverted;

    private int dragDirectionFactor;
    private float totalDragRotation = 0;
    private float dragDirectionChangeLimit = 5f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        firstPersonCam = player.GetComponentInChildren<Camera>();
        playerDetection = player.GetComponent<PlayerDetection>();

        dragableObjects = (1 << LayerMask.NameToLayer(layerMaskName));
    }

    void Update()
    {
        if (!Input.GetButton("Use")) { focusedDoor = null; lastFocusedDoor = null; return; }

        //GameObject focusedObj = playerDetection.getBestMatchObjInFOV();
        //validate(focusedObj);

        LookForDoorInPlayersFocus();

        if (!focusedDoor) { return; }

        if (focusedDoorControl.isActive() && !focusedDoorControl.isLocked()) { Drag(); }
    }

    private void LookForDoorInPlayersFocus()
    {
        // RAYCAST FOR DOOR AROUND PLAYER'S FOCUSPOINT WITHIN THE FOCUS FIELD
        float focusFieldDiameter = fieldOfFocus / firstPersonCam.fieldOfView;
        float stepSize = focusFieldDiameter / (numOfRays - 1);
        float leftBorder = 0.5f - (focusFieldDiameter / 2);
        float middle = Mathf.Ceil((numOfRays - 1) / 2);

        for (int i = 0; i <= middle; i++)
        {
            float playerViewXPos = leftBorder + ((middle + i) * stepSize);
            if (lookForValidDoorAt(playerViewXPos)) { return; }

            if ((middle - i - 1) >= 0)
            {
                playerViewXPos = leftBorder + ((middle - i - 1) * stepSize);
                if (lookForValidDoorAt(playerViewXPos)) { return; }
            }
        }
    }

    private bool lookForValidDoorAt(float playerViewXPos)
    {
        GameObject door = raycastForDoorAt(playerViewXPos);
        validate(door);
        if (focusedDoor) { return true; }

        return false;
    }

    private GameObject raycastForDoorAt(float xPos)
    {
        GameObject hitDoor = null;

        Ray ray = firstPersonCam.ViewportPointToRay(new Vector3(xPos, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * maxDragDistance, Color.magenta);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDragDistance, dragableObjects))
        {
            Renderer hitRenderer = hit.collider.GetComponent<Renderer>();

            if (hitRenderer.tag == "Door")
            {
                hitDoor = hit.collider.gameObject;
            }
        }

        return hitDoor;
    }

    private void validate(GameObject focusedObj)
    {
        if (!focusedObj || focusedObj.tag != "Door")
        {
            focusedDoor = null;
        }

        else if (!focusedDoor)
        {
            if (!lastFocusedDoor)
            {
                initializeFocusedDoorVariables(focusedObj);
            }

            if (focusedObj == lastFocusedDoor)
            {
                focusedDoor = focusedObj;
            }
        }
    }

    private void initializeFocusedDoorVariables(GameObject door)
    {
        focusedDoor = door;
        lastFocusedDoor = door;
        totalDragRotation = 0;

        focusedDoorControl = focusedDoor.GetComponentInParent<Door>();

        doorMirrored = isDoorMirrored();
        zAxisInverted = focusedDoorControl.isInverted();
        pullToOpen = focusedDoorControl.isPulledOpen();
        maxOpenAngle = focusedDoorControl.getMaxOpenAngle();
    }

    private bool isDoorMirrored()
    {
        float doorwayRot = focusedDoor.transform.parent.eulerAngles.y;
        float defaultDoorRot = focusedDoorControl.getDefaultRotation();

        float defaultOffsetRotation = Mathf.Abs(doorwayRot - defaultDoorRot);
        bool doorMirrored = (defaultOffsetRotation >= 179.9f) ? true : false;

        return doorMirrored;
    }

    private void Drag()
    {
        float angle = calcDragRotationAngle();
        angle = limitMaxSpeed(angle);

        // IF THE DRAG ANGLE WOULD MOVE THE DOOR OUTSIDE ITS BOUNDARIES, IT WILL BE REVERSED TO ROTATE THE DOOR IN THE INVERSE DIRECTION.
        // IN THIS WAY WE CAN ENSURE THAT THE BOUNDARIES ARE NOT VIOLATED TO MUCH, 
        // WHILE IT IS STILL GUARANTEED, THAT THE PLAYER CAN DRAG THE DOOR IF IT MOVES OUTSIDE THE BOUNDARIES
        if (outsideBoundaries(angle)) { angle *= -boundaryLimiterDampingFactor; }

        focusedDoor.transform.Rotate(Vector3.up, angle);
    }


    // CALC ANGLE FROM MOUSE MOVEMENT AND PLAYER ORIENTATION
    private float calcDragRotationAngle()
    {
        int yDragDirectionFactor = 1;
        if (doorMirrored) { yDragDirectionFactor = -1; }

        float mouseAcceleration = ((Input.GetAxis("Mouse X") * xSensitivity) + (Input.GetAxis("Mouse Y") * ySensitivity * yDragDirectionFactor));

        if (Mathf.Abs(totalDragRotation) < dragDirectionChangeLimit) { dragDirectionFactor = calcDragDirectionFactor(); }

        float dragAngle = mouseAcceleration * dragAcceleration * dragDirectionFactor;

        totalDragRotation += dragAngle;

        return dragAngle;
    }

    // THE IDEA OF THIS METHOD IS TO INTRODUCE A REVERSE FACTOR FOR THE MOUSE DRAG DIRECTION. 
    // THE DRAG DIRECTION IS BASED ON THE ORIENTATION OF THE PLAYER TOWARDS THE AXIS OF THE OPEN DOOR.
    // THE SWITCH POINT IS MARKED BY THE ROTATION THE PLAYER HAS, WHEN HE STANDS PARALLEL TO THE 90° OPENED DOOR.
    private int calcDragDirectionFactor()
    {
        int dragDirectionFactor = 1;

        float playerRot = player.transform.eulerAngles.y;
        float doorRot = focusedDoor.transform.eulerAngles.y;
        float doorwayRot = focusedDoor.transform.parent.eulerAngles.y;

        int playerRotationCorrection = 90;
        int defaultOpenDoorAxis = 90;

        // ORIENTATION OF THE PLAYER TOWARDS THE OPEN DOOR AXIS. THE RANGE OF VALUES OF THIS VARIABLE REACH FROM -180° TO 180°.
        // THE DOORWAY ROTATION IS USED TO ADJUST THE OPEN DOOR AXIS ROTATION VALUE, WHICH IS 90° FOR A DOORWAY WITH 0° ROTATION.
        // THE PLAYER ROTATION CORRECTION IS NEEDED BECAUSE THE PLAYERS Z-AXIS POINTS INTO ITS LOOK DIRECTION, WHILE THE DOORS Z-AXIS LIES PARALLEL TO IT.
        float playerOrientationAngle = (playerRot - doorwayRot) + playerRotationCorrection + defaultOpenDoorAxis;

        // THE NORMALIZED PLAYER ORIENTATION ANGLE IS SHIFTED BY 180°, TO ALIGN THE SWITCH POINT FROM POSITIVE TO NEGATIVE VALUES AT A PLAYER ORIENTATION ANGLE OF 0°. 
        float relativePlayerOrientation = 180 - NormalizeAngle(playerOrientationAngle);

        // IF THE PLAYER LOOKS IN NEGATIVE DIRECTION OF THE OPEN DOOR AXIS, THE DRAG DIRECTION WILL BE REVERSED
        if (relativePlayerOrientation > 0) { dragDirectionFactor *= -1; }

        // ADITIONAL FACTORS TO REVERSE DRAG ROTATION
        if (zAxisInverted) { dragDirectionFactor *= -1; }
        if (pullToOpen) { dragDirectionFactor *= -1; }

        return dragDirectionFactor;
    }

    private float limitMaxSpeed(float angle)
    {
        if (angle > 0) { angle = Mathf.Min(angle, dragSpeedLimit); }
        else { angle = Mathf.Max(angle, -dragSpeedLimit); }

        return angle;
    }

    // CHECK IF THE GIVEN ANGLE WILL MOVE THE DOOR OUTSIDE ITS PREDEFINED BOUNDARIES. 
    private bool outsideBoundaries(float currentDragAngle)
    {
        bool outsideBoundaries = false;

        float defaultDoorRot = focusedDoorControl.getDefaultRotation();

        float doorRot = focusedDoor.transform.eulerAngles.y;
        float minDoorAngle = NormalizeAngle((pullToOpen == doorMirrored) ? defaultDoorRot : defaultDoorRot - maxOpenAngle);
        float maxDoorAngle = NormalizeAngle((pullToOpen == doorMirrored) ? (defaultDoorRot + maxOpenAngle) : defaultDoorRot);

        float middleAngle = minDoorAngle + (maxOpenAngle / 2);
        float turningAngle = NormalizeAngle(middleAngle + 180);

        // MORE CRAZY SHIT
        if (minDoorAngle < maxDoorAngle)
        {
            if (turningAngle > 180)
            {
                if (((doorRot < minDoorAngle || doorRot > turningAngle) && currentDragAngle < 0) || ((doorRot > maxDoorAngle && doorRot < turningAngle) && currentDragAngle > 0))
                {
                    outsideBoundaries = true;
                }
            }
            else
            {
                if ((doorRot < minDoorAngle && doorRot > turningAngle && currentDragAngle < 0) || ((doorRot > maxDoorAngle || doorRot < turningAngle) && currentDragAngle > 0))
                {
                    outsideBoundaries = true;
                }
            }
        }
        else
        {
            if ((doorRot < minDoorAngle && doorRot > turningAngle && currentDragAngle < 0) || (doorRot > maxDoorAngle && doorRot < turningAngle && currentDragAngle > 0))
            {
                outsideBoundaries = true;
            }
        }

        //Debug.Log(minDoorAngle + " " + maxDoorAngle + " " + turningAngle + " " + doorRot + " " + reverseFactor + " " + currentDragAngle * reverseFactor);

        return outsideBoundaries;
    }

    private float NormalizeAngle(float angle)
    {
        float modulo = Mathf.Abs(angle) - (360 * Mathf.Floor(Mathf.Abs(angle) / 360));
        if (angle < 0) modulo = 360 - modulo;

        return modulo;
    }

    private bool isTimeLimitReached(float startTime, float timeLimit)
    {
        return (Time.time - startTime) > timeLimit;
    }


    public GameObject getFocusedDoor()
    {
        return focusedDoor;
    }
}