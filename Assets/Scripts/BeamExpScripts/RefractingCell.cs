using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using Photon.Pun;
using System;
public class RefractingCell : MonoBehaviour
{
    public GameObject startCyl;
    public GameObject stopCyl;
    public GameObject Beam;
    private ManageBeam beamScript;
    private PhotonView pv;
    public int pulseCount, prevIndex, index;
    private float timeLeft, timeCount, distance;
    public float speed;
    private Vector3 trajectory;
    private GameObject[] pulses;
    public bool playing;

    // Start is called before the first frame update
    void Start()
    {
        pv = gameObject.GetComponent<PhotonView>();
        pulseCount = 201;
        prevIndex = 0;
        index = 0;
        speed = 1f;
        playing = false;
        pulses = new GameObject[pulseCount];
        beamScript = Beam.GetComponent<ManageBeam>();

        loadPulses();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)

        {
            double i = timeCount / timeLeft;

            i *= pulseCount;
            i = Math.Round(i);

            index = (int)i;
            nextPulse();
            timeCount += Time.deltaTime;
        }
    }

    public void play(float speed)
    {        
        distance = Vector3.Distance(startCyl.transform.position, stopCyl.transform.position);
        timeLeft = (distance / speed);
        this.speed = speed;
        timeCount = ((float)index / (float)pulseCount) * timeLeft;
        playing = true;
    }

    public void nextPulse()
    {

        if (prevIndex == index)
        {
            return;
        }
        

        if (index >= pulseCount)
        {
            resetBeam();
            beamScript.transition();
            /*foreach(GameObject pulseobj in pulses)
            {
                togglePulse(pulseobj, false);
            }*/
            return;
        }
        togglePulse(pulses[prevIndex], false);
        togglePulse(pulses[index], true);
        prevIndex = index;
    }

    public void resetBeam()
    {
        togglePulse(pulses[prevIndex], false);
        timeCount = 0f;
        index = 0;
        prevIndex = 0;
        playing = false;
    }

    private void loadPulses()
    {
        Vector3 curpos;
        GameObject pulse;
        Vector3 trajectory = startCyl.transform.position - stopCyl.transform.position;

        for (int i = 0; i < 201; i += 1)
        {
            curpos = Vector3.Lerp(startCyl.transform.position, stopCyl.transform.position, ((float)i / 200f));
            //pulse = PhotonNetwork.Instantiate("Pulse"+i.ToString(), curpos, Quaternion.LookRotation(beamAxis, Vector3.up));
            pulse = GameObject.Instantiate(Resources.Load("Pulse" + i.ToString()) as GameObject, curpos, Quaternion.LookRotation(trajectory, Vector3.up), gameObject.transform);
            //pulse.transform.parent = pulseBeamObj.transform;
            pulse.transform.Rotate(new Vector3(0, 90, 0));
            togglePulse(pulse, false);
            pulses[i] = pulse;
        }
        distance = Vector3.Distance(startCyl.transform.position, stopCyl.transform.position);
        timeLeft = (distance / speed);
        timeCount = ((float)index / (float)pulseCount) * timeLeft;

    }

    private void togglePulse(GameObject pulse, bool visible)
    {
        MeshRenderer[] mrs = pulse.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in mrs)
        {
            mr.enabled = visible;
        }
    }

    public void updatePositions()
    {
        pv.RPC("updatePositionsRPC", RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    private void updatePositionsRPC()
    {

        Vector3 beamAxis = startCyl.transform.position - stopCyl.transform.position;
        Quaternion lookAxis = Quaternion.LookRotation(beamAxis, Vector3.up);
        for (int i = 0; i < pulseCount; i++)
        {
            Vector3 curpos = Vector3.Lerp(startCyl.transform.localPosition, stopCyl.transform.localPosition, (float)i / ((float)pulseCount - 1f));
            //Vector3 curpos = startCyl.transform.localPosition;
            GameObject pulse = pulses[i];
            pulse.transform.localPosition = curpos;
            pulse.transform.rotation = lookAxis;
            pulse.transform.Rotate(new Vector3(0, 90, 0));

        }
        distance = Vector3.Distance(startCyl.transform.localPosition, stopCyl.transform.localPosition);
        timeLeft = (distance / speed);
        timeCount = ((float)index / (float)pulseCount) * timeLeft;
    }

    public void pause(float position)
    {
        index = (int)position;
        distance = Vector3.Distance(startCyl.transform.position, stopCyl.transform.position);
        timeLeft = (distance / speed);
        timeCount = ((float)index / (float)pulseCount) * timeLeft;
        playing = false;

        if (index == 0)
        {
            prevIndex = 0;
        }
        else
        {
            prevIndex = index - 1;
        }

        for (int i = 0; i < pulseCount; i++)
        {
            GameObject pulse = pulses[i];
            if (i != prevIndex) 
            {
                togglePulse(pulse, false);
            }
            else
            {
                togglePulse(pulse, true);
            }
        }
    }

    public void scalePulses(float value)
    {
        foreach (GameObject pulse in pulses)
        {
            pulse.transform.localScale = new Vector3(value, value, value);
        }
    }

}
