using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using Photon.Pun;
using System;

public class ManageBeam : MonoBehaviour
{
    public GameObject refractingCell, speedSlider, scaleSlider, startButton, stopButton, freeSpace, beamLine, afterCell;
    private RefractingCell cell;
    private FreeSpace freeSpaceScript;
    private AfterCell afterCellScript;
    private LineRenderer beamLineScript;
    private float speedSliderVal, scaleSliderVal;
    private float[] status;
    private PhotonView pv;
    private bool cellPlaying, cylindersVisible, drawingBeam;
    public bool cellOnly;
    private List<GameObject> cylinders;

    // Start is called before the first frame update
    void Start()
    {
        cellOnly = true;
        status = new float[] { 0f, 0f};
        beamLineScript = beamLine.GetComponent<LineRenderer>();
        pv = GetComponentInParent<PhotonView>();
        cell = refractingCell.GetComponent<RefractingCell>();
        freeSpaceScript = freeSpace.GetComponent<FreeSpace>();
        afterCellScript = afterCell.GetComponent<AfterCell>();
        cylinders = new List<GameObject>();
        drawingBeam = true;
        cellPlaying = false;
        cylindersVisible = true;

        initializeCylList();
        initializeLine();       

    }

    // Update is called once per frame
    void Update()
    {
        if (drawingBeam)
        {
            updateBeam();
        }
        
    }

    public void startPulse()
    {

        if (pv != null)
        {
            speedSliderVal = speedSlider.GetComponent<ConstrainToLine>().scaledVal;

            if (speedSliderVal < 0.05f)
            {
                speedSliderVal = 0.05f;
            }

            pv.RPC("startPulseRPC", RpcTarget.AllBufferedViaServer, speedSliderVal /2f);

        }
        else
            Debug.LogError("PV is nll");

    }

    [PunRPC]
    private void startPulseRPC(float speed)
    {
        stopButton.SetActive(true);
        startButton.SetActive(false);
        play(speed);
    }

    private void play(float speed)
    {

        switch (status[0])
        {
            case 0f:
                if (cellOnly)
                {
                    status[0] = 1f;
                    cell.play(speed);                    
                }
                else
                {
                    freeSpaceScript.play(speed);
                }
                
                break;
            case 1f:
                cell.play(speed);
                break;
            case 2f:
                afterCellScript.play(speed);
                break;
        }
    }

    public void pause()
    {
        if (pv != null)
        {
            switch (status[0])
            {
                case 0f:
                    status[1] = freeSpaceScript.getPosition();
                    break;
                case 1f:
                    status[1] = (float)cell.index;
                    break;
                case 2f:
                    status[1] = afterCellScript.getPosition();
                    break;
            }
            pv.RPC("pauseRPC", RpcTarget.AllBufferedViaServer, status);
        }
        else
            Debug.LogError("PV is nll");
    }

    [PunRPC]
    private void pauseRPC(float[] position)
    {
        stopButton.SetActive(false);
        startButton.SetActive(true);
        status = position;
        Debug.Log(status[0]);
        Debug.Log(status[1]);

        switch (status[0])
        {
            case 0f:
                freeSpaceScript.pause(status[1]);

                cell.resetBeam();
                afterCellScript.resetBeam();
                break;
            case 1f:
                cell.pause((int)status[1]);

                freeSpaceScript.resetBeam();
                afterCellScript.resetBeam();
                break;
            case 2f:
                afterCellScript.pause(status[1]);
                
                freeSpaceScript.resetBeam();
                cell.resetBeam();
                break;
        }
    }

    public void transition() 
    {
        if (status[0]<1.9f)
        {
            status[0] += 1f;
        }
        else
        {
            if (cellOnly)
            {
                status[0] = 1f;
            }
            else
            {
                status[0] = 0;
            }
        }

        if (speedSliderVal < 0.05f)
        {
            speedSliderVal = 0.05f;
        }

        speedSliderVal = speedSlider.GetComponent<ConstrainToLine>().scaledVal;
        play(speedSliderVal/2f);
    }

    public void toggleCylinders()
    {
        pv.RPC("toggleCylindersRPC", RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    private void toggleCylindersRPC() 
    {
        cylindersVisible = !cylindersVisible;

        for (int i = 0; i < 12; i++)
        {
            GameObject cylinder = cylinders[i];
            cylinder.GetComponent<MeshRenderer>().enabled = cylindersVisible;
            cylinder.transform.GetChild(0).transform.gameObject.SetActive(cylindersVisible);
        }
        if(!cylindersVisible)
        {
            drawingBeam = false;
            beamLineScript.positionCount = 0;
            beamLineScript.SetPositions(new Vector3[0]);
        }
        else
        {
            drawingBeam = true;
        }
    }

    private void initializeCylList()
    {
        for (int i = 0; i < 10; i++)
        {
            cylinders.Add(freeSpace.transform.GetChild(i).transform.gameObject);
        }
        cylinders.Add(refractingCell.transform.GetChild(0).transform.gameObject);
        cylinders.Add(refractingCell.transform.GetChild(1).transform.gameObject);
        Debug.Log("cylinders: " + cylinders.Count.ToString());
        
    }

    private void initializeLine()
    {
        beamLineScript.startWidth = 0.01f;
        beamLineScript.endWidth = 0.01f;
    }

    private void updateBeam()
    {
        int cylCount = freeSpaceScript.cylinderCount;
        Vector3[] points = new Vector3[cylCount + 2];
        for (int i = 10 - cylCount; i < 12; i++)
        {
            points[i - 10 + cylCount] = cylinders[i].transform.position;
        }
        beamLineScript.positionCount = cylCount + 2;
        beamLineScript.SetPositions(points);
    }

    public void updateCellPosition()
    {
        pv.RPC("updateCellPositionRPC", RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    private void updateCellPositionRPC()
    {
        cell.updatePositions();
    }

    public void scalePulses()
    {
        scaleSliderVal = scaleSlider.GetComponent<ConstrainToLine>().scaledVal;
        float buffer = 0.05f;
        float scaler = (1-buffer) * scaleSliderVal + buffer;
        pv.RPC("scalePulsesRPC", RpcTarget.AllBufferedViaServer, scaler);
    }

    [PunRPC]
    private void scalePulsesRPC(float scaler)
    {
        cell.scalePulses(scaler);
        freeSpaceScript.scalePulse(scaler);
        afterCellScript.scalePulse(scaler);
    }
}
