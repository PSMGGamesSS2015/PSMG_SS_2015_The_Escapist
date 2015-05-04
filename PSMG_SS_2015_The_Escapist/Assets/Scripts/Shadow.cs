using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour
{

    public GameObject player;
    public bool isPlayerInLight;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 rayDirection = player.transform.position - transform.position;

        if (Physics.Raycast(transform.position, rayDirection, out hit))
        {

            if (hit.transform.tag == "Player")
            {

                isPlayerInLight = true;
           }
            else if (hit.transform.tag != "Enemy")
            {
                isPlayerInLight = false;
        
            }

            else if (hit.transform.tag == "Collider")
            {
                isPlayerInLight = false;
            }
    }
}
    void OnGUI()
    {
        if (isPlayerInLight == false)
        {
            GUILayout.Label("Versteckt");
        }
    }
}
