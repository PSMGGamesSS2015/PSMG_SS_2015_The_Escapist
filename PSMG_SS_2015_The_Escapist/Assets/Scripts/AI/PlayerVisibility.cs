using UnityEngine;
using System.Collections;

public class PlayerVisibility : MonoBehaviour 
{
    public float sneakingFactor = 0.5f;
    public float standingFactor = 0.7f;
    public float walkingFactor = 1f;
    public float runningFactor = 1.2f;

    private GamingControl gameController;

    private float motionFactor;

	void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
	}
	
    void Update()
    {
        motionFactor = standingFactor;

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
    }

    public float getVisibilityFactor()
    {
        return gameController.getPlayerVisibilityFactor() * motionFactor;
    }
}
