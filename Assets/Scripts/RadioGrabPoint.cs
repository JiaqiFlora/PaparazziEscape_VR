using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RadioGrabPoint : MonoBehaviour
{
    public GameObject positionToStay;
    private XRGrabInteractable grabInteractable;
    private bool isGrabbing;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener((interactor) => OnGrabStart());
        grabInteractable.selectExited.AddListener((interactor) => OnGrabEnd());
    }

    private void LateUpdate()
    {
        transform.position = positionToStay.transform.position;
        transform.rotation = positionToStay.transform.rotation;
    }

    private void OnGrabStart()
    {
        GetComponent<Rigidbody>().isKinematic = false;

        XRBaseInteractor selectingInteractor = (XRBaseInteractor)grabInteractable.interactorsSelecting[0];
        if (positionToStay != null)
        {
            transform.position = positionToStay.transform.position;
            transform.rotation = positionToStay.transform.rotation;
        }
    }

    private void OnGrabEnd()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
