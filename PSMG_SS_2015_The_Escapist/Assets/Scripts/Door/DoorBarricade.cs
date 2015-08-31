using UnityEngine;
using System.Collections;

public class DoorBarricade : InteractiveObject {

    public GameObject baricade;
    public GameObject brokenPieces;

    public enum States { intact = 0, destroyed = 1 }
    public States state = States.intact;

    public override void trigger()
    {
        if (state == States.intact)
        {
            baricade.SetActive(false);
            brokenPieces.SetActive(true);
            state = States.destroyed;
        }
    }

    public override int getState()
    {
        return (int)(state);
    }

    public override bool hasInteractions()
    {
        return (state == States.intact);
    }
}
