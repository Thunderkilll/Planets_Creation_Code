using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{

    ShapeSettings settings;
    //NoiseFilter[] noiseFilters;
    INoiseFilter[] noiseFilters;
    public MinMax elevationMinMax; //assign in the editor 
    public void UpdateSettings(ShapeSettings settings)
    {
        this.settings = settings;
        noiseFilters = new INoiseFilter[settings.noiseLayers.Length];
        for (int i = 0; i < noiseFilters.Length; i++)
        {
            //noiseFilters[i] = new NoiseFilter(settings.noiseLayers[i].noiseSettings);
            noiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
        elevationMinMax = new MinMax();
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float firstLayervalue = 0f;
        float elevation = 0f;
        if (noiseFilters.Length > 0)
        {
            firstLayervalue = noiseFilters[0].Evaluate(pointOnUnitSphere);
            if (settings.noiseLayers[0].enable)
            {
                //set the mask for the mountains and islands to grow on 
                elevation = firstLayervalue;
            }
        }
        for (int i = 1; i < noiseFilters.Length; i++)
        {
            if (settings.noiseLayers[i].enable)
            {
                float mask = (settings.noiseLayers[i].isFirstLayerAsMask) ? firstLayervalue : 1;//depending on the value of isFirstLayerAsMask set mask = firstLayervalue else mask = 1
                elevation += noiseFilters[i].Evaluate(pointOnUnitSphere)* mask;
            }
           
        }
        elevation = settings.planetRadius * (1 + elevation);
        elevationMinMax.AddValue(elevation);
        return pointOnUnitSphere * elevation ;
    }
}
