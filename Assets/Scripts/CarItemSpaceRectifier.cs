using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class CarItemSpaceRectifier : MonoBehaviour
{

    public XRGrabInteractable throttle;
    public float minX, maxX, minZ, maxZ;
    public GameObject car;
    public GameObject throttleObject;

    private Vector3 initialLocalPosition;
    private bool isGrabbing;
    private Vector3 currentLocalPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialLocalPosition = transform.localPosition;

        throttle.selectEntered.AddListener((interactor) => OnGrabStart());
        throttle.selectExited.AddListener((interactor) => OnGrabEnd());

        currentLocalPosition = transform.localPosition;

    }

    private void OnGrabStart()
    {
        isGrabbing = true;

        XRBaseInteractor selectingInteractor = (XRBaseInteractor)throttle.interactorsSelecting[0];


    }

    private void OnGrabEnd()
    {
        isGrabbing = false;

        // at this moment, throttle's parent has changed to car
        //Debug.Log(transform.parent);
        currentLocalPosition = this.transform.localPosition;


        // TODO: - ganjiaqi only change x and z in the local position
        this.transform.localPosition = currentLocalPosition;

        Debug.Log($"world position of throttle is {this.transform.position}");
        Debug.Log($"local position of throttle is {currentLocalPosition}");

    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbing)
        {
            if (throttle.interactorsSelecting.Count > 0)
            {
                //Debug.Log("======");
                XRBaseInteractor selectingInteractor = (XRBaseInteractor)throttle.interactorsSelecting[0];
                //Debug.Log("!!!!!");
                //Debug.Log(selectingInteractor);
                //Debug.Log(throttle.transform.parent);
                //Debug.Log(throttle.gameObject.transform.parent);


                //localHandPosition = throttle.transform.parent.InverseTransformPoint(selectingInteractor.transform.position);
                //localHandPosition = car.transform.InverseTransformPoint(selectingInteractor.transform.position);
                Vector3 localHandPosition = selectingInteractor.transform.position;
                //Debug.Log("@@@@@");

                //Debug.Log(localHandPosition);
                //Debug.Log(selectingInteractor);

                //throttleObject.transform.localPosition = new Vector3(
                //    0,
                //    0,
                //    0
                //);





                // TODO: - ganjiaqi, later only update its x and z position or use update
                // when grabbing, update throttle object position to be the same world position with hand(interactor)
                throttleObject.transform.position = selectingInteractor.transform.position;


            }
        }
        else
        {
            //this.transform.localPosition = currentLocalPosition;
        }

    }


}
