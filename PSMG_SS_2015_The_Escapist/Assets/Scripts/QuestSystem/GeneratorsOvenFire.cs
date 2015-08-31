using UnityEngine;
using System.Collections;

public class GeneratorsOvenFire : InteractiveObject {

    public GameObject fire;

    public enum States { embers = 0, burning = 1 }
    public States state = States.embers;

    public override void trigger()
    {
        if(state == States.embers)
        {
            fire.SetActive(true);
            state = States.burning;
        }
    }

    public override int getState()
    {
        return (int)(state);
    }

    public override bool hasInteractions()
    {
        return (state == States.embers);
    }
}
