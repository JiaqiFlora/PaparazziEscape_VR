using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RealCarThrottleMovement : MonoBehaviour
{
    public SimpleCarController simpleCarController;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "gear0":
                simpleCarController.motorInput = -1.0f;
                Debug.Log("reach to gear0");
                break;
            case "gear1":
                simpleCarController.motorInput = 0.2f;
                Debug.Log("reach to gear1");
                break;
            case "gear2":
                simpleCarController.motorInput = 0.4f;
                Debug.Log("reach to gear2");
                break;
            case "gear3":
                simpleCarController.motorInput = 0.6f;
                Debug.Log("reach to gear3");
                break;
            case "gear4":
                simpleCarController.motorInput = 0.8f;
                Debug.Log("reach to gear4");
                break;
            case "gear5":
                simpleCarController.motorInput = 1f;
                Debug.Log("reach to gear5");
                break;
            default:
                Debug.Log("not reach any gear");
                break;
        }
    }
}

