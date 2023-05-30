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
        //animator = GetComponent<Animator>();
        animator = GetComponentInParent<Animator>();
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
            isDead = true;

            //Debug.Log("trigger dead billboard!");
            //animator.SetTrigger("fall");

            //RadioController.instance.StopSongs();
            //VirtualAudioHelper.instance.PlayVirtualAudio(4, true, true);
            //VirtualAudioHelper.instance.PlayVirtualAudio(6, true, true);
            //VirtualAudioHelper.instance.PlayVirtualAudio(7, true, true);

            //changingController.PlanToHitBillBoard();

            //changingController.HitEndBillboard();
            //FadeScreen.instance.EndingScreen(4f);

            StartCoroutine(EndingProcess());
        }

    }

    IEnumerator EndingProcess()
    {
        Debug.Log("end process begin!");
        changingController.speed = 50;

        RadioController.instance.StopSongs();
        MotoManager.instance.DestroyAllPaparazzi(); // destroy all paparazzi!!!
        VirtualAudioHelper.instance.PlayVirtualAudio(8, true, true); // congrates audio
        RadioController.instance.StopSongs();

        yield return new WaitForSeconds(5f);

        RadioController.instance.StopSongs();
        VirtualAudioHelper.instance.PlayVirtualAudio(4, true, true); // omg, falling billboard

        yield return new WaitForSeconds(1f);

        animator.SetTrigger("fall");

        yield return new WaitForSeconds(1.5f);

        VirtualAudioHelper.instance.PlayVirtualAudio(7, true, true);
        VirtualAudioHelper.instance.PlayVirtualAudio(6, true, true);
        
        changingController.PlanToHitBillBoard();
    }
}
