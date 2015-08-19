using UnityEngine;
using System.Collections;

public class PlayerVisibility : MonoBehaviour 
{
    public float maxVisibilityRange = 20f;

    public float sneakingFactor = 0.5f;
    public float standingFactor = 0.7f;
    public float walkingFactor = 1f;
    public float runningFactor = 1.2f;

    private GamingControl gameController;
    private SphereCollider visibilityColl;

	void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
        visibilityColl = GameObject.Find("player_visibility").GetComponent<SphereCollider>();
	}
	
    void Update()
    {
        float visibilityRadius = maxVisibilityRange;
        float motionFactor = standingFactor;

        //Sneaking
        if (gameController.isSneakingActive())
        {
            motionFactor = sneakingFactor;
        }

        //Running
        else if (gameController.isRunningActive())
        {
            motionFactor = runningFactor;
        }

        // Walking
        else
        {
            motionFactor = walkingFactor;
        }

        visibilityColl.radius = maxVisibilityRange * gameController.getPlayerVisibilityFactor() * motionFactor;
    }
}
