using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    public float mouseSensitivity = 2f;
    public float minClampAngle = -20.0f; // Minimum angle the camera can look up
    public float maxClampAngle = 85.0f; // Maximum angle the camera can look down

    private float rotX, rotY;

    void Start()
    {
        offset = transform.position - player.transform.position;
        rotX = transform.eulerAngles.x;
        rotY = transform.eulerAngles.y;
    }

    void LateUpdate()
    {
        // Move the camera relative to the player
        transform.position = player.transform.position + offset;

        // Rotate the camera based on mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, minClampAngle, maxClampAngle);

        rotY += mouseX;

        transform.localRotation = Quaternion.Euler(rotX, rotY, 0);
    }
}