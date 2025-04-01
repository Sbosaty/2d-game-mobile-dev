using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The car's transform
    public Rigidbody2D targetRb; // The car's Rigidbody2D
    public float smoothSpeed = 5f; // Camera follow smoothness
    public float baseZoom = 5f; // Default zoom level
    public float maxZoomOut = 10f; // Max zoom out level
    public float zoomSpeedFactor = 1f; // How much zoom changes with speed

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;

        target = GameManager.Instance.selectedCar.transform;

        targetRb = target.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (target == null || targetRb == null) return;

        // Smoothly follow target position
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Adjust camera zoom based on speed
        AdjustZoom();
    }

    void AdjustZoom()
    {
        float speed = targetRb.linearVelocity.magnitude; // Get car speed
        float targetZoom = baseZoom + (speed * zoomSpeedFactor);
        targetZoom = Mathf.Clamp(targetZoom, baseZoom, maxZoomOut); // Limit zoom range

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * smoothSpeed);
    }
}
