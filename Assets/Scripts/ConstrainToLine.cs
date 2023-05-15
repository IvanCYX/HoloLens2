using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainToLine : MonoBehaviour
{
    private float zval, limit, backLimit;
    public float scaledVal;
    private Vector3 prevPosition, curPosition;
    // Start is called before the first frame update
    void Start()
    {        
        limit = 0.152f;
        backLimit = -0.03f;
        prevPosition = gameObject.transform.localPosition;
        updatePosition(prevPosition);
    }

    // Update is called once per frame
    void Update()
    {

        curPosition = gameObject.transform.localPosition;
        if (curPosition != prevPosition)
        {
            //gameObject.transform.rotation = gameObject.transform.parent.transform.rotation;
            updatePosition(curPosition);
        }
    }

    private void updatePosition(Vector3 newPos)
    {
        if (newPos.z > limit)
        {
            zval = limit;
        }
        else if (newPos.z < backLimit)
        {
            zval = backLimit;
        }
        else
        {
            zval = newPos.z;
        }
        prevPosition = new Vector3(0f, -0.004f, zval);
        gameObject.transform.localPosition = prevPosition;
        scaledVal = (zval + limit)/ (2*limit);
        Debug.Log(curPosition);
    }
}