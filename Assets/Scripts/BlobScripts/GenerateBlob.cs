using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBlob : MonoBehaviour
{
    private int diskCount;
    private float blobLength, blobWidth, scale;
    // Start is called before the first frame update
    void Start()
    {
        blobLength = 0.1f;
        blobWidth = 0.15f;
        diskCount = 16;
        scale = blobLength / (float)diskCount;
        createBlob();

    }

    private void createBlob()
    {
        for(int i = 0; i<diskCount; i++)
        {
            Vector3 offset = new Vector3(gameObject.transform.position.x + 2f * scale * (float)i, gameObject.transform.position.y, gameObject.transform.position.z);
            GameObject blob;
            if (i < diskCount / 2)
            {
                blob = GameObject.Instantiate(Resources.Load("RedCylinder") as GameObject, offset, Quaternion.identity, gameObject.transform);
            }
            else
            {
                blob = GameObject.Instantiate(Resources.Load("BlueCylinder") as GameObject, offset, Quaternion.identity, gameObject.transform);
            }
            blob.transform.Rotate(new Vector3(0f, 0f, 90f));
            blob.transform.localScale = new Vector3(diskRadius(scale * (i+.5f)), scale, diskRadius(scale * (i+.5f)));
        }
    }

    private float diskRadius(float distance)
    {
        float radius = blobWidth * Mathf.Exp(-  Mathf.Pow(distance-blobLength/2f, 2f)/Mathf.Pow(blobLength/3f, 2f)    );
        Debug.Log(Mathf.Pow(distance - blobLength / 2f, 2f) / Mathf.Pow(blobLength / 4f, 2f));
        return radius;
    }
}
