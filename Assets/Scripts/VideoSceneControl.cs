using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoSceneControl : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(StartEndingVideo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartEndingVideo()
    {
        //FadeScreen.instance.FadeIn(3f);
        yield return new WaitForSeconds(2f);
        videoPlayer.Play();
    }
}
