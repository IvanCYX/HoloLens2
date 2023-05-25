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
        position1 = new Vector3(0.2569284f, -0.1517174f, -5.502f);
        position2 = new Vector3(0.2569284f, -0.1517174f, -5.51775f);
        position3 = new Vector3(0.2569284f, -0.1517174f, -5.535f);
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
