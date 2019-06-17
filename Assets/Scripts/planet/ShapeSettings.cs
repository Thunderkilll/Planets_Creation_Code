using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    //we will have load of stuff heheheh

    public float planetRadius = 1f;
    public NoiseLayer[] noiseLayers;
    [System.Serializable]
    public class NoiseLayer
    {
        public bool enable = true;
        public bool isFirstLayerAsMask ;
        public NoiseSettings noiseSettings;

    }

}
