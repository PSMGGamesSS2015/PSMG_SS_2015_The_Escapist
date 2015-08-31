using UnityEngine;
using System.Collections;

public class GeneratorsBrokenPipe : InteractiveObject {

    public GameObject missingPart;
    public GameObject steam;

    private enum States { broken = 0, repaired = 1 }
    private States state = States.broken;

    public override void trigger()
    {
        if (state == States.broken)
        {
            missingPart.SetActive(true);
            steam.SetActive(false);
            state = States.repaired;
        }
    }

    public override int getState()
    {
        return (int)(state);
    }

    public override bool hasInteractions()
    {
        return (state == States.broken);
    }
}
