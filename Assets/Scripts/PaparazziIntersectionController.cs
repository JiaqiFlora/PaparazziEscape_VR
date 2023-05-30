using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PaparazziIntersectionController : MonoBehaviour
{
    public static PaparazziIntersectionController instance { get; private set; }

    public GameObject car;
    public GameObject[] paparazziGroup;
    public float searchDistance;
    public float startDistance;
    private NavMeshAgent[] enemy;

    private bool isMoveTime = false;
    private float distance;

    private bool isCloseToIntersection = false;
    private List<NavMeshAgent> curEnemies = new List<NavMeshAgent>();


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

    private void Update()
    {
        //if (distance > startDistance)
        //    isMoveTime = false;
        //else
        //    isMoveTime = true;

        if (!isCloseToIntersection && curEnemies.Count <= 0)
        {
            return;
        }

        foreach (NavMeshAgent curEnemy in curEnemies)
        {
            distance = Vector3.Distance(car.transform.position, curEnemy.transform.position);

            if (distance < startDistance)
            {
                curEnemy.enabled = true;
                curEnemies.Remove(curEnemy);
            }
        }

        if(curEnemies.Count == 0)
        {
            isCloseToIntersection = false;
        }
    }

    public void EnableSomePaparazzi()
    {
        foreach(GameObject pap in paparazziGroup)
        {
            //enemy = pap.GetComponentsInChildren<NavMeshAgent>();
            distance = Vector3.Distance(car.transform.position, pap.transform.position);

            //if(distance < searchDistance)
            //{
            //    pap.SetActive(true);
            //    isCloseToIntersection = true;

            //    if (distance > startDistance)
            //    {
            //foreach (NavMeshAgent paparazzo in enemy)
            //{
            //    Debug.Log("===============");
            //    paparazzo.enabled = false;



            //    //if (!isMoveTime)
            //    //{
            //    //    paparazzo.enabled = false;
            //    //}
            //    //else 
            //    //{
            //    //    paparazzo.enabled = true;
            //    //}

            //}
            //    }

            //    return;
            //}





            if (distance < searchDistance)
            {
                pap.SetActive(true);
                enemy = pap.GetComponentsInChildren<NavMeshAgent>();
                foreach (NavMeshAgent paparazzo in enemy)
                {
                    Debug.Log("===============");
                    paparazzo.enabled = false;
                }

                curEnemies = new List<NavMeshAgent>(enemy);
                isCloseToIntersection = true;
            }
        }
    }
}
