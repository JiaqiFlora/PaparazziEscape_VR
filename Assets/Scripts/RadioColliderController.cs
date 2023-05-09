using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioColliderController : MonoBehaviour
{
    public List<AudioSource> audioLists = new List<AudioSource>();

    private GameObject lastCollider;
    private int index = 0;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"collide!!! {other.gameObject}");
        if(lastCollider == null)
        {
            PlayANewSong();
            lastCollider = other.gameObject;
        }

        if(other.tag == "radioCollider" && other.gameObject.name != lastCollider.name)
        {
            Debug.Log($"collide!!!======= {other.gameObject}");
            PlayANewSong();
            lastCollider = other.gameObject;
        }
    }

    private void PlayANewSong()
    {
        int randomIndex = Random.Range(0, audioLists.Count);
        while (randomIndex == index)
        {
            randomIndex = Random.Range(0, audioLists.Count);
        }

        audioLists[index].Stop();
        audioLists[randomIndex].Play();

        Debug.Log($"play a new song. index is: {randomIndex}");

        index = randomIndex;
    }
}
