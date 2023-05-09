using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RadioController : MonoBehaviour
{
    public List<AudioSource> audioLists = new List<AudioSource>();
    public GameObject positionToStay;

    private HingeJoint hingeJoint;
    private XRGrabInteractable grabInteractable;
    private bool isGrabbing;
    private float grabAngle;

    // TODO: - ganjiaqi just for temp demo, delete or polish later
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener((interactor) => OnGrabStart());
        grabInteractable.selectExited.AddListener((interactor) => OnGrabEnd());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = positionToStay.transform.position;

        //Debug.Log($"radio angle is: {hingeJoint.angle}");

        //if(hingeJoint.angle > 90 && index != 1 && audioLists.Count > 1)
        //{
        //    index = 1;
        //    audioLists[0].Pause();
        //    audioLists[1].Play();
        //    Debug.Log("play the second song!");
        //} else if(hingeJoint.angle < -90 && index != -1 && audioLists.Count > 0)
        //{
        //    index = -1;
        //    audioLists[1].Stop();
        //    audioLists[0].Play();
        //    Debug.Log("play the first song!");
        //}
    }

    private void LateUpdate()
    {
        if (isGrabbing)
        {
            if (grabInteractable.interactorsSelecting.Count > 0)
            {
                XRBaseInteractor selectingInteractor = (XRBaseInteractor)grabInteractable.interactorsSelecting[0];
                // when grabbing, update throttle object position to be the same world position with hand(interactor)
                transform.position = new Vector3(selectingInteractor.transform.position.x, selectingInteractor.transform.position.y, selectingInteractor.transform.position.z);

            }
        }

        transform.position = positionToStay.transform.position;
    }

    private void OnGrabStart()
    {
        XRBaseInteractor selectingInteractor = (XRBaseInteractor)grabInteractable.interactorsSelecting[0];
        //if (selectingInteractor.gameObject.tag == "Player")
        //{
        //    Debug.Log("is grabbing radio");
        //    isGrabbing = true;
        //    grabAngle = hingeJoint.angle;
        //}
        isGrabbing = true;
        grabAngle = hingeJoint.angle;

        if (positionToStay != null)
        {
            transform.position = positionToStay.transform.position;
        }

        GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnGrabEnd()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        isGrabbing = false;

        float angleDiff = Mathf.Abs(hingeJoint.angle - grabAngle);

        Debug.Log($"grabangle: {grabAngle}, cur angle: {hingeJoint.angle}, anglediff: {angleDiff}");

        if (angleDiff >= 15f)
        {
            PlayANewSong();
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
