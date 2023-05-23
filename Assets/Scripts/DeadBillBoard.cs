using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBillBoard : MonoBehaviour
{
    public CarChangingController changingController;

    private bool isDead = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Car" && !isDead)
        {
            Debug.Log(other.name);
            isDead = true;

            Debug.Log("trigger dead billboard!");
            changingController.HitEndBillboard();
            FadeScreen.instance.FadeOut(0.1f);
        }
    }
}
