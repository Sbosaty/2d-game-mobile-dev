using System;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public float accFactor = 30.0f;
    public float turnFactor = 3.5f;

    public float accInput = 0;
    public float steerInput = 0;

    float rotationAngle;

    private Rigidbody2D carRigidbody;

    private void Awake()
    {
        carRigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyEngineForce();

        ApplySteering();
    }

    private void ApplySteering()
    {
        rotationAngle -= steerInput * turnFactor;
        carRigidbody.MoveRotation(rotationAngle);
    }

    private void ApplyEngineForce()
    {
        Vector2 engineForceVector = transform.up * accFactor * accInput;

        carRigidbody.AddForce(engineForceVector, ForceMode2D.Force);
    }

    public void SetInputVector(Vector2 inputVector) {
        steerInput = inputVector.x;
        accFactor = inputVector.y;
    }

}
