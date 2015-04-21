using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {

    public Light sun;
    public Light pointLight;

    private GameObject player;
    private Animation switchAnim;
    private float switchAnimTime = 0.5f;
    private float nextSwitchTime;
    private bool switchOn = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        switchAnim = GetComponent<Animation>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player && Input.GetButton("Use"))
        {
            SwitchLight();
        }
    }

    void SwitchLight()
    {
        if (Time.time > nextSwitchTime)
        {
            switchAnim["switch"].speed = !switchOn ? 1 : -1;
            switchAnim["switch"].time = !switchOn ? 0 : switchAnim["switch"].length;

            switchAnim.Play();

            sun.enabled = !sun.enabled;
            pointLight.enabled = !pointLight.enabled;
            switchOn = !switchOn;

            nextSwitchTime = Time.time + switchAnimTime;
        }
    }
}
