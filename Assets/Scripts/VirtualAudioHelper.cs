using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualAudioHelper : MonoBehaviour
{
    public static VirtualAudioHelper instance { get; private set; }

    public bool isAudioOn = false;
    // 0: shiftGear
    // 1: accessoryIntro
    // 2: intersection
    // 3: deadEnd
    // 4: fallingBill
    // 5: driveFast
    // 6: billboard crash
    public List<AudioSource> audios;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void PlayVirtualAudio(int index, bool force = false, bool now = false)
    {
        if(now)
        {
            audios[index].Play();
            Debug.Log("play the song: " + index);
            // TODO: - ganjiaqi make sure this logic!!!
            return;
        }

        if(isAudioOn && !force)
        {
            return;
        }

        isAudioOn = true;
        StartCoroutine(PlayAudioProcess(index));
    }

    IEnumerator PlayAudioProcess(int index)
    {
        while(isOthersPlay())
        {
            // here to wait for other audios stop;
            continue;
        }


        float audioLength = audios[index].clip.length;
        audios[index].Play();

        yield return new WaitForSeconds(audioLength);
        isAudioOn = false;
    }

    private bool isOthersPlay()
    {
        foreach(AudioSource audio in audios)
        {
            if(audio.isPlaying)
            {
                return true;
            }
        }

        return false;
    }
} 
