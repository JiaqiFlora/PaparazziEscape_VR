using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class BrekeControllerForCarChange : MonoBehaviour
{
    public Haptic brakedHaptic;
    public GameObject carObj;

    private HingeJoint hingeJoint;
    public HapticInteractable hapticInteractable;
    public XRGrabInteractable grabInteractable;
    public AudioSource audioSource;

    private bool isStropped = false;

    //private CarFollower carFollower;
    private FixedJoint fixedJoint;
    //private CarSmoothChange carFollower;
    private CarChangingController carChangingController;

    private void Start()
    {
        //carFollower = transform.parent.GetComponent<CarFollower>();
        carChangingController = transform.parent.GetComponent<CarChangingController>();

        hingeJoint = GetComponent<HingeJoint>();
        grabInteractable.selectEntered.AddListener((interactor) => OnGrabStart());
        grabInteractable.selectExited.AddListener((interactor) => OnGrabEnd());
    }

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"trigger enter: {other}");
        if (other.name == "brake Collider" && !isStropped)
        {
            TriggerBrake();
        } else if(other.name == "recover break collider" && isStropped)
        {
            RecoverBrake();
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

    private void TriggerBrake()
    {
        Debug.Log("trigger to stop");
        carChangingController.StopTheCar();
        isStropped = true;

        hapticInteractable.TriggerHaptic(brakedHaptic);
        audioSource.Play();
    }

    private void RecoverBrake()
    {
        Debug.Log("exit collide to restart the car");
        carChangingController.RestartCar();
        isStropped = false;
    }
}