using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePattern : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject model;
    // Start is called before the first frame update
    void Start()
    {
        model.SetActive(false);

    }

    public void ShowModel()
    {
        model.SetActive(true);
    }
}