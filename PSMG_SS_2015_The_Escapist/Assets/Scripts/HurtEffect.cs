using UnityEngine;
using System.Collections;

public class HurtEffect : MonoBehaviour
{

    public Texture hurtEffect;

    public bool displayHurtEffect = false;

    /// <summary>
    /// Check if enemy hit player
    /// </summary>
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            displayHurtEffect = true;
        }
    }

    /// <summary>
    /// GUI Method for drawing HurtEffect
    /// </summary>
    void OnGUI()
    {
        if (displayHurtEffect)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), hurtEffect, ScaleMode.StretchToFill);
            StartCoroutine(StopDisplayingEffect());
        }


    }

    /// <summary>
    /// Method for displaying the HurtEffect for a specific time
    /// </summary>
    IEnumerator StopDisplayingEffect()
    {
        yield return new WaitForSeconds(Constants.DISPLAY_TIME);
        displayHurtEffect = false;
    }
}
