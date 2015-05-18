using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {

    //Constants for the playerMovement
    public static float SNEAKING_SPEED = 2f;
    public static float WALKING_SPEED = 5f;
    public static float RUNNING_SPEED = 8f;

    public static float SNEAKING_ROTATION = 3f;
    public static float WALKING_ROTATION = 4f;
    public static float RUNNING_ROTATION = 5f;

    public static float VERTICAL_ROTATION = 175f;

    //This Constants will be removed (only for debugging).
    public static float DEBUG_SNEAKING_ROTATION = 40f;
    public static float DEBUG_WALKING_ROTATION = 50f;
    public static float DEBUG_RUNNING_ROTATION = 60f;
    //End

    public static float JUMPING_SPEED = 5;

    //Constants for the FollowAI
    public static float AI_ROTATION_SPEED = 3f;
    public static float AI_RANGE = 10f;
    public static float AI_STOP = 0f;
    public static float AI_NORMAL_SPEED = 2f;
    public static float AI_CHASING_SPEED = 5f;
    public static float AI_VIEW_ANGLE = 110f;
    public static float AI_PATROL_RANGE = 40f;

    //Constants for the HurtEeffect
    public static float DISPLAY_TIME = 5f;

}
