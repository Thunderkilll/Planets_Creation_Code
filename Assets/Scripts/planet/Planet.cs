using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2 ,256)]
    public int resolution = 10;
    [SerializeField , HideInInspector]
    MeshFilter[] meshFilters;
    //array of terrain faces 
    TerrainFace[] terrainFaces;
    public enum FacesMaskRender { All , Top , Bottom , Left , Right}
    public FacesMaskRender faceMaskRender;
    //shape seeting & color settings 
    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;
    [HideInInspector]
    public bool shapeSettingsFoldout = true;
    [HideInInspector]
    public bool colorSettingsFoldout = true;
    //auto update 
    public bool autoUpdate = true;
    //planet generator
    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColourGenerator colorGenerator = new ColourGenerator();

    void Initialize()
    {
        shapeGenerator.UpdateSettings(shapeSettings);

        colorGenerator.UpdateSettings(colourSettings);

        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
      
        terrainFaces = new TerrainFace[6];
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.right, Vector3.left, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;
                //add mesh renderer 
                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();

            }

            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colourSettings.PlanetMaterial;
            //assemble
            terrainFaces[i] = new TerrainFace(shapeGenerator ,meshFilters[i].sharedMesh, resolution, directions[i]);
            bool renderFace = faceMaskRender == FacesMaskRender.All || (int)faceMaskRender - 1 == i;//index of render mask 
            meshFilters[i].gameObject.SetActive(renderFace);
        }
    }


    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }
    public void OnShapeSettingsUpdated() //changing shape
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
        
    }
    public void OnColourSettingsUpdated() //change colors 
    {

        if (autoUpdate)
        {
            Initialize();
            GenerateColours();
        }
    }
    void GenerateMesh()
    {
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConsructMesh();
            }
        }
        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }
    void GenerateColours()
    {
        colorGenerator.UpdateColors();
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].UpdateUVs(colorGenerator);
            }
        }
    }
}

 