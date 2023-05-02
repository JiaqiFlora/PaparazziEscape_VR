using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SteerWheelForCarChange : MonoBehaviour
{
    public CarChangingController carChangingController;

    private HingeJoint hingeJoint;
    private float previousAngle; // help to fix the hingjoint angle's problem


    // Start is called before the first frame update
    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = hingeJoint.angle;
        //Debug.Log($"hinge joint's angle is: {angle}");

        if (angle < -10 && previousAngle < 0)
        {
            carChangingController.TurnLeft();
        }
        else if (angle > 10 && previousAngle > 0)
        {
            carChangingController.TurnRight();
        }

        previousAngle = angle;
        //Debug.Log($"steer wheel angle is: {angle}");
    }
}
