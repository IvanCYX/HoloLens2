using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteObjects: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ShowModel()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Debug.Log("Showing Image");
    }

    public void HideModel()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log("Hide Image");
    }
}
