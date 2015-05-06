using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {

    public Light sun;
    public Light pointLight;
    private Shadow shadowScript;
    private Shadow shadowScript2;

    private bool test;

    private GameObject player;
    private Animation switchAnim;
    private float switchAnimTime = 0.5f;
    private float nextSwitchTime;
    private bool switchOn = false;

    void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        switchAnim = GetComponent<Animation>();

        shadowScript = sun.GetComponent<Shadow>();
        shadowScript2 = pointLight.GetComponent<Shadow>();

        shadowScript.enabled = true;
        shadowScript2.enabled = false;
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

            sun.enabled = !sun.enabled;

            if (sun.enabled) shadowScript.enabled = true;
            else shadowScript.enabled = false;

            pointLight.enabled = !pointLight.enabled;
            if (pointLight.enabled) shadowScript2.enabled = true;
            else shadowScript2.enabled = false;

            switchOn = !switchOn;

            nextSwitchTime = Time.time + switchAnimTime;
        }
    }
}
