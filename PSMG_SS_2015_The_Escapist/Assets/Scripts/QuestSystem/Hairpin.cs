using UnityEngine;
using System.Collections;

public class Hairpin : InteractiveObject {

    public GameObject hairpin;

    private enum States { burnAround = 0, pickedUp = 1 }
    private States state = States.burnAround;

    public override void trigger()
    {
        if (state == States.burnAround)
        {
            hairpin.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().pickedUpHairpin();
            state = States.pickedUp;
        }
    }

    public override int getState()
    {
        return (int)(state);
    }

    public override bool hasInteractions()
    {
        return (state == States.burnAround);
    }
}
