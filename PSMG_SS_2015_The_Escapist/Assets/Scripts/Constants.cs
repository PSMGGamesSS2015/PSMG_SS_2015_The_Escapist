using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {

    //Constants for the playerMovement
    public static float SNEAKING_SPEED = 1f;
    public static float WALKING_SPEED = 2f;
    public static float RUNNING_SPEED = 3.5f;

    public static float SNEAKING_ROTATION = 1f;
    public static float WALKING_ROTATION = 1f;
    public static float RUNNING_ROTATION = 1f;

    public static float VERTICAL_ROTATION = 150f;

    public static float JUMPING_SPEED = 5;

    //Constants for the FollowAI
    public static float AI_ROTATION_SPEED = 3f;
    public static float AI_RANGE = 6f;
    public static float AI_RUN_RANGE = 3f;
    public static float AI_STOP = 0f;
    public static float AI_NORMAL_SPEED = 0.8f;
    public static float AI_CHASING_SPEED = 2.5f;
    public static float AI_RAT_WALKING_SPEED = 1f;
    public static float AI_VIEW_ANGLE = 120f;
    public static int AI_PLAYER_DETECTION_THRESHOLD = 70;
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


    // Constants for options (temporary)
    public static float MOUSE_SENSITIVITY = 1f;


    //public static float TEST = 0.5f;

}
