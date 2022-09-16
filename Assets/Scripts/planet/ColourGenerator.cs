using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class ColourGenerator  
{
    ColourSettings settings;
    Texture2D texture;
    INoiseFilter filter;
    const int textureResolution = 50;

    public void UpdateSettings(ColourSettings settings)
    {
        this.settings = settings;
        if (texture == null || texture.height != settings.biomeColourSettings.biomes.Length)
        {
            texture = new Texture2D(textureResolution, settings.biomeColourSettings.biomes.Length);
        }

        filter = NoiseFilterFactory.CreateNoiseFilter(settings.biomeColourSettings.noiseSettings);
    }


    public float BiomePercentFromPoint(Vector3 pointOnUniSphere)
    {
        float hightPercent = (pointOnUniSphere.y + 1) / 2;
        hightPercent += (filter.Evaluate(pointOnUniSphere) - settings.biomeColourSettings.noisOffset) *
                        settings.biomeColourSettings.noisStrength;
        float index = 0;
        int nb = settings.biomeColourSettings.biomes.Length;
        float blendRange = (settings.biomeColourSettings.blendAmount/2f) + 0.0001f;
        for (int i = 0; i < nb; i++)
        {
             float dist = hightPercent - settings.biomeColourSettings.biomes[i].startHight;
             float weight = Mathf.InverseLerp(-blendRange, blendRange , dist);
             index *= (1 - weight);
             index += i * weight;

        }
        return index / Mathf.Max(1,nb - 1);
    }

    public void UpdateElevation(MinMax elevationMinMax)
    {
        settings.PlanetMaterial.SetVector("_elevationMinMax",new  Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }
    public void UpdateColors()
    {
        Color[] colours = new Color[texture.width * texture.height];
        int colorIndex = 0;
        foreach (var biome in settings.biomeColourSettings.biomes)
        {
            for (int i = 0; i < textureResolution; i++)
            {
                 Color gradiantCol = biome.gradiant.Evaluate(i / textureResolution - 1f);
                 Color tintCol = biome.tint;
                 colours[colorIndex] = gradiantCol * (1 - biome.tintPercent)+ tintCol * biome.tintPercent;
                 colorIndex++;
            }
        }
        
        texture.SetPixels(colours);
        texture.Apply();
        settings.PlanetMaterial.SetTexture("_Texture", texture);//shadergraph
    }

   
}
