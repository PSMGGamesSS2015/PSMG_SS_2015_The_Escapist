using UnityEngine;
using System.Collections;

public class LightFlickering : MonoBehaviour {

    private Light light;

    float minFlickerTime = 0.01f;
    float maxFlickerTime = 1f;
    float minLightIntensity = 3f;
    float maxLightIntensity = 3.5f;


    private void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine("flicker");
    }

    IEnumerator flicker()
    {
        while(true)
        {
            light.enabled = true;
            light.intensity = Random.Range(minLightIntensity, maxLightIntensity);
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
            light.enabled = false;
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
        }
    }
}