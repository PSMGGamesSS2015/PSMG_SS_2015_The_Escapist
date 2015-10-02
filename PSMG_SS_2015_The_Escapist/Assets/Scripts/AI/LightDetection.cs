using UnityEngine;
using System.Collections;

public class LightDetection : MonoBehaviour {

    public LayerMask opaqueLayers;
    public bool active = true;

    private new Light light;
    private ShadowDetection shadowDetection;
    private float previousPercentage = 0;
    private GameObject player;
    private bool wasInLight = false;

    void Start()
    {
        light = GetComponent<Light>();

        player = GameObject.FindGameObjectWithTag("Player");
        shadowDetection = player.GetComponent<ShadowDetection>();
    }

    void FixedUpdate()
    {
        float playerDistance = getDistanceTo(player.transform.position);

        if(playerDistance <= light.range + 5 && isRaycastHittingPlayer() && active)
        {
            wasInLight = true;
            float percentage = (1.0f / (1.0f + 25.0f * (playerDistance / light.range) * (playerDistance / light.range))) * (1.8f * light.intensity) * 100;
            float percentageDelta = percentage - previousPercentage;
            previousPercentage = percentage;

            Debug.Log(percentage);
            shadowDetection.addLightDelta(percentageDelta);
        }
        else if (wasInLight)
        {
            shadowDetection.addLightDelta(-previousPercentage);
            wasInLight = false;
        }
    }

    private bool isRaycastHittingPlayer()
    {
        Vector3 direction = (player.transform.position + new Vector3(0, 1f, 0)) - transform.position;

        Ray ray = new Ray(transform.position, direction);
        Debug.DrawRay(ray.origin, ray.direction * getDistanceTo(player.transform.position), Color.green);

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
}
