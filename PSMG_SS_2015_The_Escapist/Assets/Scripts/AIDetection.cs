using UnityEngine;
using System.Collections;

public class AIDetection : MonoBehaviour 
{
    public float visibilityDiscoveryDistanceFactor = 0.7f;
    public float noiseDiscoveryDistanceFactor = 0.7f;
    public float attackRange = 1.0f;

    public LayerMask opaqueLayers;
    public LayerMask soundProofLayerrs;

    private GamingControl gameController;
    private GameObject player;

    private bool playerVisibilityDetected = false;
    private bool playerNoiseDetected = false;
    private bool playerVisibilityDiscovered = false;
    private bool playerNoiseDiscovered = false;
    private bool playerInAttackRange = false;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Debug.Log(name + " noise: " + playerNoiseDetected + " visible: " + playerVisibilityDetected);
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.name == "player_visibility" || coll.name == "player_noise")
        {
            checkAttackRange();
        }

        if (coll.name == "player_visibility")
        {
            checkVisibility(coll);
        }

        else if (coll.name == "player_noise") 
        {
            checkNoise(coll);
        }
    }


    void OnTriggerExit()
    {
        setAllAIBoolsFalse();
    }


    private void checkVisibility(Collider playerVisibility)
    {
        if (!isPlayerInFOV()) { return; }

        if (!isPlayerInDiscoveryDistance(playerVisibility, visibilityDiscoveryDistanceFactor))
        {
            playerVisibilityDetected = true;
            playerVisibilityDiscovered = false;
        }
        else
        {
            playerVisibilityDiscovered = true;
        }
    }

    private void checkAttackRange()
    {
        if (getDistanceTo(player.transform.position) < attackRange) { playerInAttackRange = true; }
        else { playerInAttackRange = false; }
    }

    private void checkNoise(Collider playerNoise)
    {
        if (!isPlayerInDiscoveryDistance(playerNoise, noiseDiscoveryDistanceFactor))
        {
            playerNoiseDetected = true;
            playerNoiseDiscovered = false;
        }
        else
        {
            playerNoiseDiscovered = true;
        }
    }


    private void setAllAIBoolsFalse()
    {
        playerVisibilityDetected = false;
        playerNoiseDetected = false;
        playerVisibilityDiscovered = false;
        playerNoiseDiscovered = false;
    }

    private bool isPlayerInFOV()
    {
        Vector3 direction = player.transform.position - transform.position;
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

    private bool isPlayerInDiscoveryDistance(Collider detectionColl, float distanceFactor)
    {
        return (getDistanceTo(player.transform.position) <= detectionColl.gameObject.GetComponent<SphereCollider>().radius * distanceFactor);
    }

    private float getDistanceTo(Vector3 targetPos)
    {
        Vector3 direction = targetPos - transform.position;
        return direction.magnitude;
    }


    //PUBLIC METHODS
    public bool playerDetected()
    {
        return (playerNoiseDetected || playerVisibilityDetected);
    }

    public bool playerDiscovered()
    {
        return (playerNoiseDiscovered || playerVisibilityDiscovered);
    }

    public bool isPlayerInAttackRange()
    {
        return playerInAttackRange;
    }
}