using UnityEngine;
using System.Collections;

public class HurtEffect : MonoBehaviour
{

    public Texture hurtEffect;
    private int counter = 0;
    private AudioSource audio;

    private GUIStyle style;

    public bool displayHurtEffect = false;
    public bool displayDead = false;

    /// <summary>
    /// Check if enemy hit player
    /// </summary>
    /// 

    void Start()
    {
        audio = GetComponent<AudioSource>();

    }
    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.CompareTag("Enemy"))
        {
            if (counter == 2)
            {
                displayDead = true;
                StartCoroutine(WaitForRestart());
            }
            else
            {
                counter++;
                audio.Play();

                displayHurtEffect = true;
            }
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

        if (displayDead)
        {
            style = new GUIStyle();
            style.fontSize = 72;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), hurtEffect, ScaleMode.StretchToFill);
            GUI.Label(new Rect(100, 100, 120, 256), "Du bist deinen Verletzungen erlegen!", style);

        }


    }

    /// <summary>
    /// Method for displaying the HurtEffect for a specific time
    /// </summary>
    IEnumerator StopDisplayingEffect()
    {
        yield return new WaitForSeconds(Constants.DISPLAY_TIME);
     

        counter = 0;
        displayHurtEffect = false;
    }

    IEnumerator WaitForRestart()
    {
        yield return new WaitForSeconds(3.0f);
        Application.LoadLevel(Application.loadedLevel);
        
       
    }
}
