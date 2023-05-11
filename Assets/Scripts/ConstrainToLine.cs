using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainToLine : MonoBehaviour
{
    private float xval, limit;
    public float scaledVal;
    private Vector3 prevPosition, curPosition;
    // Start is called before the first frame update
    void Start()
    {        
        limit = 0.15f;
        prevPosition = gameObject.transform.localPosition;
        updatePosition(prevPosition);
    }

    // Update is called once per frame
    void Update()
    {

        curPosition = gameObject.transform.localPosition;
        if (curPosition != prevPosition)
        {
            gameObject.transform.rotation = gameObject.transform.parent.transform.rotation;
            updatePosition(curPosition);
        }
    }

    private void updatePosition(Vector3 newPos)
    {
        if (newPos.x > limit)
        {
            xval = limit;
        }
        else if (newPos.x < -limit)
        {
            xval = -limit;
        }
        else
        {
            xval = newPos.x;
        }
        prevPosition = new Vector3(xval, 0f, 0f);
        gameObject.transform.localPosition = prevPosition;
        scaledVal = (xval + limit)/ (2*limit);
        //Debug.Log(scaledVal);
    }
}