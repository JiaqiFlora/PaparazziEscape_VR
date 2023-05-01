//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;


//public class SpaceRectifier : MonoBehaviour
//{

//    public XRGrabInteractable throttle;
//    public float minX, maxX, minZ, maxZ;
//    public GameObject car;
//    public GameObject throttleObject;

//    private Vector3 initialLocalPosition;
//    private bool isGrabbing;
//    private Vector3 currentLocalPosition;

//    // Start is called before the first frame update
//    void Start()
//    {
//        initialLocalPosition = transform.localPosition;

//        throttle.selectEntered.AddListener((interactor) => OnGrabStart());
//        throttle.selectExited.AddListener((interactor) => OnGrabEnd());

//        currentLocalPosition = transform.localPosition;

//    }

//    private void OnGrabStart()
//    {
//        isGrabbing = true;

//    }

//    private void OnGrabEnd()
//    {
//        isGrabbing = false;

//        // at this moment, throttle's parent has changed to car
//        //Debug.Log(transform.parent);
//        //currentLocalPosition = this.transform.localPosition;
//        //this.transform.localPosition = currentLocalPosition;

//        //Debug.Log($"world position of throttle is {this.transform.position}");
//        //Debug.Log($"local position of throttle is {currentLocalPosition}");

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (isGrabbing)
//        {
//            if (throttle.interactorsSelecting.Count > 0)
//            {
//                //Debug.Log("======");
//                XRBaseInteractor selectingInteractor = (XRBaseInteractor)throttle.interactorsSelecting[0];
//                //Debug.Log("!!!!!");
//                //Debug.Log(selectingInteractor);
//                //Debug.Log(throttle.transform.parent);
//                //Debug.Log(throttle.gameObject.transform.parent);


//                //localHandPosition = throttle.transform.parent.InverseTransformPoint(selectingInteractor.transform.position);
//                //localHandPosition = car.transform.InverseTransformPoint(selectingInteractor.transform.position);
//                Vector3 localHandPosition = selectingInteractor.transform.position;
//                //Debug.Log("@@@@@");

//                //Debug.Log(localHandPosition);
//                //Debug.Log(selectingInteractor);

//                //throttleObject.transform.localPosition = new Vector3(
//                //    0,
//                //    0,
//                //    0
//                //);





//                // TODO: - ganjiaqi, later only update its x and z position
//                // when grabbing, update throttle object position to be the same world position with hand(interactor)
//                //throttleObject.transform.position = selectingInteractor.transform.position;
//            }
//        } else
//        {
//            //this.transform.localPosition = currentLocalPosition;
//        }

//    }


//}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;


//public class SpaceRectifier : MonoBehaviour
//{

//    public XRGrabInteractable throttle;
//    public float minX, maxX, minZ, maxZ;
//    public GameObject car;
//    public GameObject throttleObject;
//    public Waypoints path;

//    private Vector3 initialLocalPosition;
//    private bool isGrabbing;
//    private Vector3 currentLocalPosition;

//    // Start is called before the first frame update
//    void Start()
//    {
//        initialLocalPosition = transform.localPosition;

//        throttle.selectEntered.AddListener((interactor) => OnGrabStart());
//        throttle.selectExited.AddListener((interactor) => OnGrabEnd());

//        currentLocalPosition = transform.localPosition;

//    }

//    private void OnGrabStart()
//    {
//        isGrabbing = true;

//        XRBaseInteractor selectingInteractor = (XRBaseInteractor)throttle.interactorsSelecting[0];


//    }

//    private void OnGrabEnd()
//    {
//        isGrabbing = false;

//        // at this moment, throttle's parent has changed to car
//        //Debug.Log(transform.parent);
//        currentLocalPosition = this.transform.localPosition;


//        // TODO: - ganjiaqi only change x and z in the local position
//        this.transform.localPosition = currentLocalPosition;

//        PinToClosestPointThroughPath();

