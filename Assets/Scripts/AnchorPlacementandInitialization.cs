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
[RequireComponent(typeof(SpatialAnchorManager))]

public class AnchorPlacementandInitialization : MonoBehaviour

{
    [SerializeField] private GameObject preFab;
    [SerializeField] private GameObject alertObject;
    private SpatialAnchorManager _spatialAnchorManager = null;
    private GameObject anchorPlane;
    private UserAlert alertScript;

    // Start is called before the first frame update
    void Start()
    {
        _spatialAnchorManager = GetComponent<SpatialAnchorManager>();
        _spatialAnchorManager.LogDebug += (sender, args) => Debug.Log($"ASA - Debug: {args.Message}");
        _spatialAnchorManager.Error += (sender, args) => Debug.LogError($"ASA - Error: {args.ErrorMessage}");
        _spatialAnchorManager.AnchorLocated += SpatialAnchorManager_AnchorLocated;
        anchorPlane = transform.Find("InclinedPlane").gameObject;
        alertScript = alertObject.GetComponent<UserAlert>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void drawPlane(Vector3 position)
    {
        anchorPlane.transform.position = position;
        anchorPlane.SetActive(true);
    }

    public void drawPlaneHere()
    {
        Debug.Log(GenericNetworkManager.Instance.azureAnchorId);
        if (GenericNetworkManager.Instance.azureAnchorId != "")
        {
            alertScript.displayMessage("Anchor Already Set!");
            return;
        }
            
        Vector3 position = Camera.main.transform.position;
        position += Vector3.Scale(Camera.main.transform.forward, new Vector3(1.2f, 1.2f, 0f));
        position += new Vector3(0f, -0.05f, 0f);
        drawPlane(position);
    }


    public void removePlane()
    {
       anchorPlane.SetActive(false);
    }


    public async void createWrap()
    {
        if (GenericNetworkManager.Instance.azureAnchorId == "")
        {
            await _spatialAnchorManager.StartSessionAsync();
            await CreateAnchor();
        }
        else
        {
            alertScript.displayMessage("Anchor already set!");
        }
    }

    private async Task CreateAnchor()
    {

        CloudNativeAnchor cloudNativeAnchor = anchorPlane.AddComponent<CloudNativeAnchor>();
        await cloudNativeAnchor.NativeToCloud();
        CloudSpatialAnchor cloudSpatialAnchor = cloudNativeAnchor.CloudAnchor;
        cloudSpatialAnchor.Expiration = DateTimeOffset.Now.AddDays(1);

        //Collect Environment Data
        while (!_spatialAnchorManager.IsReadyForCreate)
        {
            float createProgress = _spatialAnchorManager.SessionStatus.RecommendedForCreateProgress;
            Debug.Log($"ASA - Move your device to capture more environment data: {createProgress:0%}");
        }

        Debug.Log($"ASA - Saving cloud anchor... ");

        try
        {
            // Now that the cloud spatial anchor has been prepared, we can try the actual save here.
            await _spatialAnchorManager.CreateAnchorAsync(cloudSpatialAnchor);

            bool saveSucceeded = cloudSpatialAnchor != null;
            if (!saveSucceeded)
            {
                anchorPlane.GetComponent<MeshRenderer>().material.color = Color.red;
                alertScript.displayMessage("ASA - Failed to save");

                return;
            }

            alertScript.displayMessage($"Anchor created and shared");
            anchorPlane.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        catch (Exception exception)
        {
            alertScript.displayMessage("ASA - Failed to save anchor: " + exception.ToString());
            GenericNetworkManager.Instance.azureAnchorId = exception.ToString();

        }
        // use cloudSPatialAnchor.Identifier and pun rpc to share to other players
        //"User" + PhotonNetwork.NickName
        GenericNetworkManager.Instance.azureAnchorId = cloudSpatialAnchor.Identifier;
        GameObject user = GameObject.Find("User" + PhotonNetwork.NickName);
        PhotonUser other = (PhotonUser)user.GetComponent(typeof(PhotonUser));        

        PhotonNetwork.Instantiate(preFab.name, anchorPlane.transform.position,
                anchorPlane.transform.rotation);
        other.ShareAzureAnchorId();
        anchorPlane.SetActive(false);
  
    }
    // </CreateAnchor>

    public async void findWrap()
    {
        if (GenericNetworkManager.Instance.azureAnchorId == "")
        {
            alertScript.displayMessage("No anchor shared.");
            return;
        }
        alertScript.displayMessage("Look around for anchor klootzak");

        await _spatialAnchorManager.StartSessionAsync();
        LocateAnchor();
    }

    private void LocateAnchor()
    {

        //Create watcher to look for all stored anchor IDs
        Debug.Log($"ASA - Creating watcher to look for spatial anchors");
        AnchorLocateCriteria anchorLocateCriteria = new AnchorLocateCriteria();

        anchorLocateCriteria.Identifiers = new List<string> { GenericNetworkManager.Instance.azureAnchorId }.ToArray();
        _spatialAnchorManager.Session.CreateWatcher(anchorLocateCriteria);
        Debug.Log($"ASA - Watcher created!");

    }

    public void displayID()
    {
        Debug.Log("AnchorID: " + GenericNetworkManager.Instance.azureAnchorId);
    }

   
    private void SpatialAnchorManager_AnchorLocated(object sender, AnchorLocatedEventArgs args)
    {
        Debug.Log($"ASA - Anchor recognized as a possible anchor {args.Identifier} {args.Status}");

        if (args.Status == LocateAnchorStatus.Located)
        {
            //Creating and adjusting GameObjects have to run on the main thread. We are using the UnityDispatcher to make sure this happens.
            UnityDispatcher.InvokeOnAppThread(() =>
            {
                // Read out Cloud Anchor values
                CloudSpatialAnchor cloudSpatialAnchor = args.Anchor;

                //Create GameObject
                anchorPlane.SetActive(true);
                anchorPlane.GetComponent<MeshRenderer>().material.color = Color.green;

                // Link to Cloud Anchor
                anchorPlane.AddComponent<CloudNativeAnchor>().CloudToNative(cloudSpatialAnchor);
                GameObject.Find(preFab.name + "(Clone)").transform.position = anchorPlane.transform.position;
                GameObject.Find(preFab.name + "(Clone)").transform.rotation = anchorPlane.transform.rotation;
                anchorPlane.SetActive(false);
            });
        }
    }
}