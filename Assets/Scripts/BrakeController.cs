//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;


//public class BrakeController : MonoBehaviour
//{
//    public Haptic brakedHaptic; 

//    private SimpleCarController carController;
//    private HingeJoint hingeJoint;
//    private HapticInteractable hapticInteractable;
//    //private XRGrabInteractable grabInteractable;

//    public CarFollower carFollower;

//    private void Start()
//    {
//        carController = transform.parent.GetComponent<SimpleCarController>();
//        hingeJoint = GetComponent<HingeJoint>();
//        hapticInteractable = GetComponent<HapticInteractable>();
//        //grabInteractable.selectEntered.AddListener((interactor) => OnGrabStart());
//        //grabInteractable.selectExited.AddListener((interactor) => OnGrabEnd());
//    }

//    private void FixedUpdate()
//    {
//        float angle = hingeJoint.angle;
//        float ratio = angle / hingeJoint.limits.min;

//        // TODO: - ganjiaqi. temp logic for brake, can change later!!!
//        if(ratio > 0.8)
//        {
//            carController.isBrake = true;
//        } else
//        {
//            carController.isBrake = false;
//        }

//        // for haptic when braked
//        // TODO: - ganjiaqi. temp logic, can change later!!!
//        if(ratio >= 0.9 && hapticInteractable != null)
//        {
//            hapticInteractable.TriggerHaptic(brakedHaptic);
//        }
//    }

//    //private void OnGrabStart()
//    //{
//    //    GetComponent<Rigidbody>().isKinematic = false;

//    //    Debug.Log("brake set iskinematic to be false");
//    //}

//    //private void OnGrabEnd()
//    //{
//    //    GetComponent<Rigidbody>().isKinematic = true;

//    //    Debug.Log("brake set iskinematic to be true");
//    //}
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class BrakeController : MonoBehaviour
{
    public Haptic brakedHaptic;
    public GameObject carObj;

    private HingeJoint hingeJoint;
    public HapticInteractable hapticInteractable;
    public XRGrabInteractable grabInteractable;
    private bool isStropped = false;

    //private CarFollower carFollower;
    private FixedJoint fixedJoint;
    private CarSmoothChange carFollower;

    private void Start()
    {
        //carFollower = transform.parent.GetComponent<CarFollower>();
        carFollower = transform.parent.GetComponent<CarSmoothChange>();
        hingeJoint = GetComponent<HingeJoint>();
        grabInteractable.selectEntered.AddListener((interactor) => OnGrabStart());
        grabInteractable.selectExited.AddListener((interactor) => OnGrabEnd());
    }

    private void Update()
    {
        //float angle = hingeJoint.angle;
        //float ratio = angle / hingeJoint.limits.max;

        //Debug.Log($"now angle is: {angle}");

        //// TODO: - ganjiaqi. temp logic for brake, can change later!!!
        //if (ratio > 0.85 && !isStropped)
        //{
        //    Debug.Log($"angle is: {angle}");
        //    Debug.Log($"ratio is: {ratio}");
        //    carFollower.StopTheCar();
        //    isStropped = true;
        //}
        //else if (ratio < 0.15 && isStropped)
        //{
        //    Debug.Log($"angle is: {angle}");
        //    Debug.Log($"ratio is: {ratio}");
        //    carFollower.RestartCar();
        //    isStropped = false;
        //}

        // for haptic when braked
        // TODO: - ganjiaqi. temp logic, can change later!!!
        //if (ratio >= 0.9 && hapticInteractable != null)
        //{
        //    hapticInteractable.TriggerHaptic(brakedHaptic);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"trigger enter: {other}");
        if (other.tag == "brake" && !isStropped)
        {
            Debug.Log("collide to stop");
            carFollower.StopTheCar();
            isStropped = true;

            hapticInteractable.TriggerHaptic(brakedHaptic);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log($"trigger exit: {other}");
        if(other.tag == "brake" && isStropped)
        {
            Debug.Log("exit collide to restart the car");
            carFollower.RestartCar();
            isStropped = false;
        }
    }

    private void OnGrabStart()
    {
        GetComponent<Rigidbody>().isKinematic = false;

        Destroy(fixedJoint);

        Debug.Log("brake set iskinematic to be false");
    }

    private void OnGrabEnd()
    {
        GetComponent<Rigidbody>().isKinematic = true;

        fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = carObj.GetComponent<Rigidbody>();

        Debug.Log("brake set iskinematic to be true");
    }
}