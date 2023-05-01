using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class SimpleCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float maxBreakingTorque;

    public float steerHelper = 0.5f;
    public float steerInput = 0f; // -1 to 1
    public float motorInput;
    public bool isBrake;

    [Header("for animation")]
    public GameObject oldSteeringWheel;
    public GameObject newSteeringWheel;
    public GameObject oldGear1;
    public GameObject oldGear2;
    public GameObject newGear;

    private float oldRotation;
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        // update rotation, because wheels have 90 degrees at first
        Vector3 euler = rotation.eulerAngles;
        euler.z += 90;
        Quaternion zRotation = Quaternion.Euler(euler);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = zRotation;
    }

    public void FixedUpdate()
    {
        // TODO: - ganjiaqi, temp use input key, change to controller later!
        //float motor = maxMotorTorque * Input.GetAxis("Vertical");
        //float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        //float brakeTorque = 0f;

        float motor = maxMotorTorque * motorInput;
        float steering = maxSteeringAngle * steerInput;
        float brakeTorque = 0f;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isBrake = true;
        }

        if (isBrake)
        {
            brakeTorque = maxBreakingTorque;
        }

        Debug.Log($"motor: {motor}");
        Debug.Log($"brakeTorque: {brakeTorque}");
        Debug.Log($"steer angle: {steering}");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
        }

        SteerHelper();

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if(axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            axleInfo.leftWheel.brakeTorque = brakeTorque;
            axleInfo.rightWheel.brakeTorque = brakeTorque;

            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }

    private void SteerHelper()
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            WheelHit leftWheelHit;
            axleInfo.leftWheel.GetGroundHit(out leftWheelHit);
            if(leftWheelHit.normal == Vector3.zero)
            {
                return;
            }

            WheelHit rightWheeHit;
            axleInfo.rightWheel.GetGroundHit(out rightWheeHit);
            if (rightWheeHit.normal == Vector3.zero)
            {
                return;
            }
        }

        if (Mathf.Abs(oldRotation - transform.eulerAngles.y) < 10f)
        {
            var turnAdjust = (transform.eulerAngles.y - oldRotation) * steerHelper;
            Quaternion velRotation = Quaternion.AngleAxis(turnAdjust, Vector3.up);
            rigidbody.velocity = velRotation * rigidbody.velocity;
        }

        oldRotation = transform.eulerAngles.y;
    }

    public void StartCarModelChange()
    {
        oldSteeringWheel.SetActive(false);
        newSteeringWheel.SetActive(true);
        oldGear1.SetActive(false);
        oldGear2.SetActive(false);
        newGear.SetActive(true);
    }
}