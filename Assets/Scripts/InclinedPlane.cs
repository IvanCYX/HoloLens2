using UnityEngine;

public class InclinedPlane : MonoBehaviour
{
    public float initialVelocity = 5f;
    public float displacement = 10f;
    public float acceleration = 1f;
    //angle along z-axis
    public float rampAngle = 30f;
    public float frictionCoefficient = 0.1f;

    private Rigidbody rb;
    private float normalForce;
    private float frictionForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        MoveBlock();
    }

    private void MoveBlock()
    {
        // Calculate normal force
        normalForce = rb.mass * Mathf.Cos(rampAngle * Mathf.Deg2Rad);
        Debug.Log(normalForce);

        // Calculate frictional force
        frictionForce = frictionCoefficient * normalForce;
        Debug.Log(frictionForce);

        // Calculate acceleration of block along inclined plane
        float rampAngleRad = rampAngle * Mathf.Deg2Rad;
        float accelerationAlongRamp = acceleration * Mathf.Sin(rampAngleRad);

        // Adjust acceleration to account for friction
        accelerationAlongRamp -= frictionForce / rb.mass;

        // Calculate time taken for block to reach displacement
        float time = Mathf.Sqrt(2f * displacement / accelerationAlongRamp);

        // Calculate final velocity at end of displacement
        float finalVelocity = initialVelocity + accelerationAlongRamp * time;

        // Set velocity of block in direction of inclined plane
        Vector3 velocity = new Vector3(finalVelocity * Mathf.Cos(rampAngleRad), 0f, finalVelocity * Mathf.Sin(rampAngleRad));
        rb.velocity = velocity;
    }
}
