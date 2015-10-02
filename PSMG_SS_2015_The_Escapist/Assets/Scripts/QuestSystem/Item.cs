using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public string name;
    public AudioClip pickUpSound;

    public enum ItemClass { Normal, Heavy, Throwable };
    public ItemClass itemClass;

    private GameObject player;
    private PlayerInventory playerInventory;
    private AudioSource audioSource;
    private Material originalMaterial;
    private bool focused = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();

        AudioSource[] audioSources = player.GetComponents<AudioSource>();
        if(audioSources.Length > 1)
        {
            audioSource = audioSources[1];
        }
    }

    void Update()
    {
        if (focused && Input.GetButtonDown("Use"))
        {
            if(audioSource && pickUpSound)
            {
                audioSource.clip = pickUpSound;
                audioSource.Play();
            }
            playerInventory.addItem(this);
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
