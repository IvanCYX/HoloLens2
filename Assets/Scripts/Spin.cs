using UnityEngine;

public class Spin : MonoBehaviour
{
    private float rotationSpeed = 65f;
    private float delay = 0.015f;
    private bool isRotating = false;

    private void Start()
    {
    }

    private System.Collections.IEnumerator RotateCube()
    {
        // Rotate the cube 180 degrees on the y-axis clockwise
        Quaternion targetRotation = Quaternion.Euler(0f, -180f, 0f);
        Quaternion initialRotation = transform.rotation;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * (rotationSpeed / 180f);
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);
            yield return null;
        }

        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Rotate the cube back 180 degrees on the y-axis counter-clockwise
        targetRotation = Quaternion.Euler(0f, 0f, 0f);
        initialRotation = transform.rotation;

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * (rotationSpeed / 180f);
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);
            yield return null;
        }

        // Rotation completed
        isRotating = false;
    }

    public void startSpin()
    {
        StartCoroutine(RotateCube());
    }
}
