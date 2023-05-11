using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivePulseMesh : MonoBehaviour
{
    Mesh mesh;
    Vector3[] verticies;
    int[] triangles;
    int xSize = 200;
    int zSize = 200;
    float time = 0;
    PulseEquation equation;
    float[] wVals = {1f, 2f, 3f, 4f};

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
        equation = new PulseEquation(xSize, zSize, wVals, 1f);

    }

    // Update is called once per frame
    void Update()
    {
        equation.updateWaveArray(time);
        for (int i = 0; i<(xSize+1)*(zSize+1); i++) 
        {
            Vector3 vert = verticies[i];
            verticies[i].y = 20*equation.y((int)vert.x, (int)vert.z);
        }
        UpdateMesh();
        time += Time.deltaTime;
    }

    void CreateShape()
    {
        verticies = new Vector3[(xSize +1)*(zSize+1)];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                verticies[i] = new Vector3(x, 0, z);
                i++;
            }
        }
        triangles = new int[xSize * zSize *6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < xSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;

                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }

            

    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();    
        mesh.RecalculateNormals();    
        
    }
}
