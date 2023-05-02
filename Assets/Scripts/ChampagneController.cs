using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChampagneController : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private bool isGrabbing;
    private GameObject grabbedHand;
    private XRBaseInteractor selectingInteractor;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener((interactor) => OnGrabStart());
        grabInteractable.selectExited.AddListener((interactor) => OnGrabEnd());
    }

    private void OnGrabStart()
    {
        selectingInteractor = (XRBaseInteractor)grabInteractable.interactorsSelecting[0];
        grabbedHand = selectingInteractor.gameObject;

        //GetComponent<Rigidbody>().isKinematic = false;
        //GetComponent<Rigidbody>().useGravity = true;

        if (selectingInteractor.gameObject.tag == "Player")
        {
            isGrabbing = true;
        }
    }

    private void OnGrabEnd()
    {
        isGrabbing = false;

        //XRGrabInteractable interactable = this.GetComponent<XRGrabInteractable>();
        //Debug.Log($"throw velocity: {interactable.throwVelocityScale}");

        // control by path creators, just move translate on path, no gravity no speed
        //Debug.Log($"velocity now: {GetComponent<Rigidbody>().velocity}");
        //Debug.Log($"velocity normalized now: {GetComponent<Rigidbody>().velocity.normalized}");

        //Debug.Log($"world velocity now: {transform.TransformDirection(GetComponent<Rigidbody>().velocity)}");
        //Debug.Log($"world velocity normalized now: {transform.TransformDirection(GetComponent<Rigidbody>().velocity).normalized}");

        //Debug.Log($"car velocity now: {car.GetComponent<Rigidbody>().velocity}");
        //Debug.Log($"car velocity normalized now: {car.GetComponent<Rigidbody>().velocity.normalized}");

        Debug.Log($"select interactor direction: {selectingInteractor.transform.forward}");
        Vector3 throwDirection = selectingInteractor.transform.forward;
        GetComponent<Rigidbody>().AddForce(throwDirection * 1000f, ForceMode.Impulse);

        // at this moment, throttle's parent has changed to car
        if (grabbedHand != null)
        {
            //transform.position = new Vector3(grabbedHand.transform.position.x, grabbedHand.transform.position.y, grabbedHand.transform.position.z);
            grabbedHand = null;
            selectingInteractor = null;
        }

        //GetComponent<Rigidbody>().isKinematic = false;
        //GetComponent<Rigidbody>().useGravity = true;

    }

    private void LateUpdate()
    {
        if (isGrabbing)
        {
            if (grabInteractable.interactorsSelecting.Count > 0)
            {
                XRBaseInteractor selectingInteractor = (XRBaseInteractor)grabInteractable.interactorsSelecting[0];
                // when grabbing, update throttle object position to be the same world position with hand(interactor)
                transform.position = new Vector3(selectingInteractor.transform.position.x, selectingInteractor.transform.position.y, selectingInteractor.transform.position.z);
            }
        }
    }

}
