using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {

    //Constants for the playerMovement
    public static float SNEAKING_SPEED = 1f;
    public static float WALKING_SPEED = 3f;
    public static float RUNNING_SPEED = 5f;

    public static float SNEAKING_ROTATION = 1f;
    public static float WALKING_ROTATION = 1f;
    public static float RUNNING_ROTATION = 1f;

    public static float VERTICAL_ROTATION = 150f;

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
    public static float AI_NORMAL_SPEED = 1f;
    public static float AI_CHASING_SPEED = 3f;
    public static float AI_VIEW_ANGLE = 110f;
    public static float AI_PATROL_RANGE = 20f;

    //Constants for the HurtEeffect
    public static float DISPLAY_TIME = 5f;

    //Constants for Shadow
    public static float FIELD_OF_VIEW_RANGE = 60f;
    public static float RAY_RANGE = 180f;

    public static float FULL_VISIBLE = 0f;
    public static float HEAD_COVERED = 20f;
    public static float FRONT_COVERED = 40f;
    public static float BACK_COVERED = 40f;
    public static float FULL_COVERED = 100f;

    //Constants for LightSwitch
    public static float SWITCH_ANIM_TIME = 0.5f;
}
