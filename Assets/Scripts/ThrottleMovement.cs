//// Hadi's original version
////using UnityEngine;
////using UnityEngine.XR.Interaction.Toolkit;

////public class ThrottleMovement : MonoBehaviour
////{
////    public GameObject Car;
////    public float CarSpeed = 3f;
////    public Waypoints path;
////    public XRGrabInteractable throttle;

////    private Vector3 initialLocalPosition;
////    private float speedMultiplier;
////    private bool isGrabbing;
////    private bool isInContactWithGear1;

////    private void Start()
////    {
////        throttle = GetComponent<XRGrabInteractable>();
////    }

////    private void Update()
////    {
////        if (throttle.isSelected)
////        {
////            Vector3 closestPoint = path.GetClosestPoint(transform.position);
////            transform.position = closestPoint;
////        }

////        if (isInContactWithGear1)
////        {
////            Car.transform.position += Car.transform.forward * CarSpeed * Time.deltaTime;
////        }
////    }

////    private void OnTriggerEnter(Collider other)
////    {
////        if (other.tag == "gear1")
////        {
////            isInContactWithGear1 = true;
////        }
////    }

////    private void OnTriggerExit(Collider other)
////    {
////        if (other.tag == "gear1")
////        {
////            isInContactWithGear1 = false;
////        }
////    }
////}




//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

//public class ThrottleMovement : MonoBehaviour
//{
//    public GameObject Car;
//    public float CarSpeed = 3f;
//    public Waypoints path;
//    public XRGrabInteractable throttle;

//    private Vector3 initialLocalPosition;
//    private float speedMultiplier;
//    private bool isGrabbing;
//    private bool isInContactWithGear1;
//    private SimpleCarController carController;

//    private void Start()
//    {
//        throttle = GetComponent<XRGrabInteractable>();
//        carController = this.transform.parent.GetComponent<SimpleCarController>();
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        switch(other.tag)
//        {
//            case "gear0":
//                carController.motorInput = -1.0f;
//                Debug.Log("reach to gear0");
//                break;
//            case "gear1":
//                carController.motorInput = 0.2f;
//                Debug.Log("reach to gear1");
//                break;
//            case "gear2":
//                carController.motorInput = 0.4f;
//                Debug.Log("reach to gear2");
//                break;
//            case "gear3":
//                carController.motorInput = 0.6f;
//                Debug.Log("reach to gear3");
//                break;
//            case "gear4":
//                carController.motorInput = 0.8f;
//                Debug.Log("reach to gear4");
//                break;
//            case "gear5":
//                carController.motorInput = 1f;
//                Debug.Log("reach to gear5");
//                break;
//            default:
//                carController.motorInput = 0f;
//                Debug.Log("not reach any gear");
//                break;
//        }
//    }
//}




// Hadi's original version
//using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

//public class ThrottleMovement : MonoBehaviour
//{
//    public GameObject Car;
//    public float CarSpeed = 3f;
//    public Waypoints path;
//    public XRGrabInteractable throttle;

//    private Vector3 initialLocalPosition;
//    private float speedMultiplier;
//    private bool isGrabbing;
//    private bool isInContactWithGear1;

//    private void Start()
//    {
//        throttle = GetComponent<XRGrabInteractable>();
//    }

//    private void Update()
//    {
//        if (throttle.isSelected)
//        {
//            Vector3 closestPoint = path.GetClosestPoint(transform.position);
//            transform.position = closestPoint;
//        }

//        if (isInContactWithGear1)
//        {
//            Car.transform.position += Car.transform.forward * CarSpeed * Time.deltaTime;
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "gear1")
//        {
//            isInContactWithGear1 = true;
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.tag == "gear1")
//        {
//            isInContactWithGear1 = false;
//        }
//    }
//}




using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ThrottleMovement : MonoBehaviour
{
    public XRGrabInteractable throttle;
    //public CarFollower carFollower;
    public CarSmoothChange carSmoothChange;

    private void Start()
    {
        throttle = GetComponent<XRGrabInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "gear0":
                carSmoothChange.speed = -5.0f;
                Debug.Log("reach to gear0");
                break;
            case "gear1":
                carSmoothChange.speed = 2f;
                Debug.Log("reach to gear1");
                break;
            case "gear2":
                carSmoothChange.speed = 5f;
                Debug.Log("reach to gear2");
                break;
            case "gear3":
                carSmoothChange.speed = 10f;
                Debug.Log("reach to gear3");
                break;
            case "gear4":
                carSmoothChange.speed = 20f;
                Debug.Log("reach to gear4");
                break;
            case "gear5":
                carSmoothChange.speed = 60f;
                Debug.Log("reach to gear5");
                break;
            default:
                Debug.Log("not reach any gear");
                Debug.Log(other);
                break;
        }
    }
}
