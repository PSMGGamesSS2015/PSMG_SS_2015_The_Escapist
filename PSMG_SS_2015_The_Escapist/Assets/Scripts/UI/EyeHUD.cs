using UnityEngine;
using System.Collections;

public class EyeHUD : MonoBehaviour {

    //
    // Script for displaying EyeTexture
    //

    public GameObject openeye, openeyered;
    private FollowAI fai;
    private Color color;
    private GamingControl gc;
 
    private float shadowPercentage;

    
      
	void Start () {

        color = new Color(1f, 1f, 1f);

        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GamingControl>();
        fai = GameObject.FindWithTag("Enemy").GetComponent<FollowAI>();
    }

    void Update()
    {
        shadowPercentage = gc.getPlayerHiddenPercentage();

        // Change Alpha of Texture
        color.a = 0.9f - shadowPercentage / 100;

    }

    void OnGUI()
    {

        bool safe = (shadowPercentage > 90) ? true : false;

        GUI.Label(new Rect(20, 80, 120, 256), "Versteckt: " + (int)(shadowPercentage) + "%");

        // States for Eye Texture
        if (fai.isChasing() == true)
        {
            openeye.SetActive(false);
            openeyered.SetActive(true);

        }
        else if (safe && fai.isChasing() == false)
        {
            openeye.SetActive(true);
            openeyered.SetActive(false);
            openeye.GetComponent<CanvasRenderer>().SetColor(color);

        }
        else if (!safe && fai.isChasing() == false)
        {
            openeye.SetActive(true);
            openeyered.SetActive(false);
            openeye.GetComponent<CanvasRenderer>().SetColor(color);

        }
        
    }
}
