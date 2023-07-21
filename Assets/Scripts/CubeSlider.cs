using UnityEngine;
using UnityEngine.UI;

public class CubeSlider : MonoBehaviour
{
    [SerializeField] private Transform ramp;
    [SerializeField] private Transform cube;
    [SerializeField] private Transform cylinder;
    [SerializeField] private Transform sphere;
    [SerializeField] private float minAngle = 0f;
    [SerializeField] private float maxAngle = 90f;
    [SerializeField] private float stepSize = 10f;
    [SerializeField] private float rampLength = 10f;
    [SerializeField] private float cubeSpeed = 2f;
    [SerializeField] private Slider angleSlider;

    private float currentAngle;
    private SlideDownRamp slideScript;

    private void Start()
    {
        // Initialize the angle and ramp position
        currentAngle = minAngle;
        UpdateRamp();
        UpdateSlider();

        // Get reference to SlideDownRamp script
        slideScript = GetComponent<SlideDownRamp>();
    }

    private void Update()
    {
        // Handle input to change the angle
        float input = Input.GetAxis("Horizontal"); // Adjust the input axis as needed

        // Calculate the new angle based on the input and step size
        float newAngle = Mathf.Clamp(currentAngle + input * stepSize, minAngle, maxAngle);

        // Check if the angle has changed
        if (newAngle != currentAngle)
        {
            currentAngle = newAngle;
            UpdateRamp();
            UpdateSlider();
            slideScript.SetRampAngle(currentAngle); // Pass the angle to SlideDownRamp script
        }

        // Move the cube down the ramp
        float distance = cubeSpeed * Time.deltaTime;
        Vector3 newPosition = cube.position + ramp.forward * distance;
        cube.position = newPosition;
    }

    private void UpdateRamp()
    {
        // Update the ramp rotation based on the current angle
        ramp.rotation = Quaternion.Euler(currentAngle, 0f, 0f);
    }

    private void UpdateSlider()
    {
        // Calculate the normalized value within the range
        float normalizedValue = Mathf.InverseLerp(minAngle, maxAngle, currentAngle);

        // Calculate the position along the slider
        float position = normalizedValue * rampLength;

        // Move the sphere along the slider
        sphere.localPosition = new Vector3(position, sphere.localPosition.y, sphere.localPosition.z);
    }
}

public class SlideDownRamp : MonoBehaviour
{
    private float rampAngle;  // Current angle of the ramp
    private float slideSpeed;  // Slide speed of the block

    private void Start()
    {
        // Initialize the ramp angle with the default slider value
        rampAngle = 0f;
        slideSpeed = 0f;
    }

    private void Update()
    {
        // Move the block along the slide direction with the slide speed
        Vector3 slideDirection = Quaternion.Euler(-rampAngle, 0f, 0f) * -Vector3.up;
        transform.Translate(slideDirection * slideSpeed * Time.deltaTime);
    }

    public void SetRampAngle(float angle)
    {
        // Set the ramp angle and calculate the slide speed based on the angle
        rampAngle = angle;
        slideSpeed = angle * 0.05f; // Adjust the multiplier to control the speed scaling
    }
}
