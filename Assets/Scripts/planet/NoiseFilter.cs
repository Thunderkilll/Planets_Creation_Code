using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilter : INoiseFilter
{
    NoiseSettings.SimpleNoiseSettings noiseSettings;
    Noise noise = new Noise();

    public NoiseFilter(NoiseSettings.SimpleNoiseSettings noiseSettings)
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
        for (int i = 0; i < noiseSettings.numLayers; i++)
        {
            float v = noise.Evaluate(point * frequency +noiseSettings.centre);
            noisevalue += (v + 1) * .5f * amplitude ;
            frequency *= noiseSettings.roughness;
            amplitude *= noiseSettings.persistance;
            //when roughness value is > 1 , frequency encrease within each layer ,
            //and when persistance < 1 then amplitude decrease whithin the layer 
        }
        noisevalue = Mathf.Max(0, noisevalue - noiseSettings.minValue);
        return noisevalue * noiseSettings.strength;
    }
}
