using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class BeginningControl : MonoBehaviour
{
    public static BeginningControl instance { get; private set; }

    public Transform driverTransform;
    public GameObject XROrigin;

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

    public void ChangeToDriverSeat()
    {
        //Debug.Log("change to drive seat!");
        //XROrigin.transform.position = driverTransform.position;
        //XROrigin.transform.rotation = driverTransform.rotation;

        StartCoroutine(changeToDriverProcess());
    }

    IEnumerator changeToDriverProcess()
    {
        FadeScreen.instance.FadeOut(2.5f);
        yield return new WaitForSeconds(2.5f);

        Debug.Log("change to drive seat!");
        XROrigin.transform.position = driverTransform.position;
        XROrigin.transform.rotation = driverTransform.rotation;

        FadeScreen.instance.FadeIn(2.5f);
    }
}
