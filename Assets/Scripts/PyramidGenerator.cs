using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidGenerator : MonoBehaviour
{
    public float baseWidth = 2f;
    public float height = 3f;

    private void Awake()
    {
        GeneratePyramidPrefab();
    }

    private void GeneratePyramidPrefab()
    {
        GameObject pyramidPrefab = new GameObject("Pyramid");
        MeshFilter meshFilter = pyramidPrefab.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = pyramidPrefab.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        Vector3[] vertices = new Vector3[5];
        int[] triangles = new int[18];

        // Base vertices
        vertices[0] = new Vector3(-baseWidth / 2f, 0f, -baseWidth / 2f);
        vertices[1] = new Vector3(-baseWidth / 2f, 0f, baseWidth / 2f);
        vertices[2] = new Vector3(baseWidth / 2f, 0f, baseWidth / 2f);
        vertices[3] = new Vector3(baseWidth / 2f, 0f, -baseWidth / 2f);
        // Apex vertex
        vertices[4] = new Vector3(0f, height, 0f);

        // Base triangles
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        // Side triangles
        triangles[6] = 0;
        triangles[7] = 4;
        triangles[8] = 1;

        triangles[9] = 1;
        triangles[10] = 4;
        triangles[11] = 2;

        triangles[12] = 2;
        triangles[13] = 4;
        triangles[14] = 3;

        triangles[15] = 3;
        triangles[16] = 4;
        triangles[17] = 0;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        // Set material (you can assign your own material here)
        meshRenderer.material = new Material(Shader.Find("Standard"));

        // Attach the MeshFilter and MeshRenderer to the prefab
        meshFilter.sharedMesh = mesh;
        meshRenderer.sharedMaterial = meshRenderer.material;

        // Set the pyramid prefab's position, rotation, and scale as needed
        pyramidPrefab.transform.position = Vector3.zero;
        pyramidPrefab.transform.rotation = Quaternion.identity;
        pyramidPrefab.transform.localScale = Vector3.one;
    }
}

