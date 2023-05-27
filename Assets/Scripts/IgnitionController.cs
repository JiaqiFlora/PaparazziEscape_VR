using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IgnitionController : MonoBehaviour
{
    public Animator animator;
    public GameObject radioAnimationObject;
    public AudioSource audioSource;

    private bool isCarStart = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isCarStart)
        {
            Debug.Log("trigger ignition, plan to active steer wheel");

            animator.SetTrigger("start");
            radioAnimationObject.SetActive(true);
            isCarStart = true;

            StartCoroutine(IgnitionProcess());
        }
    }

    IEnumerator IgnitionProcess()
    {
        audioSource.Play();
        yield return new WaitForSeconds(6f);

        VirtualAudioHelper.instance.PlayVirtualAudio(0, true);
    }

}
