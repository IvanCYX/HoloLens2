using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneObjects : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject model;

    private void Start()
    {
        model.SetActive(false);
    }
    public void HideModel()
    {
        model.SetActive(false);
    }

    public void ShowModel()
    {
        model.SetActive(true);
    }
}
