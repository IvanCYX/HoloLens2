using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Azure.SpatialAnchors;
using Microsoft.Azure.SpatialAnchors.Unity;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.XR;
using Photon.Pun;
using MRTK.Tutorials.MultiUserCapabilities;

public class Pulse : ScriptableObject
{
    private GameObject pulseInst;
    public GameObject gun;
    public Pulse(Vector3 position, Quaternion quat)
    {
        pulseInst = GameObject.Instantiate(Resources.Load("Pulse0") as GameObject, position, quat);
    }
    public Vector3 getPosition()
    {
        return pulseInst.transform.localPosition;
    }

    public void setPosition(Vector3 positionIn)
    {
        pulseInst.transform.localPosition = positionIn;
    }
}
