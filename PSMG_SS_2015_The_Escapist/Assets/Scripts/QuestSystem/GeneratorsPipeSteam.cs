using UnityEngine;
using System.Collections;

public class GeneratorsPipeSteam : MonoBehaviour {

    public GeneratorsOven[] ovens;
    public GameObject visibleSteam;

    private ParticleSystem steamParticles;
    private SphereCollider steamCollider;

    private int activeOvenNum = 0;

    void Start()
    {
        if (visibleSteam)
        {
            steamParticles = visibleSteam.GetComponent<ParticleSystem>();
            steamCollider = visibleSteam.GetComponent<SphereCollider>();
        }
    }

    void Update()
    {
        int tempOvenNum = 0;
        foreach (GeneratorsOven oven in ovens)
        {
            if (oven.getState() == 1) { tempOvenNum ++; }
        }

        activeOvenNum = tempOvenNum;

        if(visibleSteam)
        {

            if (activeOvenNum > 0) 
            {
                if (!steamParticles.isPlaying) { steamParticles.Play(); }
                steamCollider.enabled = true;
            }
            else 
            {
                steamParticles.Stop();
                steamCollider.enabled = false;
            }
        }
    }

    public int getActiveOvenNum()
    {
        return activeOvenNum;
    }
}
