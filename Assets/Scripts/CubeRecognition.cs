/*using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.ObjectRecognition;

public class CubeRecognition : MonoBehaviour
{
    public GameObject boxPrefab; // Reference to the box prefab to be instantiated
    private GameObject trackedCube; // Reference to the tracked cube

    private ObjectAVRecognitionObjectTracker objectTracker;

    private void Start()
    {
        // Get the object tracker component
        objectTracker = MixedRealityToolkit.Instance.GetService<ObjectRecognitionObjectTracker>();
        if (objectTracker == null)
        {
            Debug.LogError("Object Recognition Object Tracker not found. Make sure the MRTK ObjectRecognition package is imported.");
            return;
        }

        // Subscribe to object recognition events
        objectTracker.ObjectAdded += ObjectAddedHandler;
        objectTracker.ObjectUpdated += ObjectUpdatedHandler;
        objectTracker.ObjectRemoved += ObjectRemovedHandler;
    }

    private void OnDestroy()
    {
        // Unsubscribe from object recognition events
        if (objectTracker != null)
        {
            objectTracker.ObjectAdded -= ObjectAddedHandler;
            objectTracker.ObjectUpdated -= ObjectUpdatedHandler;
            objectTracker.ObjectRemoved -= ObjectRemovedHandler;
        }
    }

    private void ObjectAddedHandler(ObjectRecognitionEventData eventData)
    {
        if (eventData.Object.DisplayName == "Cube")
        {
            Debug.Log("Cube recognized and being tracked.");

            // Instantiate the box prefab and place it over the tracked cube
            trackedCube = eventData.GameObject;
            Instantiate(boxPrefab, trackedCube.transform.position, trackedCube.transform.rotation);
        }
    }

    private void ObjectUpdatedHandler(ObjectRecognitionEventData eventData)
    {
        if (eventData.Object.DisplayName == "Cube")
        {
            Debug.Log("Cube updated.");

            // Update the position and rotation of the box to match the tracked cube
            if (trackedCube != null)
            {
                Transform boxTransform = trackedCube.transform.Find("Box(Clone)");
                if (boxTransform != null)
                {
                    boxTransform.position = trackedCube.transform.position;
                    boxTransform.rotation = trackedCube.transform.rotation;
                }
            }
        }
    }

    private void ObjectRemovedHandler(ObjectRecognitionEventData eventData)
    {
        if (eventData.Object.DisplayName == "Cube")
        {
            Debug.Log("Cube removed.");

            // Destroy the box when the tracked cube is removed
            if (trackedCube != null)
            {
                Transform boxTransform = trackedCube.transform.Find("Box(Clone)");
                if (boxTransform != null)
                {
                    Destroy(boxTransform.gameObject);
                }
            }

            trackedCube = null;
        }
    }
}*/