using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour
{
    private HingeJoint hingeJoint;

    public List<AudioSource> audioLists = new List<AudioSource>();

    // TODO: - ganjiaqi just for temp demo, delete or polish later
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"radio angle is: {hingeJoint.angle}");

        if(hingeJoint.angle > 90 && index != 1 && audioLists.Count > 1)
        {
            index = 1;
            audioLists[0].Pause();
            audioLists[1].Play();
            Debug.Log("play the second song!");
        } else if(hingeJoint.angle < -90 && index != -1 && audioLists.Count > 0)
        {
            index = -1;
            audioLists[1].Stop();
            audioLists[0].Play();
            Debug.Log("play the first song!");
        }
    }
}
