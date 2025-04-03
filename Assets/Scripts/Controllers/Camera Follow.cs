using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float baseZoom = 5f, maxZoomOut = 10f, zoomSpeedFactor = 1f, smoothSpeed = 5f;

    private Transform target;
    
    private Rigidbody2D targetRb;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;

        FindTarget();

    }

    private void FixedUpdate()
    {
        if (target == null || targetRb == null)
            return;

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        //AdjustZoom();
    }

    void AdjustZoom()
    {
        float speed = targetRb.linearVelocity.magnitude; 

        float targetZoom = baseZoom + (speed * zoomSpeedFactor);

        targetZoom = Mathf.Clamp(targetZoom, baseZoom, maxZoomOut);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * smoothSpeed);
    }

    public void FindTarget() 
    {
        GameObject newTarget = GameObject.FindWithTag("Player");

        if (newTarget == isActiveAndEnabled)
        {
            target = newTarget.transform;
            targetRb = target.GetComponent<Rigidbody2D>();

        }
    }
}
