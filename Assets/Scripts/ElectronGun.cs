using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ElectronGun : MonoBehaviour
{
    private PhotonView pv;
    private float electronConstant;
    public float speed;
    public int fireRate;
    private Vector3 electronGunTip, direction, offsetVector;
    public Electron[] createdElectrons;
    private int electronIndex, electronCount, electronsInAir, trajectoryIndex;
    private bool gunOn;
    public GameObject startGunButton, stopGunButton, fireGunButton;
    public Vector3[] trajectories;
    TextAsset jsonFile;

    // Start is called before the first frame update
    void Start()
    {
        pv = gameObject.GetComponent<PhotonView>();
        speed = 4f;
        offsetVector = new Vector3(0.041f, 0.024f, -0.032f);
        electronGunTip = gameObject.transform.position + offsetVector;
        direction = Vector3.left;
        electronIndex = 0;
        trajectoryIndex = 0;
        electronCount = 20;
        gunOn = false;
        fireGunButton.SetActive(false);
        stopGunButton.SetActive(false);
        //InitializeElectrons();

        //Get trajectories from JSON
        List<Vector3> vs = new List<Vector3>();
        jsonFile = Resources.Load(string.Format("vectors")) as TextAsset;
        Trajectories trajectoriesInJson = JsonUtility.FromJson<Trajectories>(jsonFile.text);
        foreach (Trajectory trajectory in trajectoriesInJson.trajectories)
        {
            vs.Add(new Vector3(trajectory.x, trajectory.y, trajectory.z));
        }
        trajectories = vs.ToArray();
    }

    void Update()
    {

    }

    /*public void InitializeElectrons()
    {
        for (int i = 0; i < electronCount; ++i)
        {
            createdElectrons[i] = new Electron(electronGunTip, trajectories[trajectoryIndex]);
        }

        GameObject.Instantiate(Resources.Load("electronPrefab") as GameObject, electronGunTip, Quaternion.identity);
    }*/

    //Fire initialized electrons
    public void Fire()
    {
        createdElectrons[electronIndex].Fire(trajectories[trajectoryIndex]);
        trajectoryIndex += 1;
        electronIndex += 1;
    }
   
    //Coroutine to call fire function
    IEnumerator Firing()
    {
        while(gunOn == true)
        {
            Fire();
            yield return new WaitForSeconds(.1f);
        }
    }

    private void IncrementIndices()
    {
        if(electronIndex == electronCount - 1)
        {
            electronIndex = 0;
        } else
        {
            electronIndex += 1;
        }
    }

    public void StartGun()
    {
        pv.RPC("StartGunRPC", RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    private void StartGunRPC()
    {
        gunOn = true;
        //InitializeElectrons();
        StartCoroutine(Firing());
        fireGunButton.SetActive(true);
        startGunButton.SetActive(false);
    }

    public void StopGun()
    {
        pv.RPC("StopGunRPC", RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    private void StopGunRPC()
    {
        gunOn = false;
        StopCoroutine(Firing());
        stopGunButton.SetActive(false);
    }

    [PunRPC]
    private void SetElectronSpeeds()
    {
        //float speed = sliderObjectScript.value * factor
        //fireRate = speed * electronsInAir;
        foreach (Electron electron in createdElectrons)
        {
            //set speed of electron
        }
    }

}
