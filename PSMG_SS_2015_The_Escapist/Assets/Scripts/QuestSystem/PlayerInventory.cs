using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour {

    private GameObject player;
    private List<Item> inventory;
    private bool hairpinActive = false;

    private enum ItemClass { Normal, Heavy, Throwable };


	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = new List<Item>();
	}

    public void addItem(Item item)
    {
        inventory.Add(item);
        if(item.getClass() == (int)(ItemClass.Heavy))
        {
            //Slow down Player!
        }
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

    public void removeItem(string name, int count)
    {
        foreach (Item inventoryItem in inventory)
        {
            if (inventoryItem.name == name)
            {
                inventory.Remove(inventoryItem);

                if (inventoryItem.getClass() == (int)(ItemClass.Heavy))
                {
                    //Remove Slowdown From Player!
                }

            }
        }
    }

    public void pickedUpHairpin()
    {
        hairpinActive = true;
    }

    public bool isHairpinActive()
    {
        return hairpinActive;
    }
}