using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MoveCover : MonoBehaviour
{
    private Vector3 position1, position2, position3;
    private PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        position1 = gameObject.transform.localPosition + new Vector3(0f, 0f, 0.03f);
        position2 = gameObject.transform.localPosition + new Vector3(0f, 0f, 0f);
        position3 = gameObject.transform.localPosition + new Vector3(-0.065f, -0.004f, 0.86f);
    }

    [PunRPC]
    public void FirstPositionRPC()
    {
        gameObject.transform.localPosition = position1;
    }

    public void FirstPosition()
    {
        pv.RPC("FirstPositionRPC", RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    public void SecondPositionRPC()
    {
        gameObject.transform.position = position2;
    }

    public void SecondPosition()
    {
        pv.RPC("SecondPositionRPC", RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    public void ThirdPositionRPC()
    {
        gameObject.transform.position = position3;
    }

    public void ThirdPosition()
    {
        pv.RPC("ThirdPositionRPC", RpcTarget.AllBufferedViaServer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
