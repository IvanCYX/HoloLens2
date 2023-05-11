using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseGen : MonoBehaviour
{
    public Pulse[] pulses;
    private float speed;
    private Pulse pulse;
    void Start()
    {
        pulses = new Pulse[5];
        speed = 0.2f;

        for (int i = 0; i<5; i++)
        {
            pulses[i] = new Pulse(new Vector3((float)i / 5, 0f, 0f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        pulse = pulses[0];
        pulse.setPosition(pulse.getPosition() + speed * Vector3.right * Time.deltaTime);
    }
}
