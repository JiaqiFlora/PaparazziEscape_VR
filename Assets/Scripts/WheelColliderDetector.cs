using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelColliderDetector : MonoBehaviour
{
    public CarChangingController carChangingController;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "turnLeft")
        {
            Debug.Log("detect to turn left!");
            carChangingController.TurnLeft();
        } else if (other.tag == "turnRight")
        {
            Debug.Log("detect to turn right!");
            carChangingController.TurnRight();
        }
    }
}
