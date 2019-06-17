using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColourSettings : ScriptableObject
{

    public Gradient gradiant;
    public Material PlanetMaterial;

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
