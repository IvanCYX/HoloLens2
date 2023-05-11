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

public class PulseBeam
{
    public GameObject pulseBeamObj;
    Vector3 startpos, stoppos, curpos, beamAxis;
    float speed;
    public GameObject[] pulses;
    GameObject pulse;
    MeshRenderer[] mrs;
    private int index, pulseCount;

    /*public int Curindex
    {
        get { return curindex; }
        set { 
            curindex = value;
            ShowCurrentPulse();
        }
    }*/
    public PulseBeam(Vector3 startpos, Vector3 stoppos, float speed)
    {
        pulseBeamObj = new GameObject();
        pulseBeamObj.name = "Beam Elements";
        this.speed = speed;
        this.startpos = startpos;
        this.stoppos = stoppos;
        this.curpos = startpos;
        pulseCount = 201;
        this.pulses = new GameObject[pulseCount];
        

        /*pulse = PhotonNetwork.Instantiate("WavePacket", startpos,
                Quaternion.identity);
        pulse.transform.parent = pulseBeamObj.transform;*/
        initialize();
        index = 0;

    }

   
    private void initialize() {
        beamAxis = startpos - stoppos;
        for (int i = 0; i < 201; i+=1)
        {
            curpos = Vector3.Lerp(startpos, stoppos, ((float)i / 200f));
            //pulse = PhotonNetwork.Instantiate("Pulse"+i.ToString(), curpos, Quaternion.LookRotation(beamAxis, Vector3.up));
            pulse = GameObject.Instantiate(Resources.Load("Pulse" + i.ToString()) as GameObject, curpos, Quaternion.LookRotation(beamAxis, Vector3.up), pulseBeamObj.transform);
            //pulse.transform.parent = pulseBeamObj.transform;
            pulse.transform.Rotate(new Vector3(0, 90, 0));

            mrs = pulse.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in mrs)
            {
                mr.enabled = false;
            }

            pulses[i] = pulse;
        }

        curpos = startpos;
    }

    public void nextPulse()
    {
        if (index == pulseCount)
        {
            togglePulse(pulses[pulseCount - 1], false);
            togglePulse(pulses[0], true);
            return;
        }
        pulse = pulses[index];
        togglePulse(pulse, true);

        if (index != 0)
        {
            pulse = pulses[index - 1];
            togglePulse(pulse, false);
        }

    }

    public void togglePulse(GameObject pulse, bool visible)
    {
        MeshRenderer[] mrs = pulse.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in mrs)
        {
            mr.enabled = visible;
        }
    }

}