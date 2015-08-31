using UnityEngine;
using System.Collections;

public class GeneratorsTrigger : MonoBehaviour {

    public int connectedOvenNum;
    public GeneratorsPipeSteam steam;
    public GeneratorsBrokenPipe brokenPipe;

    public Generators generators;

    void Update()
    {
        if(steam.getActiveOvenNum() == connectedOvenNum && (!brokenPipe || brokenPipe.getState() == 1))
        {
            generators.trigger(1);
        }
        else
        {
            generators.trigger(0);
        }
    }
}
