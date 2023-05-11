using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class pvTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = Camera.main.transform.position;
        position += Vector3.Scale(Camera.main.transform.forward, new Vector3(0.4f, 0.4f, 0.4f));
        position += new Vector3(0f, -0.05f, 0f);
        PhotonNetwork.Instantiate("SphereObj", position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
