using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioColliderDetector : MonoBehaviour
{

    public List<AudioSource> audioLists = new List<AudioSource>();

    // TODO: - ganjiaqi just for temp demo, delete or polish later
    private int index = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "audiofirst" && index != -1)
        {
            index = -1;
            audioLists[1].Stop();
            audioLists[0].Play();
            Debug.Log("play the first song!");
        } else if(other.tag == "audiosecond" && index != 1)
        {
            index = 1;
            audioLists[0].Stop();
            audioLists[1].Play();
            Debug.Log("play the second song!");
        }
    }
}
