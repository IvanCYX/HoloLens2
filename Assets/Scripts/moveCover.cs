using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCover : MonoBehaviour
{
    private Vector3 openPosition, closedPosition;
    // Start is called before the first frame update
    void Start()
    {

        //TODO FIND OPEN AND CLOSE POSITION
        openPosition = new Vector3(0f, -0.0041f, -0.0125f);
        closedPosition = new Vector3(0f, -0.0041f, 0.152f);
    }

    public void Open()
    {
        gameObject.transform.position = openPosition;
    }

    public void Close()
    {
        gameObject.transform.position = closedPosition;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
