using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FreeSpace : MonoBehaviour
{
    private List<GameObject> mirrorCylinders;
    public GameObject beam, cellStart, pulseOrig;
    private GameObject pulse;
    private ManageBeam manageBeamScript;
    private PhotonView pv;
    public int cylinderCount, segment;
    private float speed, segmentLength;
    private bool playing;
    private Vector3 trajectory;

    // Start is called before the first frame update
    void Start()
    {
        segment = 0;
        cylinderCount = 0;
        playing = false;
        mirrorCylinders = new List<GameObject>();

        manageBeamScript = beam.GetComponent<ManageBeam>();
        pv = gameObject.GetComponent<PhotonView>();

        pulse = GameObject.Instantiate(pulseOrig, Vector3.zero, Quaternion.identity);

        foreach (Transform child in transform)
        {
            mirrorCylinders.Add(child.gameObject);
        }

        foreach (GameObject cylinder in mirrorCylinders)
        {
            cylinder.SetActive(false);
        }
        mirrorCylinders.Add(cellStart);
        resetBeam();
        addMirror();
        addMirror();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            Vector3 newPos = trajectory.normalized * Time.deltaTime * speed + pulse.transform.position;
            Vector3 newPosFromRecentMirror = newPos - mirrorCylinders[9 - segment].transform.position;
            float distance = newPosFromRecentMirror.sqrMagnitude;
            if (distance < segmentLength)
            {
                pulse.transform.position = newPos;
            }
            else
            {
                if (segment >= 1)
                {
                    segment -= 1;
                    updateTrajectory();
                }
                else
                {
                    resetBeam();
                    manageBeamScript.transition();
                }
            }
        }

    }

    public void resetBeam()
    {
        segment = cylinderCount - 1;
        if (segment<0)
        {
            segment = 0;
        }
        updateTrajectory();
        playing = false;
        togglePulse(pulse, false);
    }
    public void play(float speedIn)
    {
        playing = true;
        speed = speedIn;
        togglePulse(pulse, true);
    }

    public void addMirror()
    {
        if (cylinderCount <= 9)
        {
            pv.RPC("addMirrorRPC", RpcTarget.AllBufferedViaServer);
        }
    }

    [PunRPC]
    private void addMirrorRPC()
    {
        mirrorCylinders[9 - cylinderCount].SetActive(true);
        cylinderCount += 1;
        if (cylinderCount == 1)
        {
            /*pulse = GameObject.Instantiate(pulseOrig, mirrorCylinders[9].transform.position, Quaternion.identity);
            togglePulse(pulse, false);
            updateTrajectory();*/
        }
        segment = cylinderCount - 1;
        manageBeamScript.cellOnly = false;
        updateTrajectory();
    }

    public void removeMirror()
    {
        if (cylinderCount >= 1)
        {
            pv.RPC("removeMirrorRPC", RpcTarget.AllBufferedViaServer);
        }
    }

    [PunRPC]
    private void removeMirrorRPC()
    {
        mirrorCylinders[10 - cylinderCount].SetActive(false);
        cylinderCount -= 1;
        if (cylinderCount == 0)
        {
            togglePulse(pulse,false);
            segment = 0;
            manageBeamScript.cellOnly = true;
            if (playing)
            {
                manageBeamScript.transition();
            }
            playing = false;
            return;
        }
        segment = cylinderCount - 1;
        Debug.Log(segment);
        updateTrajectory();
    }

    private void updateTrajectory()
    {
        trajectory = mirrorCylinders[10 - segment].transform.position - mirrorCylinders[9 - segment].transform.position;
        segmentLength = trajectory.sqrMagnitude;
        pulse.transform.position = mirrorCylinders[9 - segment].transform.position;
        pulse.transform.rotation = Quaternion.LookRotation(trajectory, Vector3.up);
        pulse.transform.Rotate(new Vector3(0, -90, 0));
    }

    public void togglePulse(GameObject pulse, bool visible)
    {
        MeshRenderer[] mrs = pulse.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in mrs)
        {
            mr.enabled = visible;
        }
    }

    public void initializeSegments()
    {
        segment = cylinderCount - 1;
    }

    public float getPosition()
    {

        float totalDistance = Vector3.Distance(mirrorCylinders[10 - segment].transform.position, mirrorCylinders[9 - segment].transform.position);
        float pulseDistance = Vector3.Distance(mirrorCylinders[9 - segment].transform.position, pulse.transform.position);
        float codedPosition = (float)segment + (float)(pulseDistance / totalDistance);
        return codedPosition;
    }

    public void setPosition(float codedPosition)
    {
        segment = 0;
        while (codedPosition > 1)
        {
            segment += 1;
            codedPosition -= 1;
        }
        updateTrajectory();
        pulse.transform.position = Vector3.Lerp(mirrorCylinders[9 - segment].transform.position, mirrorCylinders[10 - segment].transform.position, (float)codedPosition);
    }

    public void pause(float codedPosition)
    {
        playing = false;
        togglePulse(pulse, true);
        setPosition(codedPosition);
    }

    public void scalePulse(float value)
    {
        pulse.transform.localScale = new Vector3(value, value, value);
    }

}
