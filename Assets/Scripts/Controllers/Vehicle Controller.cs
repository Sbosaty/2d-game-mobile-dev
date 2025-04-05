using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [SerializeField]
    private float acceleration = 5f, defaultMaxSpeed = 10f, turnSpeed = 200f,  driftFactor = 0.9f, friction = 3f;

    private float maxSpeed = 0;

    private float inputX, inputY = 1;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        maxSpeed = defaultMaxSpeed;
    }

    private void Update()
    {
        inputX = InputHandeler.GetTouchInput();
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

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }

    void ApplySteering()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            float turnAmount = -inputX * turnSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation + turnAmount);
        }
    }

    void ApplyDrift()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);

        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    void ApplyFriction()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, friction * Time.fixedDeltaTime);
    }

    //public void Stop() 
    //{
    //    inputY = 0;
    //}

    //public void Go() 
    //{
    //    inputY = 1;
    //}

    public void ReturnToDefaultVelocity() 
    {
        maxSpeed = defaultMaxSpeed;
    }

    public void SetMaxSpeed(float SetMaxSpeed)
    {
        maxSpeed = SetMaxSpeed;
    }
}
