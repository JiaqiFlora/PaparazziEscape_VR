using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public WheelCollider frontRight;
    public WheelCollider frontLeft;
    public WheelCollider backRight;
    public WheelCollider backLeft;

    public float acceleration = 100f;
    public float breakingForce = 60f;

    private float currentAcceleration = 0f;
    private float currentBreakingForce = 0f;

    private void FixedUpdate()
    {
        // for brake
        if(Input.GetKey(KeyCode.Space))
        {
            currentBreakingForce = breakingForce;
        } else
        {
            currentBreakingForce = 0f;
        }

        frontLeft.brakeTorque = currentBreakingForce;
        frontRight.brakeTorque = currentBreakingForce;
        backLeft.brakeTorque = currentBreakingForce;
        backRight.brakeTorque = currentBreakingForce;


        // for acceleration, apply acceleration to the front wheels
        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        frontLeft.motorTorque = currentAcceleration;
        frontRight.motorTorque = currentAcceleration;

    }

}
