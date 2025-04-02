using UnityEngine;

public class CarController : MonoBehaviour
{
    public float acceleration = 5f, maxSpeed = 10f, turnSpeed = 200f,  driftFactor = 0.9f, friction = 3f;   

    private float inputX, inputY = 1;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            inputX = InputHandeler.GetTouchInput();
        }
        else 
        {
            inputX = Input.GetAxisRaw("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        ApplyAcceleration();

        ApplySteering();

        ApplyDrift();

        if (inputY == 0) 
        {
            ApplyFriction();
        }
    }

    void ApplyAcceleration()
    {
        if (inputY != 0)
        {
            rb.AddForce(transform.up * inputY * acceleration, ForceMode2D.Force);
        }

        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    void ApplySteering()
    {
        if (rb.linearVelocity.magnitude > 0.1f)
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
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, friction * Time.fixedDeltaTime);
    }
}
