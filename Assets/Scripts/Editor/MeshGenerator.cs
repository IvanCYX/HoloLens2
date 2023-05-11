using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Photon.Pun;

[RequireComponent(typeof(MeshFilter))]

public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] verts;
    int[] triangles;
    TextAsset jsonFile;
    Color[] colors;
    public Material material;
    public GameObject pulseObjectExample;


    void Start()
    {
        for(int i = 0; i<201; i+= 1)
        {
            jsonFile = Resources.Load(string.Format("surface{0}", i)) as TextAsset;
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;

            CreateShape(i);
            UpdateMesh();
            AssetDatabase.CreateAsset(mesh, string.Format("Assets/Pulse{0}.mesh", i));
            GameObject pulseQuarter = new GameObject();
            MeshFilter filter = new MeshFilter();
            filter = pulseQuarter.AddComponent<MeshFilter>();
            MeshRenderer renderer = new MeshRenderer();
            renderer = pulseQuarter.AddComponent<MeshRenderer>();
            filter.mesh = AssetDatabase.LoadAssetAtPath(string.Format("Assets/Pulse{0}.mesh", i), typeof(Mesh)) as Mesh;
            renderer.material = material;
            renderer.allowOcclusionWhenDynamic = false;
            pulseQuarter.name = "PulseQuarter" + i.ToString();
            pulseQuarter.transform.localScale = gameObject.transform.localScale;
            pulseQuarter.transform.position = gameObject.transform.position;
            pulseQuarter.transform.rotation = gameObject.transform.rotation;
            
            GameObject pulse = new GameObject();
            pulse.name = "Pulse" + i.ToString();
            for (int j = 0; j < 4; j++)
            {
                GameObject pqcopy;
                pqcopy = Instantiate(pulseQuarter);
                pqcopy.name = "PulseQuarter" + j.ToString();
                pqcopy.transform.localScale = pulseObjectExample.transform.GetChild(j).gameObject.transform.localScale;
                pqcopy.transform.position = pulseObjectExample.transform.GetChild(j).gameObject.transform.position;
                pqcopy.transform.rotation = pulseObjectExample.transform.GetChild(j).gameObject.transform.rotation;
                pqcopy.transform.parent = pulse.transform;

            }
            PrefabUtility.SaveAsPrefabAsset(pulse, string.Format("Assets/Resources/TestPulses/Pulse{0}.prefab", i));
            Destroy(pulse);
            Destroy(pulseQuarter);

        }
        
    }

    void CreateShape(int pulseNum)
    {
        
        List<int> tris;

        tris = JsonUtility.FromJson<TriIndexes>(jsonFile.text).triindexes;
        
        triangles = tris.ToArray();


        List<Vector3> vs = new List<Vector3>();
        Vertices verticesInJson = JsonUtility.FromJson<Vertices>(jsonFile.text);
        // Debug.Log(verticesInJson.vertices);
        foreach (Vertex vertex in verticesInJson.vertices)
        {
            //Debug.Log(vertex.x);
            vs.Add(new Vector3(vertex.x/500, vertex.y/7500, vertex.z/500));
        }
        verts = vs.ToArray();
        colors= new Color[verts.Length];
        for (int i = 0; i< verts.Length; i++)
        {
            float x = verts[i].x;
            float scaledX = (x) / 3f + 0.5f;
            if (pulseNum<100)
            {
                scaledX = Mathf.Lerp(0f, 0.8f, scaledX);
            }
            else
            {
                scaledX = Mathf.Lerp(0.8f, 0f, scaledX);
            }
            
            
            colors[i].r = Color.HSVToRGB(scaledX, Mathf.Abs((float)pulseNum-100f)/100f, 1f- Mathf.Abs((float)pulseNum-100)/200).r;
            //colors[i].r = 0f;
            colors[i].g = Color.HSVToRGB(scaledX, Mathf.Abs((float)pulseNum-100f)/100f, 1f- Mathf.Abs((float)pulseNum-100)/200).g;
            colors[i].b = Color.HSVToRGB(scaledX, Mathf.Abs((float)pulseNum-100f)/100f, 1f- Mathf.Abs((float)pulseNum-100)/200).b;
            colors[i].a = 0.5f;
        }

        
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();
    }

    
}
