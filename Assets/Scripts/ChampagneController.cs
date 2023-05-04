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

    private Vector3 myLastPosition;
    private float myLastTime;

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

        if (selectingInteractor.gameObject.tag == "Player")
        {
            isGrabbing = true;
        }
    }

    private void OnGrabEnd()
    {
        isGrabbing = false;

        Vector3 velocityForHand = (transform.position - myLastPosition) / (Time.time - myLastTime);
        Debug.Log($"my hand velocity is: {velocityForHand}");

        Vector3 throwDirection = selectingInteractor.transform.forward;
        Debug.Log($"select interactor direction: {selectingInteractor.transform.forward}");

        GetComponent<Rigidbody>().AddForce(velocityForHand.magnitude * throwDirection * 600f, ForceMode.Impulse);
        //GetComponent<Rigidbody>().AddForce(throwDirection * 1000f, ForceMode.Impulse);

        // at this moment, throttle's parent has changed to car
        if (grabbedHand != null)
        {
            //transform.position = new Vector3(grabbedHand.transform.position.x, grabbedHand.transform.position.y, grabbedHand.transform.position.z);
            grabbedHand = null;
            selectingInteractor = null;
        }

        Invoke("DisappearChampagne", 5f);
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

                myLastPosition = transform.position;
                myLastTime = Time.time;
            }
        }
    }

    private void DisappearChampagne()
    {
        Debug.Log("champagne disappear!!");
        this.gameObject.SetActive(false);
        Destroy(this);
    }
}
