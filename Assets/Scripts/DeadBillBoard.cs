using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadBillBoard : MonoBehaviour
{
    public CarChangingController changingController;

    private Animator animator;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // version 1
        // TODO: - ganjiaqi. try to use distance to control dead!!! more accurate
        //if(other.tag == "Car" && !isDead)
        //{
        //    Debug.Log(other.name);
        //    isDead = true;

        //    Debug.Log("trigger dead billboard!");
        //    changingController.HitEndBillboard();

        //    FadeScreen.instance.EndingScreen(4f);
        //}


        // version2. collider move forward and add animation
        if (other.tag == "Car" && !isDead)
        {
            Debug.Log(other.name);
            isDead = true;

            Debug.Log("trigger dead billboard!");
            animator.SetTrigger("fall");

            RadioController.instance.StopSongs();
            VirtualAudioHelper.instance.PlayVirtualAudio(4, true, true);
            VirtualAudioHelper.instance.PlayVirtualAudio(6, true, true);

            changingController.PlanToHitBillBoard();

            //changingController.HitEndBillboard();
            //FadeScreen.instance.EndingScreen(4f);
        }

    }
}
