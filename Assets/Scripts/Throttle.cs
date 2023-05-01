//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Throttle : MonoBehaviour
//{
//    public GameObject Car;
//    public float CarSpeed = 3f;

//    private Vector3 initialLocalPosition;
//    private float speedMultiplier;

//    // Start is called before the first frame update
//    void Start()
//    {
//        initialLocalPosition = transform.localPosition;
//        speedMultiplier = CarSpeed;
//    }

//    // Update is called once per frame
//    void FixedUpdate()
//    {
//        Vector3 currentLocalPosition = transform.localPosition;
//        float throttleChange = currentLocalPosition.z - initialLocalPosition.z;

//        if (Mathf.Abs(throttleChange) > 0.5f)
//        {
//            CarSpeed = speedMultiplier * throttleChange;
//            Car.transform.position += Car.transform.forward * CarSpeed * Time.deltaTime;
//        }
//    }
//}

using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Throttle : MonoBehaviour
{
    public GameObject Car;
    public float CarSpeed = 3f;
    public XRGrabInteractable throttle;
    public float minX, maxX, minY, maxY, minZ, maxZ;

    private Vector3 initialLocalPosition;
    private float speedMultiplier;
    private bool isGrabbing;

    void Start()
    {
        initialLocalPosition = transform.localPosition;
        speedMultiplier = CarSpeed;

        throttle.selectEntered.AddListener((interactor) => OnGrabStart());
        throttle.selectExited.AddListener((interactor) => OnGrabEnd());

    }

    private void OnGrabStart()
    {
        isGrabbing = true;
    }

    private void OnGrabEnd()
    {
        isGrabbing = false;
    }


    void Update()
    {
        if (isGrabbing)
        {
            if (throttle.interactorsSelecting.Count > 0)
            {
                XRBaseInteractor selectingInteractor = (XRBaseInteractor)throttle.interactorsSelecting[0];
                Vector3 localHandPosition = throttle.transform.parent.InverseTransformPoint(selectingInteractor.transform.position);

                throttle.transform.localPosition = new Vector3(
                    Mathf.Clamp(localHandPosition.x, minX, maxX),
                    Mathf.Clamp(localHandPosition.y, minY, maxY),
                    Mathf.Clamp(localHandPosition.z, minZ, maxZ)
                );
            }
        }

        Vector3 currentLocalPosition = transform.localPosition;
        float throttleChange = currentLocalPosition.z - initialLocalPosition.z;

        CarSpeed = speedMultiplier * throttleChange;
        Car.transform.position += Car.transform.forward * CarSpeed * Time.deltaTime;
    }

}





