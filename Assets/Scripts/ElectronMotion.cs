using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronMotion : MonoBehaviour
{
    public Vector3 trajectory, electronGunTip;
    public float speed;
    private bool moving;
    private Electron electron;
    private GameObject electronInstance;
    private ElectronMotion electronMotionScript;
    private ElectronGun gun;
    public Vector3[] trajectories;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        speed = 4f;
        trajectory = Vector3.left;
        electronGunTip = new Vector3(1.5f, 0.5006f, 1.996f);
        gameObject.tag = "Electron";
    }

    void Update()
    {
        if (moving)
        {
            gameObject.transform.position = gameObject.transform.position + speed * Time.deltaTime * trajectory;
        }
    }

    public void toggleVis(bool isVis)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = moving;
    }

    public void Fire(float speed, Vector3 trajectory)
    {
        //TODO Add speed and trajectory input
        moving = true;
        toggleVis(true);
    }

    public void Reset()
    {
        moving = false;
        gameObject.transform.position = electronGunTip;
        toggleVis(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Electron"))
        {
            electronInstance.transform.position = electronMotionScript.electronGunTip;
        }
    }
}
