using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCover : MonoBehaviour
{
    private Vector3 position1, position2, position3;
    // Start is called before the first frame update
    void Start()
    {

        //TODO FIND OPEN AND CLOSE POSITION
        position1 = new Vector3(-0.248f, -0.021f, -6.266f);
        position2 = new Vector3(-0.248f, -0.021f, -6.283f);
        position3 = new Vector3(-0.248f, -0.021f, -6.3f);
    }

    public void FirstPosition()
    {
        gameObject.transform.position = position1;
    }

    public void SecondPosition()
    {
        gameObject.transform.position = position2;
    }
    
    public void ThirdPosition()
    {
        gameObject.transform.position = position3;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
