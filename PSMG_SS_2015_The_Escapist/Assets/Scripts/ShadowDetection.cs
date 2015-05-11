using UnityEngine;
using System.Collections;

public class ShadowDetection : MonoBehaviour
{

    public Color surfaceColor;
    public float brightness1; // http://stackoverflow.com/questions/596216/formula-to-determine-brightness-of-rgb-color 
    public float brightness2; // http://www.nbdtech.com/Blog/archive/2008/04/27/Calculating-the-Perceived-Brightness-of-a-Color.aspx
    public LayerMask layerMask;

    void Update()
    {
        Raycast();

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

    void Raycast()
    {
        // RAY TO PLAYER'S FEET
        Ray ray = new Ray(transform.position, -Vector3.up);
        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.magenta);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 5f, layerMask))
        {
            // GET RENDERER OF OBJECT HIT
            Renderer hitRenderer = hitInfo.collider.GetComponent<Renderer>();

            // GET LIGHTMAP APPLIED TO OBJECT
            Debug.Log(LightmapSettings.lightmaps.Length + " " + hitRenderer.lightmapIndex);
            LightmapData lightmapData = LightmapSettings.lightmaps[hitRenderer.lightmapIndex];

            // STORE LIGHTMAP TEXTURE
            Texture2D lightmapTex = lightmapData.lightmapFar;

            // GET LIGHTMAP COORDINATE WHERE RAYCAST HITS
            Vector2 pixelUV = hitInfo.lightmapCoord;

            // GET COLOR AT THE LIGHTMAP COORDINATE
            Color surfaceColor = lightmapTex.GetPixelBilinear(pixelUV.x, pixelUV.y);

            // APPLY
            this.surfaceColor = surfaceColor;
        }
    }
}