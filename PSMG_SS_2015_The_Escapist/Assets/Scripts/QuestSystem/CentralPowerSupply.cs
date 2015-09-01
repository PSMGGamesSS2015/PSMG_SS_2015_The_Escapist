using UnityEngine;
using System.Collections;

public class CentralPowerSupply : MonoBehaviour {

    public GameObject fuseBoxCollection;
    public GameObject firstAffectedLightsCollection;
    public GameObject alertLightsCollection;
    public GameObject energyBeam;
    public AudioSource audioSrc;
    public Color alertColor;
    public float alertPulseSpeed = 0.01f;

    private CentralPowerSupplyFuseBox[] fuseBoxes;
    private int deactivatedFuseBoxNum = 0;

    private Light[] firstAffectedLights;
    private Light[] otherAffectedLights;
    private Light[] alertLights;

    private LightFlickering[] firstAffectedLightsFlicker;
    private Renderer[] firstAffectedLightsRenderer;

    private float alertLightIntensity = 8;

    void Start()
    {
        fuseBoxes = fuseBoxCollection.GetComponentsInChildren<CentralPowerSupplyFuseBox>();
        firstAffectedLights = firstAffectedLightsCollection.GetComponentsInChildren<Light>();
        firstAffectedLightsFlicker = firstAffectedLightsCollection.GetComponentsInChildren<LightFlickering>();
        firstAffectedLightsRenderer = firstAffectedLightsCollection.GetComponentsInChildren<Renderer>();
        alertLights = alertLightsCollection.GetComponentsInChildren<Light>();
    }

    void FixedUpdate()
    {
        if (deactivatedFuseBoxNum >= fuseBoxes.Length)
        {
            changeLightColor(alertLights);
            
            if (!audioSrc.isPlaying) 
            { 
                audioSrc.Play();
                energyBeam.SetActive(false);
            }
        }

        else if (deactivatedFuseBoxNum >= fuseBoxes.Length / 2)
        {
            setFlickerEnabled(firstAffectedLightsFlicker, false);
        }

        else if (deactivatedFuseBoxNum > 4)
        {
            setFlickerEnabled(firstAffectedLightsFlicker, true);
        }
    }
    
    private void setFlickerEnabled(LightFlickering[] flickers, bool enabled)
    {
        foreach (LightFlickering flicker in flickers)
        {
            if (enabled) { flicker.enabled = true; }
            else { flicker.setDisabled(true); }
        }
    }

    private void changeLightColor(Light[] alertLights)
    {
        foreach (Light light in alertLights)
        {
            light.color = alertColor;
            light.intensity = alertLightIntensity;

            alertLightIntensity -= alertPulseSpeed;

            if(alertLightIntensity < 0)
            {
                alertLightIntensity = 8;
            }
        }
    }

    public void fuseBoxDeactivated()
    {
        deactivatedFuseBoxNum++;
    }
}