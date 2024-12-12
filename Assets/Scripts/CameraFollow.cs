using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Drag the Player object here in the Inspector
    public Vector3 offset = new Vector3(0, 1, -10); // Adjust as needed
    public float smoothSpeed = 0.125f; // Smoothing factor

    void LateUpdate()
    {
        if (player != null)
        {
            // Desired position with offset
            Vector3 desiredPosition = player.position + offset;
            // Smoothly interpolate to the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}

