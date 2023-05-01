using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RealCarSteeringWheel : MonoBehaviour
{
    //public GameObject player;
    //public GameObject car;

    //[Header("Temp and Delete Later")]
    //public float speed = 0.03f;
    //public float turnSpeed = 5;

    private HingeJoint hingeJoint;
    private SimpleCarController carController;
    private Quaternion referenceRot;
    //private XRGrabInteractable grabInteractable;

    // Start is called before the first frame update
    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
        referenceRot = transform.rotation;
        carController = transform.parent.GetComponent<SimpleCarController>();
        //grabInteractable.selectEntered.AddListener((interactor) => OnGrabStart());
        //grabInteractable.selectExited.AddListener((interactor) => OnGrabEnd());
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: - ganjiaqi. Delet later, just for test!!!/
        // TODO: - ganjiaqi. for move, maybe parent the car and user is better!
        //car.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //player.transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // rotation1: use keyboard to control rotate, by speed
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    car.transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        //}
        //else if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    car.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        //}

        // rotation2: just rotate to the specific angle of the wheel
        //Quaternion quaternion = Quaternion.Euler(0, 30f, 0);
        //car.transform.rotation = quaternion;

        // rotation3: rotate with its specific angle
        //float angle = hingeJoint.angle;

        //Debug.Log($"ganjiaqi: -- angle is {angle}");

        //// just rotate to the specific angle of the wheel
        //Quaternion quaternion = Quaternion.Euler(0, angle, 0);
        //car.transform.rotation = quaternion;

        float angle = hingeJoint.angle;
        //carController.steerInput = angle / hingeJoint.limits.max;

        float angleNew = RotationOnAxis(1, transform.rotation * Quaternion.Inverse(referenceRot));
        //Debug.Log($"calculated angle is: {angleNew}");
        carController.steerInput = angleNew / 180f;

        //Debug.Log($"steer input: {carController.steerInput}");

    }

    private void FixedUpdate()
    {
        float angle = hingeJoint.angle;
        //carController.steerInput = angle / hingeJoint.limits.max;

        //Debug.Log($"steer wheel angle is: {angle}");
        //Debug.Log($"input for car controller steer input is: {carController.steerInput}");

        //Debug.Log($"ganjiaqi: -- angle is {angle}");

        //// just rotate to the specific angle of the wheel
        //Quaternion quaternion = Quaternion.Euler(0, angle, 0);
        //car.transform.rotation = quaternion;

        // simple rotate, temp comment
        //if (angle > 2)
        //{
        //    car.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        //}
        //else if (angle < -2)
        //{
        //    car.transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        //}
    }


    private void OnGrabStart()
    {
        GetComponent<Rigidbody>().isKinematic = false;

        Debug.Log("steering wheel set iskinematic to be false");
    }

    private void OnGrabEnd()
    {
        GetComponent<Rigidbody>().isKinematic = true;

        Debug.Log("steering wheel set iskinematic to be true");
    }

    //axis 0 = x, 1 = y, 2 = z
    private float RotationOnAxis(int axis, Quaternion rot)
    {
        rot.x /= rot.w;
        rot.y /= rot.w;
        rot.z /= rot.w;
        rot.w = 1;

        return 2.0f * Mathf.Rad2Deg * Mathf.Atan(rot[axis]);
    }

}
