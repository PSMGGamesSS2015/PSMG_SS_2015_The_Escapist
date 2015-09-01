using UnityEngine;
using System.Collections;

public class LightFlickering : MonoBehaviour {

    public GameObject[] lightObjects;
    public string emissiveMaterialName = "emissive (Instance)";
    private Light[] lights;
    private Material[] emissiveMaterials;

    private bool disabled = false;

    float minFlickerTime = 0.01f;
    float maxFlickerTime = 1f;
    public float minLightIntensity = 3f;
    public float maxLightIntensity = 3.5f;


    private void Start()
    {
        lights = new Light[lightObjects.Length];
        emissiveMaterials = new Material[lightObjects.Length];

        for (int i = 0; i < lightObjects.Length; i++)
        {
            lights[i] = lightObjects[i].GetComponentInChildren<Light>();

            Material[] materials = lightObjects[i].GetComponent<Renderer>().materials;
            foreach (Material m in materials)
            {
                if (m.name == emissiveMaterialName)
                {
                    emissiveMaterials[i] = m;
                }
            }
        }
            
        StartCoroutine("flicker");
    }

    IEnumerator flicker()
    {
        while(!disabled)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].enabled = true;

                float newIntensity = Random.Range(minLightIntensity, maxLightIntensity);
                float intensityPercent = newIntensity / maxLightIntensity;

                Material emissiveMaterial = emissiveMaterials[i];
                if (emissiveMaterial)
                {
                    emissiveMaterial.SetColor("_Color", new Color(intensityPercent, intensityPercent, intensityPercent));
                }

                lights[i].intensity = newIntensity;
            }
                
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));

            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].enabled = false;

                Material emissiveMaterial = emissiveMaterials[i];
                if (emissiveMaterial)
                {
                    emissiveMaterial.SetColor("_Color", Color.black);
                }
            }

            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
        }
    }

    public void setDisabled(bool p)
    {
        disabled = p;
        if(disabled)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].enabled = false;

                Material emissiveMaterial = emissiveMaterials[i];
                if (emissiveMaterial)
                {
                    emissiveMaterial.SetColor("_Color", Color.black);
                }
            }
        }
    }
}