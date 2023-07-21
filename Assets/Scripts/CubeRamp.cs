using UnityEngine;

public class CubeRamp : MonoBehaviour
{
    public Transform ramp;  // Reference to the ramp object
    public float minAngle = 0f;  // Minimum angle of the ramp
    public float maxAngle = 90f;  // Maximum angle of the ramp
    public float rampAngle = 0f;  // Angle of the ramp


    private float slideSpeed;  // Speed at which the cube slides down

    private void Start()
    {
        // Set the initial ramp angle and slide speed
        UpdateRamp();
    }

    private void Update()
    {

    }

    private void UpdateRamp()
    {
        // Clamp the ramp angle within the specified range
        rampAngle = Mathf.Clamp(rampAngle, minAngle, maxAngle);

        // Update the ramp rotation based on the current angle
        ramp.rotation = Quaternion.Euler(0f, 0f, rampAngle);
    }

}
