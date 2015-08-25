using UnityEngine;
using System.Collections;

public class TextBackTrigger : MonoBehaviour {

	public GameObject TextBack;

	// Use this for initialization
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
			TextBack.SetActive(true);
        }
    }
}
