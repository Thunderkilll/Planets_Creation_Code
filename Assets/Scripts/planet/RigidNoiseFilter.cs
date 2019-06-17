using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter : INoiseFilter
{
    NoiseSettings.RigidNoiseSettings noiseSettings;
    Noise noise = new Noise();

    public RigidNoiseFilter(NoiseSettings.RigidNoiseSettings noiseSettings)
    {
        this.noiseSettings = noiseSettings;
    }
    public float Evaluate(Vector3 point)
    {
        //float noisevalue = (noise.Evaluate(point * noiseSettings.roughness + noiseSettings.centre) + 1) * .5f;
        //return noisevalue * noiseSettings.strength;
        float noisevalue = 0f;
        float frequency = noiseSettings.baseRoughness;
        float amplitude = 1f;
        float weight = 1f;
        for (int i = 0; i < noiseSettings.numLayers; i++)
        {
            //|(1-sin(x))|
            float v = 1-Mathf.Abs(noise.Evaluate(point * frequency + noiseSettings.centre));
        
            //|(1-sin(x))²| 
            v *= v;// v²
            v *= weight;
            
            weight = v;
       
            noisevalue +=  Mathf.Clamp01(v * amplitude  * noiseSettings.weightMultiplier ); // we want the value between 0 and 1 always 
          
            frequency *= noiseSettings.roughness;
            amplitude *= noiseSettings.persistance;
            //when roughness value is > 1 , frequency encrease within each layer ,
            //and when persistance < 1 then amplitude decrease whithin the layer 
        }
        noisevalue = Mathf.Max(0, noisevalue - noiseSettings.minValue);
        return noisevalue * noiseSettings.strength;
    }
}
