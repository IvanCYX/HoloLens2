using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getLength : MonoBehaviour
{

    public GameObject doubleSlit, plane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetWidth()
    {
        float width = gameObject.GetComponent<Renderer>().bounds.size.z;
        Debug.Log(width);
        return width;
    }

    public float GetHeight()
    {
        float height = gameObject.GetComponent<Renderer>().bounds.size.y;
        Debug.Log(height);
        return height;
    }

    
}
