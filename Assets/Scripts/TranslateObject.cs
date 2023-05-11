using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateObject : MonoBehaviour
{
    // Start is called before the first frame update
    public void incX()
    {
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + .005f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
    }

    public void decX()
    {
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x - .005f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
    }

    public void incY()
    {
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + .005f, gameObject.transform.localPosition.z);
    }

    public void decY()
    {
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - .005f, gameObject.transform.localPosition.z);
    }

    public void incZ()
    {
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z + .005f);
    }

    public void decZ()
    {
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z - .005f);
    }
}
