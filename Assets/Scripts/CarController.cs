//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CarController : MonoBehaviour
//{
//    public GameObject Throttle;
//    public float CarSpeed = 3f;

//    private Vector3 initialThrottleLocalPosition;
//    private float speedMultiplier;

//    // Start is called before the first frame update
//    void Start()
//    {
//        initialThrottleLocalPosition = Throttle.transform.localPosition;
//        speedMultiplier = CarSpeed;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        Vector3 currentThrottleLocalPosition = Throttle.transform.localPosition;
//        float throttleChange = currentThrottleLocalPosition.z - initialThrottleLocalPosition.z;

//        if (Mathf.Abs(throttleChange) > 0.012f)
//        {
//            CarSpeed = speedMultiplier * throttleChange;
//            transform.position += transform.forward * CarSpeed * Time.deltaTime;
//        }
//    }
//}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CarController : MonoBehaviour
//{
//    public GameObject Throttle;
//    public GameObject CarHolder;
//    public float CarSpeed = 3f;

//    private Vector3 initialThrottleLocalPosition;
//    private float speedMultiplier;

//    // Start is called before the first frame update
//    void Start()
//    {
//        initialThrottleLocalPosition = Throttle.transform.localPosition;
//        speedMultiplier = CarSpeed;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        Vector3 currentThrottleLocalPosition = Throttle.transform.localPosition;
//        float throttleChange = currentThrottleLocalPosition.z - initialThrottleLocalPosition.z;

//        if (Mathf.Abs(throttleChange) > 0.5f)
//        {
//            CarSpeed = speedMultiplier * throttleChange;
//            CarHolder.transform.position += CarHolder.transform.forward * CarSpeed * Time.deltaTime;
//        }
//    }
//}


//using System.Collections;
//using System.Collections;
//using UnityEngine;

//public class CarController : MonoBehaviour
//{
//    public GameObject Throttle;
//    public GameObject CarHolder;
//    public float CarSpeed = 3f;

//    private Vector3 initialThrottleLocalPosition;
//    private float speedMultiplier;

//    private Coroutine carMovementCoroutine;

//    // Start is called before the first frame update
//    void Start()
//    {
//        initialThrottleLocalPosition = Throttle.transform.localPosition;
//        speedMultiplier = CarSpeed;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        Vector3 currentThrottleLocalPosition = Throttle.transform.localPosition;
//        float throttleChange = currentThrottleLocalPosition.z - initialThrottleLocalPosition.z;

//        if (Mathf.Abs(throttleChange) > 0.5f)
//        {
//            CarSpeed = speedMultiplier * throttleChange;

//            if (carMovementCoroutine == null)
//            {
//                carMovementCoroutine = StartCoroutine(MoveCar());
//            }
//        }
//        else
//        {
//            if (carMovementCoroutine != null)
//            {
//                StopCoroutine(carMovementCoroutine);
//                carMovementCoroutine = null;
//            }
//        }
//    }

//    IEnumerator MoveCar()
//    {
//        while (true)
//        {
//            CarHolder.transform.position += CarHolder.transform.forward * CarSpeed * Time.deltaTime;
//            yield return null;
//        }
//    }
//}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject Throttle;
    public GameObject CarHolder;
    public float CarSpeed = 3f;
    public float ThrottleThreshold = 0.3f; // Add a throttle threshold

    private Vector3 initialThrottleLocalPosition;
    private float speedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        initialThrottleLocalPosition = Throttle.transform.localPosition;
        speedMultiplier = CarSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentThrottleLocalPosition = Throttle.transform.localPosition;
        float throttleChange = currentThrottleLocalPosition.z - initialThrottleLocalPosition.z;

        if (Mathf.Abs(throttleChange) > ThrottleThreshold)
        {
            CarSpeed = speedMultiplier * throttleChange;
            CarHolder.transform.position += CarHolder.transform.forward * CarSpeed * Time.deltaTime;
        }
    }
}

