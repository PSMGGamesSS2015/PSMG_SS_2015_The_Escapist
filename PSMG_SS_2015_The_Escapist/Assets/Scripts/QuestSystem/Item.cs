using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public string name;
    public AudioClip pickUpSound;

    public enum ItemClass { Normal, Heavy, Throwable };
    public ItemClass itemClass;

    private GameObject player;
    private PlayerInventory playerInventory;
    private AudioSource[] audioSources;
    private Material originalMaterial;
    private bool focused = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
        audioSources = player.GetComponents<AudioSource>();
    }

    void Update()
    {
        if (focused && Input.GetButtonDown("Use"))
        {
            if(audioSources[1] && pickUpSound)
            {
                audioSources[1].clip = pickUpSound;
                audioSources[1].Play();
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
