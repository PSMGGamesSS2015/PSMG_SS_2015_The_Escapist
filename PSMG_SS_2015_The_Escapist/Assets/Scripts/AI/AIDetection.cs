using UnityEngine;
using System.Collections;

public class AIDetection : MonoBehaviour 
{
    private float maxNoiseDetectionDistance = 16f;
    private float maxVisibilityDetectionDistance = 20f;
    private float maxDiscoveryDistance = 5f;
    public float attackRange = 1.0f;

    public LayerMask opaqueLayers;
    public LayerMask soundProofLayerrs;

    private GamingControl gameController;
    private GameObject player;
    private PlayerVisibility playerVisibility;
    private PlayerNoise playerNoise;

    private bool playerNoiseDetected = false;
    private bool playerVisibilityDetected = false;
    private bool playerDiscovered = false;
    private bool playerInAttackRange = false;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerVisibility = player.GetComponent<PlayerVisibility>();
        playerNoise = player.GetComponent<PlayerNoise>();
    }

    void Update()
    {
        setAllAIBoolsFalse();

        float discoveryDistance = maxDiscoveryDistance * playerVisibility.getVisibilityFactor();
        float visibilityDetectionDistance = maxVisibilityDetectionDistance * playerVisibility.getVisibilityFactor();
        float noiseDetectionDistance = maxNoiseDetectionDistance * playerNoise.getNoiseFactor();
        //Debug.Log(noiseDetectionDistance + " " + visibilityDetectionDistance + " " + getDistanceTo(player.transform.position));

        if (isPlayerInFOV())
        {
            if (getDistanceTo(player.transform.position) < attackRange)
            {
                playerVisibilityDetected = true;
                playerDiscovered = true;
                playerInAttackRange = true;
            }
            else if (getDistanceTo(player.transform.position) < discoveryDistance) 
            {
                playerVisibilityDetected = true;
                playerDiscovered = true;
            }
            else if(getDistanceTo(player.transform.position) < visibilityDetectionDistance)
            {
                playerVisibilityDetected = true;
            }
        }
        else
        {
            if (getDistanceTo(player.transform.position) < noiseDetectionDistance)
            {
                playerNoiseDetected = true;
            }
        }
        //Debug.Log(name + " noise: " + playerNoiseDetected + " visible: " + playerVisibilityDetected );
    }

    private bool isPlayerInFOV()
    {
        Vector3 aiPos = transform.position;
        Vector3 playerPos = player.transform.position;
        aiPos.y = 1;
        playerPos.y = 1;

        Vector3 direction = playerPos - aiPos;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < Constants.AI_VIEW_ANGLE * 0.5f)
        {
            if (isRaycastHittingPlayer()) { return true; }
            
        }
        return false;
    }

    private bool isRaycastHittingPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;

        Ray ray = new Ray(transform.position + new Vector3(0, 1f, 0), direction);
        Debug.DrawRay(ray.origin, ray.direction * getDistanceTo(player.transform.position), Color.magenta);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, getDistanceTo(player.transform.position), opaqueLayers))
        {
            if (hit.collider.tag == "Player") { return true; }
        }

        return false;
    }

    private float getDistanceTo(Vector3 targetPos)
    {
        Vector3 direction = targetPos - transform.position;
        return direction.magnitude;
    }

    private void setAllAIBoolsFalse()
    {
        playerVisibilityDetected = false;
        playerNoiseDetected = false;
        playerDiscovered = false;
        playerInAttackRange = false;
    }


    //PUBLIC METHODS
    public bool isPlayerDetected()
    {
        return (playerVisibilityDetected || playerNoiseDetected);
    }

    public bool isPlayerDiscovered()
    {
        return playerDiscovered;
    }

    public bool isPlayerInAttackRange()
    {
        return playerInAttackRange;
    }
}