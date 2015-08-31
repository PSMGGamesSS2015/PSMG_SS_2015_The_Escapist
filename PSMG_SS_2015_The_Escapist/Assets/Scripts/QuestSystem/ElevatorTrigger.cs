using UnityEngine;
using System.Collections;

public class ElevatorTrigger : FocusTrigger {

    public Generators[] generatorSides;
    public Elevator elevator;
    public bool trigger = false;

    void Update()
    {
        if(focused && Input.GetButtonDown("Use"))
        {
            int activeGeneratorSidesNum = 0;
            foreach (Generators generatorSide in generatorSides)
            {
                if (generatorSide.getState() == 1) { activeGeneratorSidesNum++; }
            }

            if(activeGeneratorSidesNum == 2)
            {
                elevator.trigger();
            }
        }

        if (trigger) 
        {
            trigger = false; 
            elevator.trigger(); 
        }
    }
}
