using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class NoiseSettings {
    //enum filter types so we can add more than 1 type of filters 
    public enum  FilterType  {   Simple  , Rigid    };
    public FilterType filterType;
    [ConditionalHide("filterType" , 0)] //simple filter type 
    public SimpleNoiseSettings simpleNoiseSettings;
    [ConditionalHide("filterType", 1)]//rigid filter type 
    public RigidNoiseSettings rigidNoiseSettings;
    //add layers for the noise to decrease it 
    [System.Serializable]
    public class SimpleNoiseSettings
    {
        [Range(1, 10)]
        public int numLayers = 1;
        public float strength = 1f;
        public float baseRoughness = 1f;
        public float roughness = 2f;
        //amplitude in the half the persistance 
        public float persistance = .5f;
        public Vector3 centre;
        public float minValue;
    }

    [System.Serializable]
    public class RigidNoiseSettings : SimpleNoiseSettings
    {
        //[Range(0,1)]
        public float weightMultiplier = .8f;
    }
 

}
