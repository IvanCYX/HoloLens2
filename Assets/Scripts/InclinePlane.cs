using UnityEngine;

/*public class InclinedPlane : MonoBehaviour
{
    [SerializeField] private Transform block;
    [SerializeField] private float initialVelocity = 10f;
    [SerializeField] private float displacement = 10f;
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float rampAngle = 30f;
    [SerializeField] private float frictionCoefficient = 0.2f;

    private Rigidbody blockRigidbody;

    private void Start()
    {
        // Calculate force due to gravity along ramp
        float rampAngleInRadians = rampAngle * Mathf.Deg2Rad;
        float gravityForceAlongRamp = Physics.gravity.magnitude * Mathf.Sin(rampAngleInRadians);

        // Calculate force due to friction opposing motion
        float frictionForce = frictionCoefficient * Mathf.Abs(gravityForceAlongRamp);

        // Calculate net force along ramp
        float netForceAlongRamp = acceleration + gravityForceAlongRamp - frictionForce;

        // Calculate normal force
        float normalForce = -netForceAlongRamp;

        // Calculate time-of-flight (time taken to reach the displacement along the ramp)
        float timeOfFlight = Mathf.Sqrt((2 * displacement) / netForceAlongRamp);

        // Calculate final velocity along ramp using the equation v = u + at
        float finalVelocityAlongRamp = initialVelocity + (netForceAlongRamp * timeOfFlight);

        // Calculate final velocity perpendicular to ramp (remains constant)
        float finalVelocityPerpendicularToRamp = 0f;

        // Calculate total final velocity using Pythagorean theorem
        float totalFinalVelocity = Mathf.Sqrt(
            Mathf.Pow(finalVelocityAlongRamp, 2f) +
            Mathf.Pow(finalVelocityPerpendicularToRamp, 2f)
        );

        // Calculate direction of final velocity
        Vector3 finalVelocityDirection = Quaternion.Euler(0f, rampAngle, 0f) * Vector3.forward;

        // Apply final velocity to block
        blockRigidbody = block.GetComponent<Rigidbody>();
        blockRigidbody.velocity = finalVelocityDirection * totalFinalVelocity;

        // Apply normal force to block along y-axis
        blockRigidbody.AddForce(Vector3.up * normalForce, ForceMode.Force);
    }
}*/

using UnityEngine;

public class InclinedPlane : MonoBehaviour
{
    public float initialVelocity = 5f;
    public float displacement = 10f;
    public float acceleration = 1f;
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

        // Calculate frictional force
        frictionForce = frictionCoefficient * normalForce;

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
