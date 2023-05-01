using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class RealCarBrake : MonoBehaviour
{
    public Haptic brakedHaptic;
    public GameObject carObj;
    public HapticInteractable hapticInteractable;
    public XRGrabInteractable grabInteractable;
    private bool isStropped = false;

    //private CarFollower carFollower;
    //private FixedJoint fixedJoint;
    private SimpleCarController carController;

    private void Start()
    {
        carController = transform.parent.GetComponent<SimpleCarController>();
        grabInteractable.selectEntered.AddListener((interactor) => OnGrabStart());
        grabInteractable.selectExited.AddListener((interactor) => OnGrabEnd());
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"trigger enter: {other}");
        if (other.tag == "brake" && !isStropped)
        {
            Debug.Log("collide to stop");
            carController.isBrake = true;
            isStropped = true;

            hapticInteractable.TriggerHaptic(brakedHaptic);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log($"trigger exit: {other}");
        if (other.tag == "brake" && isStropped)
        {
            Debug.Log("exit collide to restart the car");
            carController.isBrake = false;
            isStropped = false;
        }
    }

    private void OnGrabStart()
    {
        //GetComponent<Rigidbody>().isKinematic = false;

        //Destroy(fixedJoint);

        Debug.Log("brake set iskinematic to be false");
    }

    private void OnGrabEnd()
    {
        //GetComponent<Rigidbody>().isKinematic = true;

        //fixedJoint = gameObject.AddComponent<FixedJoint>();
        //fixedJoint.connectedBody = carObj.GetComponent<Rigidbody>();

        Debug.Log("brake set iskinematic to be true");
    }
}