using Photon.Pun;
using UnityEngine;

namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class PhotonUser : MonoBehaviour
    {
        private PhotonView pv;
        private string username;
        private UserAlert alertScript;

        private void Start()
        {
            pv = GetComponent<PhotonView>();

            if (!pv.IsMine) return;

            username = "User" + PhotonNetwork.NickName;
            pv.RPC("PunRPC_SetNickName", RpcTarget.AllBuffered, username);
            // Access alert and tell it to say "finding anchor" if anchor id is not null? maybe this won't work, not sure if I have to wait for the rpc to be called.
        }

        [PunRPC]
        private void PunRPC_SetNickName(string nName)
        {
            gameObject.name = nName;
        }

        [PunRPC]
        private void PunRPC_ShareAzureAnchorId(string anchorId)
        {
            GenericNetworkManager.Instance.azureAnchorId = anchorId;
            Debug.Log("SHARED ID" + anchorId);

            // notify user anchor shared
            GameObject alert = GameObject.Find("Alert");
            GameObject alertTextObject = alert.transform.Find("Text (TMP)").gameObject;
            alertScript = alertTextObject.GetComponent<UserAlert>();
            alertScript.displayMessage(anchorId);

            // deactivate main menu
            GameObject ui = GameObject.Find("UI");
            GameObject mainMenu = ui.transform.Find("FirstMenu").gameObject;
            mainMenu.SetActive(false);
                  

            // call the find anchor function
            GameObject anchorManager = GameObject.Find("Spatial Anchor");
            anchorManager.GetComponent<AnchorPlacementandInitialization>().findWrap();

            Debug.Log("\nPhotonUser.PunRPC_ShareAzureAnchorId()");
            Debug.Log("GenericNetworkManager.instance.azureAnchorId: " + GenericNetworkManager.Instance.azureAnchorId);
        }

        public void ShareAzureAnchorId()
        {
            if (pv != null)
                pv.RPC("PunRPC_ShareAzureAnchorId", RpcTarget.OthersBuffered,
                    GenericNetworkManager.Instance.azureAnchorId);
            else
                Debug.LogError("PV is null");
        }
        
    }
}
