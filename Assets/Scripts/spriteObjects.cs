using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class spriteObjects: MonoBehaviour
{
    private PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    [PunRPC]
    public void ShowModelRPC()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Debug.Log("Showing Image");
    }
    
    public void ShowModel()
    {
        pv.RPC("ShowModelRPC", RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    public void HideModelRPC()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log("Hide Image");
    }

    public void HideModel()
    {
        pv.RPC("HideModelRPC", RpcTarget.AllBufferedViaServer);
    }
}
