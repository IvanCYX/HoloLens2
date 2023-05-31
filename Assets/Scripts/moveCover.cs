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
        position1 = new Vector3(-0.248f, -0.021f, -6.266f);
        position2 = new Vector3(-0.248f, -0.021f, -6.283f);
        position3 = new Vector3(-0.248f, -0.021f, -6.3f);
    }

    [PunRPC]
    public void FirstPositionRPC()
    {
        gameObject.transform.position = position1;
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
