using System;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Brightness")]
public class Brightness : MonoBehaviour
{

    /// <summary>
    /// Script for BrightnessShader
    /// </summary>

    public Shader shaderDerp;
    Material m_Material;

    [Range(0.5f, 2f)]
    public float brightness = Constants.BRIGHTNESS;

    void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }

      
        if (!shaderDerp || !shaderDerp.isSupported)
            enabled = false;
    }
    void Update()
    {

        brightness = Constants.BRIGHTNESS;
    }


    Material material
    {
        get
        {
            if (m_Material == null)
            {
                m_Material = new Material(shaderDerp);
                m_Material.hideFlags = HideFlags.HideAndDontSave;
            }
            return m_Material;
        }
    }


    void OnDisable()
    {
        if (m_Material)
        {
            DestroyImmediate(m_Material);
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_Brightness", brightness);
        Graphics.Blit(source, destination, material);
    }


}