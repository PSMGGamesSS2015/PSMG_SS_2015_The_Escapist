using UnityEngine;
using System.Collections;

public class Spider : MonoBehaviour
{

    private GameObject player;
    private AudioSource audio;
    // Use this for initialization
     private float fallSpeed = 6.0f;
     private bool spiderFalling = false;

     void Start()
    {
        player = GameObject.FindWithTag("Player");
        audio = GetComponent<AudioSource>();
    }

     void Update()
     {

         if (spiderFalling)
             transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
         
     }

    // Update is called once per frame
   
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            spiderFalling = true;
            audio.Play();
        }
    }
}
