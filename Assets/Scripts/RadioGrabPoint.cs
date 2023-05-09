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

    private void Update()
    {
        transform.position = positionToStay.transform.position;
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

        transform.position = positionToStay.transform.position;
    }

    private void OnGrabStart()
    {
        XRBaseInteractor selectingInteractor = (XRBaseInteractor)grabInteractable.interactorsSelecting[0];
        if (positionToStay != null)
        {
            transform.position = positionToStay.transform.position;
        }

        if (selectingInteractor.gameObject.tag == "Player")
        {
            Debug.Log("is grabbing radio");
            isGrabbing = true;
        }

    }

    private void OnGrabEnd()
    {
        isGrabbing = false;
    }
}
