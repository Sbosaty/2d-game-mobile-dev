using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float acceleration = 5f;   // Forward acceleration
    public float maxSpeed = 10f;      // Maximum speed
    public float turnSpeed = 200f;    // Rotation speed
    public float driftFactor = 0.9f;  // Higher means less sliding
    public float friction = 3f;       // How fast the car slows down when not moving

    private Rigidbody2D rb;
    private float inputX;
    private float inputY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get player input
        inputX = Input.GetAxis("Horizontal"); // Left / Right
        inputY = Input.GetAxis("Vertical");   // Up / Down
    }

    private void FixedUpdate()
    {
        ApplyAcceleration();
        ApplySteering();
        ApplyDrift();
        ApplyFriction();
    }

    void ApplyAcceleration()
    {
        if (inputY != 0)
        {
            rb.AddForce(transform.up * inputY * acceleration, ForceMode2D.Force);
        }

        // Limit max speed
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    void ApplySteering()
    {
        if (rb.linearVelocity.magnitude > 0.1f) // Turn only when moving
        {
            float turnAmount = -inputX * turnSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation + turnAmount);
        }
    }

    void ApplyDrift()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.linearVelocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.linearVelocity, transform.right);

        rb.linearVelocity = forwardVelocity + rightVelocity * driftFactor;
    }

    void ApplyFriction()
    {
        if (inputY == 0) // If no acceleration input, apply friction
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, friction * Time.fixedDeltaTime);
        }
    }
}
