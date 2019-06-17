using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMax  
{

    //to know the lowest point to the higheest point in the planet 
    public float Min { get; private set; }
    public float Max { get; private set; }
    public MinMax()
    {
        Min = float.MaxValue;
        Max = float.MinValue;

    }
    public void AddValue(float v)
    {
        if (v > Max)
        {
            Max = v;
        }
        if (v < Min)
        {
            Min = v;
        }
    }
}
