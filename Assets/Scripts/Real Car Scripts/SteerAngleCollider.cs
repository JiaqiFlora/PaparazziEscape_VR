using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerAngleCollider : MonoBehaviour
{
    public SimpleCarController simpleCarController;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "zeroRot":
                simpleCarController.steerInput = 0f;
                Debug.Log("reach to zero rot");
                break;
            case "leftRot1":
                simpleCarController.steerInput = -0.2f;
                Debug.Log("reach to leftRot1");
                break;
            case "leftRot2":
                simpleCarController.steerInput = -0.4f;
                Debug.Log("reach to leftRot2");
                break;
            case "leftRot3":
                simpleCarController.steerInput = -0.6f;
                Debug.Log("reach to leftRot3");
                break;
            case "leftRot4":
                simpleCarController.steerInput = -0.8f;
                Debug.Log("reach to leftRot4");
                break;
            case "leftRot5":
                simpleCarController.steerInput = -1.0f;
                Debug.Log("reach to leftRot5");
                break;
            case "rightRot1":
                simpleCarController.steerInput = 0.2f;
                Debug.Log("reach to rightRot1");
                break;
            case "rightRot2":
                simpleCarController.steerInput = 0.4f;
                Debug.Log("reach to rightRot2");
                break;
            case "rightRot3":
                simpleCarController.steerInput = 0.6f;
                Debug.Log("reach to rightRot3");
                break;
            case "rightRot4":
                simpleCarController.steerInput = 0.8f;
                Debug.Log("reach to rightRot4");
                break;
            case "rightRot5":
                simpleCarController.steerInput = 1.0f;
                Debug.Log("reach to rightRot5");
                break;
            default:
                Debug.Log("not reach any steer angle collider");
                Debug.Log(other);
                break;
        }
    }
}
