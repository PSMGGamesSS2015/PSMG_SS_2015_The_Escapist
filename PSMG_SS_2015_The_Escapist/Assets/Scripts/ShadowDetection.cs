using UnityEngine;
using System.Collections;
using System;

public class ShadowDetection : MonoBehaviour
{
    public float brightnessApprox;
    public float brightness;
    public float hiddenPercentage;
    public Color avgSurfaceColor;
    public LayerMask layerMask;
    public Texture mapMarkerTex;
    public float probes = 25;
    public float detectionRadius = 20;
    public float alphaFactor = 5;
    
    private Texture2D lightmapTex;
    private Vector2 pixelUV;
    private bool debugMode = false;

    void Update()
    {
        RaycastAndUpdateLightmapData();
        this.avgSurfaceColor = calcAverageColorAroundPlayer();
        calcDetectionValues();

        if (Input.GetKeyUp(KeyCode.U)) { debugMode = !debugMode; }
    }

    void calcDetectionValues()
    {
        // Berechnungsformel: http://stackoverflow.com/questions/596216/formula-to-determine-brightness-of-rgb-color 
        brightnessApprox = (avgSurfaceColor.r + avgSurfaceColor.r + avgSurfaceColor.b + avgSurfaceColor.g + avgSurfaceColor.g + avgSurfaceColor.g) / 6;

        // Berechnungsformel: http://www.nbdtech.com/Blog/archive/2008/04/27/Calculating-the-Perceived-brightness-of-a-Color.aspx
        brightness = Mathf.Sqrt((avgSurfaceColor.r * avgSurfaceColor.r * 0.2126f + avgSurfaceColor.g * avgSurfaceColor.g * 0.7152f + avgSurfaceColor.b * avgSurfaceColor.b * 0.0722f));

        hiddenPercentage = Math.Max(0, 100 - (brightness * (avgSurfaceColor.a * 4)) * 100);
    }

    void OnGUI()
    {
        if (debugMode) showDebugHUD();
        else showHUD();
    }

    void showDebugHUD()
    {
        GUILayout.BeginArea(new Rect(10f, 10f, Screen.width, Screen.height));

        GUILayout.Label("R = " + string.Format("{0:0.00}", avgSurfaceColor.r));
        GUILayout.Label("G = " + string.Format("{0:0.00}", avgSurfaceColor.g));
        GUILayout.Label("B = " + string.Format("{0:0.00}", avgSurfaceColor.b));
        GUILayout.Label("A = " + string.Format("{0:0.00}", avgSurfaceColor.a));

        GUILayout.Label("" + hiddenPercentage);

        GUIStyle style = new GUIStyle();
        style.normal.textColor = avgSurfaceColor;
        GUILayout.Label("████████", style);

        GUILayout.Label("brightness Approx = " + string.Format("{0:0.00}", brightnessApprox));
        GUILayout.Label("brightness = " + string.Format("{0:0.00}", brightness));

        Vector2 mapOffset = new Vector2(300, -50);
        int mapMarkerSize = 10;
        GUI.DrawTexture(new Rect(mapOffset.x, mapOffset.y, lightmapTex.width, lightmapTex.height), lightmapTex, ScaleMode.StretchToFill, true, 0);
        GUI.DrawTexture(new Rect((pixelUV.x * lightmapTex.width) + mapOffset.x - mapMarkerSize / 2, (lightmapTex.height - (pixelUV.y * lightmapTex.height)) + mapOffset.y - mapMarkerSize / 2, mapMarkerSize, mapMarkerSize), mapMarkerTex, ScaleMode.ScaleToFit, true, 0);

        GUILayout.EndArea();
    }

    void showHUD()
    {
        bool safe = (hiddenPercentage > 70) ? true : false;

        GUI.Label(new Rect(10, 10, 120, 256), "Versteckt: " + (int)(hiddenPercentage) + "%");
        GUI.Label(new Rect(10, 30, 100, 256), "Sicher: " + safe.ToString());
    }

    void RaycastAndUpdateLightmapData()
    {
        // RAY TO PLAYER'S FEET
        Ray ray = new Ray(transform.position + new Vector3(0, 0.1f, 0), -Vector3.up);
        Debug.DrawRay(ray.origin, ray.direction*1.5f, Color.magenta);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5f, layerMask))
        {
            // GET RENDERER OF OBJECT HIT
            Renderer hitRenderer = hit.collider.GetComponent<Renderer>();
            //Debug.Log(LightmapSettings.lightmaps.Length + " " + hitRenderer.lightmapIndex);

            // GET LIGHTMAP APPLIED TO OBJECT
            LightmapData lightmapData = LightmapSettings.lightmaps[hitRenderer.lightmapIndex];

            // STORE LIGHTMAP TEXTURE
            Texture2D lightmapTex = lightmapData.lightmapFar;
            this.lightmapTex = lightmapTex;

            // GET LIGHTMAP COORDINATE WHERE RAYCAST HITS
            Vector2 pixelUV = hit.lightmapCoord;
            this.pixelUV = pixelUV;
            //Debug.Log("x: " + pixelUV.x * 1024 + " y: " + pixelUV.y * 1024);
        }
    }

    Color calcAverageColorAroundPlayer()
    {
        int xNorm = (int)(pixelUV.x * lightmapTex.width);
        int yNorm = (int)(pixelUV.y * lightmapTex.height);
        //Debug.Log("xNorm: " + xNorm + " yNorm: " + yNorm);

        float r = 0;
        float g = 0;
        float b = 0;
        float a = 0;

        float steps = detectionRadius * 2 / (float)(Math.Sqrt(probes));

        //Debug.Log("steps: " + steps);
        //Debug.Log("detectionRadius: " + detectionRadius);

        int actualProbes = 0; // for debug

        for (float x = xNorm - detectionRadius; x < (xNorm + detectionRadius) - 0.1; x += steps)
        {
            for (float y = yNorm - detectionRadius; y < (yNorm + detectionRadius) - 0.1; y += steps)
            {
                Color avgSurfaceColor = lightmapTex.GetPixelBilinear(x/1024f, y/1024f);
                r += avgSurfaceColor.r;
                g += avgSurfaceColor.g;
                b += avgSurfaceColor.b;
                a += avgSurfaceColor.a;
                actualProbes++; // for debug

                //Debug.Log("x: " + x/1024f + " y: " + y/1024f);
                //Debug.Log("x: " + x + " y: " + y);
            }
        }
        //Debug.Log("actual probes: " + actualProbes);

        return new Color(r/probes, g/probes, b/probes, a/alphaFactor);
    }

    public int getHiddenPercentage()
    {
        return (int)(hiddenPercentage);
    }
}