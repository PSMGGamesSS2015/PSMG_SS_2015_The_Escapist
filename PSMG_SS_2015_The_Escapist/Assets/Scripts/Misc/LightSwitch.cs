using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {

    private GameObject player;

    private Animation switchAnim;

    public Light sunLight;
    public Light switchedSunLight;

    private Shadow shadowSunLight;
    private Shadow shadowSwitchedSunLight;

    private float switchAnimTime = 0.5f;
    private float nextSwitchTime;
    private bool switchOn = false;

    void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        switchAnim = GetComponent<Animation>();

        shadowSunLight = sunLight.GetComponent<Shadow>();
        shadowSwitchedSunLight = switchedSunLight.GetComponent<Shadow>();

        shadowSunLight.enabled = true;
        shadowSwitchedSunLight.enabled = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player && Input.GetButtonUp("Use"))
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

            sunLight.enabled = !sunLight.enabled;

            if (sunLight.enabled) shadowSunLight.enabled = true;
            else shadowSunLight.enabled = false;

            switchedSunLight.enabled = !switchedSunLight.enabled;
            if (switchedSunLight.enabled) shadowSwitchedSunLight.enabled = true;
            else shadowSwitchedSunLight.enabled = false;

            switchOn = !switchOn;

            nextSwitchTime = Time.time + switchAnimTime;
        }
    }
}
