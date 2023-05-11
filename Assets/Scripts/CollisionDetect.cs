using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{

    //make a mark when hit wall
    public Transform hitPoint;
    public Electron electronInstance;

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact;
        Quaternion rotation;
        Vector3 position;

        for (var i = 0; i < collision.contacts.Length; i++)
        {
            contact = collision.contacts[i];
            rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            position = contact.point;
            Instantiate(hitPoint, position, rotation);
        }  
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
