using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideObjects : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject model;
    public void HideModel()
    {
        model.SetActive(false);
    }
}
