using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron
{
    private GameObject electronInstance;
    private ElectronMotion electronMotionScript;
    public Vector3 direction;
    public float speed;
    public Vector3 trajectory;

    //Constructor
    public Electron(Vector3 electronGunTip, Vector3 trajectory)
    {

        electronInstance = GameObject.Instantiate(Resources.Load("electronPrefab") as GameObject, electronGunTip, Quaternion.identity);
        electronMotionScript = electronInstance.GetComponent<ElectronMotion>();
        electronMotionScript.electronGunTip = electronGunTip;
    }

    public Vector3 GetPosition()
    {
        return electronInstance.transform.localPosition;
    }

    public void SetSpeed(float speedIn)
    {
        electronMotionScript.speed = speedIn;
    }

/*    public Vector3 CalculateTrajectory(Vector3 phi, Vector3 theta) //greatest angle between slit and screen
    {
        //complicated vector math = trajectory
        direction = electronMotionScript.trajectory;
        return direction;
    }*/

    public void Fire(Vector3 trajectory)
    {
        //electronMotionScript.Fire(speed, trajectory);
    }

}