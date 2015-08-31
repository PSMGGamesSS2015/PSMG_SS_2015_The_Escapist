using UnityEngine;
using System.Collections;

public class GeneralActionTrigger : FocusTrigger {

    public bool needsInput = true;
    public string inputButtonName = "Use";
    public bool needsItem = false;
    public string itemName;
    public int itemCount;
    public InteractiveObject[] interactiveObjects;
    public int[] neededStates;

    private GameObject player;
    private PlayerInventory playerInventory;

    void Start()
    {
        if (needsItem)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerInventory = player.GetComponent<PlayerInventory>();
            Debug.Log(playerInventory);
        }
    }

    void Update()
    {
        if (focused)
        {
            if(areConditionsFulfilled())
            {
                GetComponent<InteractiveObject>().trigger();
                if (needsItem) { playerInventory.removeItem(itemName, itemCount); }
            }
        }
    }

    private bool areConditionsFulfilled()
    {
        bool b = true;
        if (needsInput) { b &= Input.GetButtonDown(inputButtonName); }
        if (needsItem) { b &= (playerInventory.getItemCount(itemName) >= itemCount); }

        for (int i = 0; i < interactiveObjects.Length; i++)
        {
            b &= (interactiveObjects[i].getState() == neededStates[i]);
        }

        return b;
    }
}