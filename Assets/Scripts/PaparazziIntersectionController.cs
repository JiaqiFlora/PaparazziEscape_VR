using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaparazziIntersectionController : MonoBehaviour
{
    public static PaparazziIntersectionController instance { get; private set; }

    public GameObject car;
    public GameObject[] paparazziGroup;
    public float searchDistance;

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


    public void EnableSomePaparazzi()
    {
        foreach(GameObject pap in paparazziGroup)
        {
            float distance = Vector3.Distance(car.transform.position, pap.transform.position);

            if(distance < searchDistance)
            {
                pap.SetActive(true);
                return;
            }
        }
    }
}
