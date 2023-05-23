using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadBillBoard : MonoBehaviour
{
    public CarChangingController changingController;

    private bool isDead = false;

    private void OnTriggerEnter(Collider other)
    {
        // TODO: - ganjiaqi. try to use distance to control dead!!! more accurate
        if(other.tag == "Car" && !isDead)
        {
            Debug.Log(other.name);
            isDead = true;

            Debug.Log("trigger dead billboard!");
            changingController.HitEndBillboard();

            FadeScreen.instance.EndingScreen(2f);
        }
    }
}
