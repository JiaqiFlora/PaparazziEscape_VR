using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabPointSteerWheel : MonoBehaviour
{
    public GameObject positionToStay;

    private XRGrabInteractable grabInteractable;
    private GameObject grabbedHand;
    private GameObject handModelObject;
    private bool isGrabbing;
    

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener((interactor) => OnGrabStart());
        grabInteractable.selectExited.AddListener((interactor) => OnGrabEnd());
    }

    // Update is called once per frame
    void Update()
    {
        //if(isGrabbing && positionToStay != null)
        //{
        //    transform.position = positionToStay.transform.position;
        //    transform.rotation = positionToStay.transform.rotation;

        //    Debug.Log($"{this.gameObject.name} grab point position: {transform.position}");
        //    Debug.Log($"{positionToStay.name} grab point signal's position: {positionToStay.transform.position}");
        //}
    }

    // only can use late update!!!! update will trigger some grab points problem. why......
    private void LateUpdate()
    {
        if (isGrabbing && positionToStay != null)
        {
            transform.position = positionToStay.transform.position;
            transform.rotation = positionToStay.transform.rotation;

            Debug.Log($"{this.gameObject.name} grab point position: {transform.position}");
            Debug.Log($"{positionToStay.name} grab point signal's position: {positionToStay.transform.position}");
        }
    }

    private void OnGrabStart()
    {
        XRBaseInteractor selectingInteractor = (XRBaseInteractor)grabInteractable.interactorsSelecting[0];

        if (positionToStay != null)
        {
            transform.position = positionToStay.transform.position;
            transform.rotation = positionToStay.transform.rotation;
        }

        if (selectingInteractor.gameObject.tag == "Player")
        {
            Debug.Log("is grabbing steering wheel");
            isGrabbing = true;
            grabbedHand = selectingInteractor.gameObject;

            handModelObject = grabbedHand.transform.GetChild(0).gameObject;
            handModelObject.SetActive(false);

            if (handModelObject.tag == "Right Hand Model")
            {
                Debug.Log("right hand model show up!");
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.GetComponent<Animator>().SetFloat("Grip", 1f);
            }
            else if (handModelObject.tag == "Left Hand Model")
            {
                Debug.Log("left hand model show up!");
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.GetComponent<Animator>().SetFloat("Grip", 1f);
            }
        }
    }

    private void OnGrabEnd()
    {
        if (grabbedHand != null)
        {
            Debug.Log("release the steering wheel");
            
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            handModelObject.SetActive(true);

            isGrabbing = false;
            grabbedHand = null;
        }
    }
}
