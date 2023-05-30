using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.Interaction.Toolkit;

public class VideoSceneControl : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Transform userTransform;
    public GameObject player;

    public GameObject creditCards;

    // Start is called before the first frame update
    void Start()
    {
        creditCards.SetActive(false);
        StartCoroutine(StartEndingVideo());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartEndingVideo()
    {
        // just in case user face other directions!
        player.transform.position = userTransform.position;
        player.transform.rotation = userTransform.rotation;

        FadeScreen.instance.FadeIn(4f);
        //yield return new WaitForSeconds(1f);
        yield return null;
        videoPlayer.Play();

        yield return new WaitForSeconds(30.7f);
        creditCards.SetActive(true);
    }
}
