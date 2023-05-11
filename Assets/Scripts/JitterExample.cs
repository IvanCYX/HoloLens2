using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JitterExample : MonoBehaviour
{
    private Vector3 position, jitter, trajectory;
    private float speed, phi, theta, radius, radMax;

    // Start is called before the first frame update
    void Start()
    {
        position = Vector3.zero;
        jitter = Vector3.zero;
        radMax = 0.015f;
        speed = 0.1f;
        trajectory = new Vector3(1f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        position = position + Time.deltaTime *speed * trajectory;

        phi = Random.Range(0, Mathf.PI);
        theta = Random.Range(0, 2 * Mathf.PI);
        radius = exponential(Random.Range(0f, 1.5f));
        float sinT = Mathf.Sin(theta);

        jitter.x = radius * sinT * Mathf.Cos(phi);
        jitter.y = radius * sinT * Mathf.Sin(phi);
        jitter.z = radius * Mathf.Cos(theta);

        gameObject.transform.position = position + jitter;
    }

    private float exponential(float randomInput)
    {
        return radMax * (Mathf.Exp(randomInput) -1f);
    }
}
