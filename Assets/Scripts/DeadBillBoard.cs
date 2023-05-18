using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBillBoard : MonoBehaviour
{
    public CarChangingController changingController;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Car")
        {
            Debug.Log("trigger dead billboard!");
            changingController.HitEndBillboard();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            Debug.Log("collide dead billboard!");
        }
    }
}
