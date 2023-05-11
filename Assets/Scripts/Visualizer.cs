using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    GameObject pulse;
    GameObject[] pulses;
    int index, length, step;

    // Start is called before the first frame update
    void Start()
    {
        step = 2;
        length = (100/step) +1 ;
        pulses = new GameObject[length];
        index = 0;
        
        for (int i = 0; i < 101; i += step)
        {
            pulse = GameObject.Instantiate(Resources.Load("TestPulses/Pulse" + i.ToString()) as GameObject, Vector3.zero, Quaternion.identity);
            togglePulse(pulse, false);
            pulses[(int)(i/step)] = pulse;
            
        }

        StartCoroutine(nextPulse());      


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void togglePulse(GameObject pulse, bool visible)
    {
        MeshRenderer[] mrs = pulse.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in mrs)
        {
            mr.enabled = visible;
        }
    }
    IEnumerator nextPulse()
    {
        while(true)
        {
            togglePulse(pulses[index], true);
            if (index == 0)
            {
                togglePulse(pulses[length - 1], false);
            }
            if (index > 0)
            {
                togglePulse(pulses[index - 1], false);
            }
            index++;
            if (index == length)
            {
                index = 0;
            }
            Debug.Log(index);
            yield return new WaitForSeconds(0.5f);

        }
    }

}
