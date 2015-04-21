using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour 
{
    public Light light1;
    public Light light2;

    private float switchAnimTime = 0.5f;
    private float nextSwitchTime;
    private float nextRotation = -45;
    private GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if (Input.GetButton("Use"))
            {
                SwitchLight();
            }
        }
    }

    void SwitchLight()
    {
        if (Time.time > nextSwitchTime)
        {
            light1.enabled = !light1.enabled;
            light2.enabled = !light2.enabled;
            transform.rotation = Quaternion.Euler(nextRotation, 0, 0);
            nextSwitchTime = Time.time + switchAnimTime;
            nextRotation *= -1;
        }
        

    }
}
