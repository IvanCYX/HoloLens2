using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using Photon.Pun;
using System;

public class BeamManager : MonoBehaviour
{
    GameObject StartCyl, StopCyl, beamElements;
    PulseBeam pulsebeam;
    public GameObject slider;
    private PinchSlider pinchSlider;
    private GameObject[] pulses;
    private GameObject pulse, pauseButton, playButton;
    private PhotonView pv;
    private bool playing, loaded;
    private float timeLeft, timeCount, distance, sliderVal;
    private int pulsesLength, prevIndex, index;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponentInParent<PhotonView>();
        pinchSlider = slider.GetComponent<PinchSlider>();
        StartCyl = gameObject.transform.parent.GetChild(0).gameObject;
        StopCyl = gameObject.transform.parent.GetChild(1).gameObject;
        pauseButton = gameObject.transform.parent.GetChild(4).gameObject;
        playButton = gameObject.transform.parent.GetChild(3).gameObject;
        pulsesLength = 201;
        prevIndex = 0;
        index = 0;
        

        playing = false;
        loaded = false;
    }

    private void Update()
    {
        
        if (playing) {
            
            double i = timeCount / timeLeft;
            

            i *= pulsesLength;
            i = Math.Round(i);

            index = (int)i;
            nextPulse();
            timeCount += Time.deltaTime;            
        }        
    }

    public void nextPulse()
    {

        if (prevIndex == index)
        {
            return;
        }

        togglePulse(pulses[prevIndex], false);

        if (index >= pulsesLength)
        {
            togglePulse(pulses[0], true);
            timeCount = 0f;
            index = 0;
            prevIndex = 0;
            return;
        }

        togglePulse(pulses[index], true);
        prevIndex = index;
        
    }


    public void startPulse()
    {
        if (!loaded)
        {
            loadPulse();
        }

        if (pv != null)
        {
            sliderVal = pinchSlider.SliderValue;

            if (sliderVal < 0.05f)
            {
                sliderVal = 0.05f;
            }

            pv.RPC("startPulseRPC", RpcTarget.AllBufferedViaServer, sliderVal/2.5f);
        }
        else
            Debug.LogError("PV is nll");

    }

    [PunRPC]
    public void startPulseRPC(float speed)
    {
        
        if (pulses.Length == 0)
        {
            beamElements = gameObject.transform.GetChild(0).gameObject;
            for (int i = 0; i < pulsesLength; i++)
            {
                pulses[i] = beamElements.transform.GetChild(i).gameObject;
            }
        }
        distance = Vector3.Distance(StartCyl.transform.position, StopCyl.transform.position);
        timeLeft = (distance / speed);
        timeCount = ((float)index/(float)pulsesLength) * timeLeft;
        playing = true;
        pauseButton.SetActive(true);
        playButton.SetActive(false);

    }

    public void pause()
    {
        if (pv != null)
            pv.RPC("pauseRPC", RpcTarget.AllBufferedViaServer, prevIndex);
        else
            Debug.LogError("PV is nll");
    }

    [PunRPC]
    public void pauseRPC(int stoppoint)
    {
        playing = false;
        index = stoppoint;
        prevIndex = stoppoint - 1;
        pauseButton.SetActive(false);
        playButton.SetActive(true);

        for (int i = 0; i<pulsesLength; i++)
        {
            pulse = pulses[i];
            if (i != stoppoint)
            {
                togglePulse(pulse, false);
            }
            else 
            {
                togglePulse(pulse, true);
            }
        }
    }

    public void loadPulse() {
        
        
        if (pv != null)
            pv.RPC("loadRPC", RpcTarget.AllBuffered);
        else
            Debug.LogError("PV is nll");

    }

    [PunRPC]
    private void loadRPC()    {

        pulsebeam = new PulseBeam(StartCyl.transform.position, StopCyl.transform.position, 2);
        //pulsebeam = new PulseBeam(Vector3.zero, Vector3.one, 2);
        pulsebeam.pulseBeamObj.transform.parent = gameObject.transform;
        pulses = pulsebeam.pulses;
        loaded = true;
    }
    

    public void togglePulse(GameObject pulse, bool visible)
    {
        MeshRenderer[] mrs = pulse.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in mrs)
        {
            mr.enabled = visible;
        }
    }
    
    public void updatePositions()
    {
        if (pv != null)
            pv.RPC("updatePositionsRPC", RpcTarget.AllBufferedViaServer);
        else
            Debug.LogError("PV is nll");
    }

    [PunRPC]
    public void updatePositionsRPC()
    {
        startPulse();
        Vector3 beamAxis = StartCyl.transform.position - StopCyl.transform.position;
        Quaternion lookAxis = Quaternion.LookRotation(beamAxis, Vector3.up);
        for (int i = 0; i < pulsesLength; i++)
        {
            Vector3 curpos = Vector3.Lerp(StartCyl.transform.position, StopCyl.transform.position, (float)i / ((float)pulsesLength - 1f));            
            pulse = pulses[i];
            pulse.transform.localPosition = curpos;
            pulse.transform.rotation = lookAxis;
            pulse.transform.Rotate(new Vector3(0, 90, 0));
            startPulse();

        }
    }
    
}
