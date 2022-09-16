﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColourSettings : ScriptableObject
{ 
    public Material PlanetMaterial;
    public BiomeColourSettings biomeColourSettings;
    public class BiomeColourSettings {
        public Biome[] biomes;
        public class Biome {
            public Gradient gradiant;
            public Color tint;
            [Range(0f, 1f)]
            public float startHight;
            [Range(0f, 1f)]
            public float tintPercent;
        }
    }
    /*
    
    ShapeSettings settings;

    public ShapeGenerator()
    {

    }
    public ShapeGenerator(ShapeSettings settings)
    {
        this.settings = settings;
    }

  public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        return pointOnUnitSphere * settings.planetRadius;
    }
     */

}