//        Debug.Log($"world position of throttle is {this.transform.position}");
//        Debug.Log($"local position of throttle is {currentLocalPosition}");

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (isGrabbing)
//        {
//            if (throttle.interactorsSelecting.Count > 0)
//            {
//                //Debug.Log("======");
//                XRBaseInteractor selectingInteractor = (XRBaseInteractor)throttle.interactorsSelecting[0];
//                //Debug.Log("!!!!!");
//                //Debug.Log(selectingInteractor);
//                //Debug.Log(throttle.transform.parent);
//                //Debug.Log(throttle.gameObject.transform.parent);


//                //localHandPosition = throttle.transform.parent.InverseTransformPoint(selectingInteractor.transform.position);
//                //localHandPosition = car.transform.InverseTransformPoint(selectingInteractor.transform.position);
//                Vector3 localHandPosition = selectingInteractor.transform.position;
//                //Debug.Log("@@@@@");

//                //Debug.Log(localHandPosition);
//                //Debug.Log(selectingInteractor);

//                //throttleObject.transform.localPosition = new Vector3(
//                //    0,
//                //    0,
//                //    0
//                //);





//                // TODO: - ganjiaqi, later only update its x and z position or use update
//                // when grabbing, update throttle object position to be the same world position with hand(interactor)
//                throttleObject.transform.position = new Vector3(selectingInteractor.transform.position.x, throttleObject.transform.position.y, selectingInteractor.transform.position.z);

//                PinToClosestPointThroughPath();
//            }
//        }
//        else
//        {
//            //this.transform.localPosition = currentLocalPosition;
//        }

//    }


//    private void PinToClosestPointThroughPath()
//    {
//        Vector3 closestPoint = path.GetClosestPoint(transform.position);
//        transform.position = closestPoint;
//    }

//}





//// clean version here! previous versions are above
//// space recitifier for throttle, only throttle, not for other items in the car
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;


//public class SpaceRectifier : MonoBehaviour
//{

//    public XRGrabInteractable throttle;
//    public float minX, maxX, minZ, maxZ;
//    public GameObject car;
//    public GameObject throttleObject;
//    public Waypoints path;

//    private Vector3 initialLocalPosition;
//    private bool isGrabbing;
//    private Vector3 currentLocalPosition;

//    // TOD: - ganjiaqi temp to delete
//    public GameObject gear5;
//    public GameObject gear4;
//    public GameObject tempRightHand;

//    // Start is called before the first frame update
//    void Start()
//    {
//        initialLocalPosition = transform.localPosition;

//        throttle.selectEntered.AddListener((interactor) => OnGrabStart());
//        throttle.selectExited.AddListener((interactor) => OnGrabEnd());

//        currentLocalPosition = transform.localPosition;

//    }

//private void OnGrabStart()
//{
//    XRBaseInteractor selectingInteractor = (XRBaseInteractor)throttle.interactorsSelecting[0];

//    if (selectingInteractor.gameObject.tag == "Player")
//    {
//        isGrabbing = true;

//        Debug.Log("change isgrabbing to true!!!+++++++++++++++++++++++++++++++++++++++++++");
//    }


//}

//private void OnGrabEnd()
//{
//    isGrabbing = false;
//    Debug.Log("change isgrabbing to false!!!!!!-------------------------------------");

//    // at this moment, throttle's parent has changed to car
//    //Debug.Log(transform.parent);
//    currentLocalPosition = this.transform.localPosition;

//    // TODO: - ganjiaqi only change x and z in the local position
//    //this.transform.localPosition = currentLocalPosition;
//    throttleObject.transform.position = new Vector3(tempRightHand.transform.position.x, throttleObject.transform.position.y, tempRightHand.transform.position.z);


//    Debug.Log("grab end!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

//    Debug.Log($"grab end throttle transform position is: {throttleObject.transform.position}");
//    Debug.Log($"grab end right hand transform position is: {tempRightHand.transform.position}");
//    PinToClosestPointThroughPath();

//}

