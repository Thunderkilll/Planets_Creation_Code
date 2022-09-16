using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace 
{
    Mesh mesh; //terrain face mesh 
    int resolution; //detail of the face mesh 
    Vector3 localUp; //witch way facing 
    Vector3 axisA; //axe x 
    Vector3 axisB; //axe y
    ShapeGenerator shapeGenerator;
    public TerrainFace(ShapeGenerator shapeGenerator ,Mesh mesh , int resolution , Vector3 localUp)
    {
        this.shapeGenerator = shapeGenerator;
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;
        axisA = new Vector3(localUp.y , localUp.z , localUp.x);
        axisB = Vector3.Cross(localUp, axisA); //perpondiculaire 3ala axisA and localUp
    }
    //construct mesh
    public void ConsructMesh()
    {
        //array of vector3 to hold vertices 
        Vector3[] vertices = new Vector3[resolution * resolution];
        //determine how many triangles in a mesh 
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        int triIndex = 0;

        Vector2[] uv = mesh.uv;
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
              
                    int i = x + y * resolution;//index of x and y  multipler par la resolution ala5atir we are doing a row 
                    Vector2 percent = new Vector2(x, y) / (resolution - 1);//tell us how near we are to the end of the loops 
                    Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                    vertices[i] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere);//change shape turn the cube into a sphere
                if (x != (resolution-1) && y != (resolution -1))
                {
                    //first triangle 
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i + 1 + resolution;
                    triangles[triIndex + 2] = i + resolution;
                    //second triangle 
                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;
                    triIndex += 6;
                }
           
            }

        }
        mesh.Clear(); //clear all the data from the mesh so no errors will be found 
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        //recalculate normals of these faces 
        mesh.RecalculateNormals();
        mesh.uv = uv;
    }
    public void UpdateUVs(ColourGenerator colourGenerator)
    {
        Vector2[] uv = new Vector2[resolution * resolution];
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {

                int i = x + y * resolution;//index of x and y  multipler par la resolution ala5atir we are doing a row 
                Vector2 percent = new Vector2(x, y) / (resolution - 1);//tell us how near we are to the end of the loops 
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                uv[i] = new Vector2(colourGenerator.BiomePercentFromPoint(pointOnUnitSphere) , 0);

            }
            mesh.uv = uv;
        }
    }
}
