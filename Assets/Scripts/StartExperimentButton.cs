using Photon.Pun;
using UnityEngine;
public class StartExperimentButton : MonoBehaviour
{
    private PhotonView pv;
    private GameObject anchorMenu;

    private void Start()
    {
        pv = GetComponentInParent<PhotonView>();
    }
    public void startExperiment()
    {
        Vector3 position = Camera.main.transform.position;
        position += Vector3.Scale(Camera.main.transform.forward, new Vector3(0.7f, 0.7f, 0.7f));
        position += new Vector3(0f, -0.05f, 0f);
        PhotonNetwork.Instantiate("Blob", position, Quaternion.identity);
        pv.RPC("hideButtonRPC", RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    private void hideButtonRPC() {
        anchorMenu = GameObject.Find("UI");
        gameObject.SetActive(false);
        //anchorMenu.SetActive(false);
    }
}
