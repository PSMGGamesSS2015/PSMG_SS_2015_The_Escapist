using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour {

    private GameObject player;
    private List<Item> inventory;

	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = new List<Item>();
	}

    public void addItem(Item newItem)
    {
        inventory.Add(newItem);
    }

    public int getItemCount(string itemName)
    {
        int itemCount = 0;

        foreach(Item inventoryItem in inventory)
        {
            if (inventoryItem.name == itemName)
            {
                itemCount ++;
            }
        }

        return itemCount;
    }
}