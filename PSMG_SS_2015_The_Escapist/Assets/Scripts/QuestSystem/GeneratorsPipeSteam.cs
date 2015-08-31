using UnityEngine;
using System.Collections;

public class GeneratorsPipeSteam : MonoBehaviour {

    public GeneratorsOven[] ovens;
    public GameObject visibleSteam;

    private int activeOvenNum = 0;

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

            if (activeOvenNum > 0) { visibleSteam.GetComponent<ParticleSystem>().loop = true; }
            else { visibleSteam.GetComponent<ParticleSystem>().loop = false; }
        }
    }

    public int getActiveOvenNum()
    {
        return activeOvenNum;
    }
}
