using UnityEngine;
using System.Collections;

public class HurtEffect : MonoBehaviour
{

    public Texture hurtEffect;

    private float displayTime = 5.0f;
    public bool displayHurtEffect = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            displayHurtEffect = true;
        }
    }


    void OnGUI()
    {
        if (displayHurtEffect)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), hurtEffect, ScaleMode.StretchToFill);
            StartCoroutine(StopDisplayingEffect());
        }


    }

    IEnumerator StopDisplayingEffect()
    {
        yield return new WaitForSeconds(displayTime);
        displayHurtEffect = false;
    }
}
