using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public string name;

    public enum ItemClass { Normal, Heavy, Throwable };
    public ItemClass itemClass;

    private GameObject player;
    private PlayerInventory playerInventory;
    private Material originalMaterial;
    private bool focused = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (focused && Input.GetButtonDown("Use"))
        {
            transform.gameObject.SetActive(false);
            playerInventory.addItem(this);
        }
    }

    public void setFocused(bool b)
    {
        focused = b;
    }

    public string getName()
    {
        return name;
    }

    public int getClass()
    {
        return (int)(itemClass);
    }
}