//    //Update is called once per frame
//    void Update()
//    {
//        if (isGrabbing)
//        {
//            if (throttle.interactorsSelecting.Count > 0)
//            {
//                XRBaseInteractor selectingInteractor = (XRBaseInteractor)throttle.interactorsSelecting[0];
//                // when grabbing, update throttle object position to be the same world position with hand(interactor)
//                throttleObject.transform.position = new Vector3(selectingInteractor.transform.position.x, throttleObject.transform.position.y, selectingInteractor.transform.position.z);

//                Debug.Log("is grabbing");
//                PinToClosestPointThroughPath();
//            }

//            Debug.Log($"isgrabbing,    and  after update position, throttle position: {throttleObject.transform.position}");
//        } else
//        {
//            Debug.Log($"ungrabing,    and  after update position, throttle position: {throttleObject.transform.position}");
//        }


//    }

//    //private void LateUpdate()
//    //{
//    //    if (isGrabbing)
//    //    {
//    //        if (throttle.interactorsSelecting.Count > 0)
//    //        {
//    //            XRBaseInteractor selectingInteractor = (XRBaseInteractor)throttle.interactorsSelecting[0];
//    //            // when grabbing, update throttle object position to be the same world position with hand(interactor)
//    //            throttleObject.transform.position = new Vector3(selectingInteractor.transform.position.x, throttleObject.transform.position.y, selectingInteractor.transform.position.z);

//    //            PinToClosestPointThroughPath();
//    //        }
//    //    }
//    //}


//    private void PinToClosestPointThroughPath()
//    {
//        Debug.Log($"before calculation transform position is: {throttleObject.transform.position}");
//        Debug.Log($"gear4 is: {gear4.transform.position}");

//        Vector3 closestPoint = path.GetClosestPoint(throttleObject.transform.position);
//        transform.position = closestPoint;

//        Debug.Log($"current transform position is: {throttleObject.transform.position}");
//        Debug.Log($"closestoint is: {closestPoint}");
//        Debug.Log($"gear4 is: {gear4.transform.position}");
//        Debug.Log($"gear5 is: {gear5.transform.position}");
//    }
//}






// clean version without debug information
// space recitifier for throttle, only throttle, not for other items in the car
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class SpaceRectifier : MonoBehaviour
{
    public float minX, maxX, minZ, maxZ;
    public Waypoints path;

    private XRGrabInteractable throttle;
    private bool isGrabbing;
    private GameObject grabbedHand;


    // Start is called before the first frame update
    void Start()
    {
        throttle = GetComponent<XRGrabInteractable>();
        throttle.selectEntered.AddListener((interactor) => OnGrabStart());
        throttle.selectExited.AddListener((interactor) => OnGrabEnd());
    }

    private void OnGrabStart()
    {
        XRBaseInteractor selectingInteractor = (XRBaseInteractor)throttle.interactorsSelecting[0];
        grabbedHand = selectingInteractor.gameObject;

        if (selectingInteractor.gameObject.tag == "Player")
        {
            isGrabbing = true;
        }
    }

    private void OnGrabEnd()
    {
        isGrabbing = false;

        // at this moment, throttle's parent has changed to car
        if(grabbedHand != null)
        {
            transform.position = new Vector3(grabbedHand.transform.position.x, transform.position.y, grabbedHand.transform.position.z);
            grabbedHand = null;
        }

        PinToClosestPointThroughPath();


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
            if (throttle.interactorsSelecting.Count > 0)
            {
                XRBaseInteractor selectingInteractor = (XRBaseInteractor)throttle.interactorsSelecting[0];
                // when grabbing, update throttle object position to be the same world position with hand(interactor)
                transform.position = new Vector3(selectingInteractor.transform.position.x, transform.position.y, selectingInteractor.transform.position.z);

                PinToClosestPointThroughPath();
            }
        }
    }

    private void PinToClosestPointThroughPath()
    {
        Vector3 closestPoint = path.GetClosestPoint(transform.position);
        transform.position = new Vector3(closestPoint.x, closestPoint.y + 0.11f, closestPoint.z);
    }
}
