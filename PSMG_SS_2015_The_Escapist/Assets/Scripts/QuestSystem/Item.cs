using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public string name;
    public AudioClip pickUpSound;

    public enum ItemClass { Normal, Heavy, Throwable };
    public ItemClass itemClass;

    private GameObject player;
    private PlayerInventory playerInventory;
    private AudioSource audioSrc;
    private Material originalMaterial;
    private bool focused = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
        audioSrc = player.GetComponents<AudioSource>()[1];
    }

    void Update()
    {
        if (focused && Input.GetButtonDown("Use"))
        {
            if(audioSrc && pickUpSound)
            {
                audioSrc.clip = pickUpSound;
                audioSrc.Play();
            }
            
            transform.gameObject.SetActive(false);
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
