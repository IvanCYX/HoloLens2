using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rotRight()
    {
        transform.Rotate(new Vector3(0f, 10f, 0f));
    }

    public void rotLeft()
    {
        transform.Rotate(new Vector3(0f, -10f, 0f));
    }
}
