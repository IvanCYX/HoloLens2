using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AfterCell : MonoBehaviour
{

    public GameObject beam, cellStart, cellStop, pulseOrig;
    private GameObject pulse;
    private ManageBeam manageBeamScript;
    private PhotonView pv;
    private float speed, distanceTraveled, totDistance;
    private bool playing;
    private Vector3 trajectory;

    void Start()
    {
        distanceTraveled = 0f;
        totDistance = 0.7f;
        playing = false;
        manageBeamScript = beam.GetComponent<ManageBeam>();
        pv = gameObject.GetComponent<PhotonView>();
        pulse = GameObject.Instantiate(pulseOrig, Vector3.zero, Quaternion.identity);
        togglePulse(pulse, false);
        setTrajectory();
        resetBeam();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            if (distanceTraveled < totDistance)
            {
                advancePulse();
            }
            else
            {
                resetBeam();
                manageBeamScript.transition();
                playing = false;
            }
        }
    }

    private void togglePulse(GameObject pulse, bool visible)
    {
        MeshRenderer[] mrs = pulse.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in mrs)
        {
            mr.enabled = visible;
        }
    }

    private void setTrajectory()
    {
        trajectory = cellStop.transform.position - cellStart.transform.position;
        trajectory = trajectory.normalized;
        pulse.transform.rotation = Quaternion.LookRotation(trajectory, Vector3.up);
        pulse.transform.Rotate(new Vector3(0, -90, 0));
    }

    public void setPosition(float position)
    {        
        pulse.transform.position = cellStop.transform.position + trajectory * position*totDistance;
        distanceTraveled = totDistance * position;
    }

    public float getPosition()
    {
        float codedPosition = distanceTraveled / totDistance;
        return codedPosition;
    }

    private void advancePulse()
    {
        float frameDistance = Time.deltaTime * speed;
        pulse.transform.position += frameDistance * trajectory;
        distanceTraveled += frameDistance;
    }

    public void play(float speedIn)
    {
        playing = true;
        speed = speedIn;
        togglePulse(pulse, true);
        setTrajectory();
    }

    public void pause(float position)
    {
        setPosition(position);
        togglePulse(pulse, true);
        playing = false;
    }

    public void resetBeam()
    {
        setPosition(0f);
        togglePulse(pulse, false);
        playing = false;
    }
    public void scalePulse(float value)
    {
        pulse.transform.localScale = new Vector3(value, value, value);
    }


}
