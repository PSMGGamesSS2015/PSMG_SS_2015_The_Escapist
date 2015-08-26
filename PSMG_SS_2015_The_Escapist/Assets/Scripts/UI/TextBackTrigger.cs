using UnityEngine;
using System.Collections;

public class TextBackTrigger : MonoBehaviour {

    // <summary>
    /// Initialisation
    /// </summary>
    // Script for activating textBackCollider
    //
    

	public GameObject TextBack;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
			TextBack.SetActive(true);
        }
    }
}
