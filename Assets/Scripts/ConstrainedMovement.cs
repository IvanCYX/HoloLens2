using UnityEngine;

public class ConstrainedMovement : MonoBehaviour
{
    public float speed = 5f;
    public float minZ = 0f;
    public float maxZ = 0.05f;

    private void Update()
    {
        // Get the current position of the object
        Vector3 currentPosition = transform.position;

        // Calculate the new position based on the input and speed
        float moveDistance = speed * Time.deltaTime;
        float newZ = Mathf.Clamp(currentPosition.z + moveDistance, minZ, maxZ);

        // Set the new position, only updating the Z-axis
        transform.position = new Vector3(currentPosition.x, currentPosition.y, newZ);
    }
}
