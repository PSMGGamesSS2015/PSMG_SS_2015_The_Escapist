using UnityEngine;
using System.Collections;

public class ShadowDetection2 : MonoBehaviour
{

    public Color surfaceColor;
    public float brightness1; // http://stackoverflow.com/questions/596216/formula-to-determine-brightness-of-rgb-color 
    public float brightness2; // http://www.nbdtech.com/Blog/archive/2008/04/27/Calculating-the-Perceived-Brightness-of-a-Color.aspx
    public LayerMask layerMask;
    public int resWidth = 256;
    public int resHeight = 256; 

    void Update()
    {
        GetRenderTexture();

        // BRIGHTNESS APPROX
        brightness1 = (surfaceColor.r + surfaceColor.r + surfaceColor.b + surfaceColor.g + surfaceColor.g + surfaceColor.g) / 6;

        // BRIGHTNESS
        brightness2 = Mathf.Sqrt((surfaceColor.r * surfaceColor.r * 0.2126f + surfaceColor.g * surfaceColor.g * 0.7152f + surfaceColor.b * surfaceColor.b * 0.0722f));
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10f, 10f, Screen.width, Screen.height));

        GUILayout.Label("R = " + string.Format("{0:0.00}", surfaceColor.r));
        GUILayout.Label("G = " + string.Format("{0:0.00}", surfaceColor.g));
        GUILayout.Label("B = " + string.Format("{0:0.00}", surfaceColor.b));

        GUILayout.Label("Brightness Approx = " + string.Format("{0:0.00}", brightness1));
        GUILayout.Label("Brightness = " + string.Format("{0:0.00}", brightness2));

        GUILayout.EndArea();
    }

    void GetRenderTexture()
    {
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        GetComponent<Camera>().targetTexture = rt;
        Texture2D render = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);

        GetComponent<Camera>().Render();
        RenderTexture.active = rt;
        render.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);

        Color surfaceColor = render.GetPixel(0, 0);

        Destroy(rt);
        GetComponent<Camera>().targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors

        // APPLY
        this.surfaceColor = surfaceColor;
    }
}