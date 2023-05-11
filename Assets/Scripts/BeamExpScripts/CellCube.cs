using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using Photon.Pun;
using System;
public class CellCube : MonoBehaviour
{
    public GameObject startCyl, stopCyl;
    private float width, padding;

    // Start is called before the first frame update
    void Start()
    {
        width = 0.4f;
        padding = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        updateTransform();
    }

    public void updateTransform()
    {
        // set rotation
        Vector3 displacementVector = stopCyl.transform.localPosition - startCyl.transform.localPosition;
        gameObject.transform.localRotation = Quaternion.LookRotation(displacementVector);

        //set length
        float distance = Vector3.Magnitude(displacementVector);
        gameObject.transform.localScale = new Vector3(width, width, distance + padding);

        // set position
        Vector3 midPoint = (startCyl.transform.localPosition + stopCyl.transform.localPosition)/ 2f;
        gameObject.transform.localPosition = midPoint;

    }
}
