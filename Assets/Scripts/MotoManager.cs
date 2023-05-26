using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MotoManager : MonoBehaviour
{
    public static MotoManager instance;
    public CarChangingController carController;
    // insatances for enemyMovement scirpt
    public Transform[] toFollow;
    public GameObject frontCollider;
    public static int currentMotos;
    public GameObject MotoPrefab;
    //public GameObject MotoPrefabLeft;
    public int instantWhenThereAreXLeft = 10;
    public Transform carTransform;
    public float spawnDistance = 400f;

    public bool Test = false;
    private bool hasSpawned = false;

    public int maxMotorsNum = 20;

    private float currentTime;


    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;


        if(!BeginningControl.instance.isOver)
        {
            return;
        }

        currentMotos = GameObject.FindGameObjectsWithTag("Moto").Length;

        Debug.Log("now the number of motors: " + currentMotos);
        if (currentMotos < maxMotorsNum)
        {
            SpawnMotorcycle();
        }

        if (currentTime >= 2)
            maxMotorsNum = 50;

        if (currentTime >= 110)
            maxMotorsNum = 70;

        if (currentTime >= 140)
            maxMotorsNum = 100;

        // Testing Flora's function
        //if (Test == true && !hasSpawned)
        //{
        //    SpawnMotorcycleAtEnd();
        //}

    }

    public void MotoDestroyed()
    {
        currentMotos--;
    }
    
    private void SpawnMotorcycle()
    {
        Vector3 spawnPosition = GetNavMeshPosition(carTransform.position, carTransform.forward, spawnDistance);
        Instantiate(MotoPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetNavMeshPosition(Vector3 origin, Vector3 direction, float distance)
    {
        Vector3 destination = origin + direction.normalized * distance;
        NavMeshHit navHit;

        if (NavMesh.SamplePosition(destination, out navHit, distance, NavMesh.AllAreas))
        {
            return navHit.position;
        }
        return destination;
    }

    // FOR FLORA AT END OF GAME
    public void SpawnMotorcycleAtEnd(bool fromFront = true) 
    {
        Debug.Log("plan to spawn motors!!!");
        Vector3 direction = carTransform.forward;
        if(!fromFront)
        {
            direction = (-1) * direction;
        }

        Vector3 spawnPosition = GetNavMeshPosition(carTransform.position, direction, 40);
        for (int i = 0; i < maxMotorsNum; i++)
        {
            Instantiate(MotoPrefab, spawnPosition, Quaternion.identity);
        }
        hasSpawned = true;
    }
}
