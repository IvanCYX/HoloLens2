using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]

public class MeshGeneratorFlipped : MonoBehaviour
{
    Mesh mesh;
    Vector3[] verts;
    int[] triangles;
    public TextAsset jsonFile;


    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();


    }

    void CreateShape()
    {

        List<int> tris;

        tris = JsonUtility.FromJson<TriIndexes>(jsonFile.text).triindexes;
        Debug.Log(tris[0]);
        foreach (int num in tris)
        {
            Debug.Log(num);
        }
        triangles = tris.ToArray();


        List<Vector3> vs = new List<Vector3>();
        Vertices verticesInJson = JsonUtility.FromJson<Vertices>(jsonFile.text);
        // Debug.Log(verticesInJson.vertices);
        int vertexcount = 1;
        foreach (Vertex vertex in verticesInJson.vertices)
        {
            //Debug.Log(vertex.x);
            vs.Add(new Vector3(vertex.x / 500, vertex.y / 500, vertex.z / 500));
            vertexcount++;
        }
        verts = vs.ToArray();



    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }


}
