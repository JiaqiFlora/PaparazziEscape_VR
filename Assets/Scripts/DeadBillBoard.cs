using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBillBoard : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Car")
        {
            Debug.Log("trigger dead billboard!");
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
