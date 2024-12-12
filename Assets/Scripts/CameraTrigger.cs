using System.Collections;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Camera mainCamera; // The main camera in the scene.
    public Transform target1; // The first target to focus on when the game starts.
    public Transform target2; // The second target to focus on after the first target.
    public float focusDuration = 2f; // Time to focus on each target.
    public float moveSpeed = 5f; // Speed at which the camera moves to each target.

    private Transform playerTransform; // Reference to the player's transform.

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Default to the main camera if not assigned.
            Debug.Log("Main camera was not assigned. Defaulting to Camera.main.");
        }

        StartCoroutine(InitialCameraFocus());
    }

    private IEnumerator InitialCameraFocus()
    {
        // Focus on target1
        Debug.Log("Starting focus on target1.");
        yield return StartCoroutine(MoveCameraToTarget(target1));

        Debug.Log("Finished focusing on target1. Waiting for duration: " + focusDuration);
        yield return new WaitForSeconds(focusDuration);

        // Focus on target2
        Debug.Log("Starting focus on target2.");
        yield return StartCoroutine(MoveCameraToTarget(target2));

        Debug.Log("Finished focusing on target2. Waiting for duration: " + focusDuration);
        yield return new WaitForSeconds(focusDuration);

        // Focus on player
        Debug.Log("Returning camera to player.");
        playerTransform = FindObjectOfType<PlayerController>().transform; // Assumes a PlayerController script is on the player object.
        yield return StartCoroutine(MoveCameraToTarget(playerTransform));

        Debug.Log("Camera focus sequence completed.");
    }

    private IEnumerator MoveCameraToTarget(Transform target)
    {
        while (Vector3.Distance(mainCamera.transform.position, new Vector3(target.position.x, target.position.y, mainCamera.transform.position.z)) > 0.1f)
        {
            mainCamera.transform.position = Vector3.Lerp(
                mainCamera.transform.position,
                new Vector3(target.position.x, target.position.y, mainCamera.transform.position.z),
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }
    }
}

