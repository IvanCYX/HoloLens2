using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuToUser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = Camera.main.transform.position;
        position += Vector3.Scale(Camera.main.transform.forward, new Vector3(0.2f, 0.2f, 0.2f));
        position += new Vector3(0f, -0.05f, 0f);
        gameObject.transform.position = position;
        gameObject.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
