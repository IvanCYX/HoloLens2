using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MotionControl : MonoBehaviour
{
    [SerializeField]
    private readonly float limit = -4.5f;
    public float doubleSlitPos = -3f;
    private Vector3 position, jitter, trajectory;
    private float phi, theta, radius, radMax;
    public float speed;
    public GameObject target;

    private void Start()
    {
        speed = 3f;
        position = new Vector3(2.34f, 0.75f, 3f);
        jitter = Vector3.zero;
        radMax = 0.015f;
        trajectory = new Vector3(-1f, 0f, 0f);
    }

    void Update()
    {
        position += Time.deltaTime * speed * trajectory;
        phi = Random.Range(0, Mathf.PI);
        theta = Random.Range(0, 2 * Mathf.PI);
        radius = expotential(Random.Range(0f, 1.5f));
        float sinT = Mathf.Sin(theta);
        jitter.x = radius * sinT * Mathf.Cos(phi);
        jitter.y = radius * sinT * Mathf.Sin(phi);
        jitter.z = radius * Mathf.Cos(theta);
        gameObject.transform.position = position + jitter;

        if (gameObject.transform.localPosition.x < doubleSlitPos)
        {
            //make electron move to a interference point after going through double slit
            Vector3 direction = target.transform.position - transform.position;
            trajectory = direction.normalized;
        }
        if (gameObject.transform.localPosition.x < limit)
        {
            position = new Vector3(2.34f, 0.75f, 3f);
            gameObject.transform.localPosition = position + jitter;
        }
    }

    private float expotential(float v)
    {
        return radMax * (Mathf.Exp(v) - 1f);
    }
}