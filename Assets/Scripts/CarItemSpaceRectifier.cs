using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class CarItemSpaceRectifier : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private bool isGrabbing;
    private GameObject grabbedHand;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener((interactor) => OnGrabStart());
        grabInteractable.selectExited.AddListener((interactor) => OnGrabEnd());
    }

    private void OnGrabStart()
    {
        XRBaseInteractor selectingInteractor = (XRBaseInteractor)grabInteractable.interactorsSelecting[0];
        grabbedHand = selectingInteractor.gameObject;

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;

        if (selectingInteractor.gameObject.tag == "Player")
        {
            isGrabbing = true;
        }
    }

    private void OnGrabEnd()
    {
        isGrabbing = false;

        // at this moment, throttle's parent has changed to car
        if (grabbedHand != null)
        {
            transform.position = new Vector3(grabbedHand.transform.position.x, grabbedHand.transform.position.y, grabbedHand.transform.position.z);
            grabbedHand = null;
        }

        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;


    }

    //Update is called once per frame
    //void Update()
    //{
    //    if (isGrabbing)
    //    {
    //        if (throttle.interactorsSelecting.Count > 0)
    //        {
    //            XRBaseInteractor selectingInteractor = (XRBaseInteractor)throttle.interactorsSelecting[0];
    //            // when grabbing, update throttle object position to be the same world position with hand(interactor)
    //            transform.position = new Vector3(selectingInteractor.transform.position.x, transform.position.y, selectingInteractor.transform.position.z);

    //            PinToClosestPointThroughPath();
    //        }
    //    }
    //}

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
