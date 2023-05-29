using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using PathCreation;

public class MotoManager : MonoBehaviour
{
    public static MotoManager instance;
    public CarChangingController carController;
    // insatances for enemyMovement scirpt
    public Transform[] toFollow;
    public static int currentMotos;
    public GameObject MotoPrefab;
    //public GameObject MotoPrefabLeft;
    public Transform carTransform;
    public float spawnDistance = 400f;
    public Transform[] beginningToFollow;
    public GameObject frontCollider;
    public GameObject backColiider;
    public GameObject rightCollider;
    public GameObject leftCollider;
    

    public bool Test = false;
    private bool hasSpawned = false;

    public int maxMotorsNum = 20;

    private float currentTime;

    private float addDistance = 1;
    private bool isSmaller = false;
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

        if (!BeginningControl.instance.isOver)
        {
            return;
        }

        // TODO: - ganjiaqi. update its distance on the road to set motors' position. front: add 30%+; otherwise, just backward!
        if (!isSmaller)
            addDistance = 1;
        if (currentMotos == maxMotorsNum)
            isSmaller = false;

        currentMotos = GameObject.FindGameObjectsWithTag("Moto").Length;

        Debug.Log("now the number of motors: " + currentMotos);
        if (currentMotos < maxMotorsNum)
        {
            SpawnMotorcycle(addDistance);
            addDistance += 4;
            isSmaller = true;
        }

        //maxMotorsNum = 10;

        if (carController.speed == 0)
        {
            maxMotorsNum = 10;
        }
        else
        {
            if (currentTime >= 60)
                maxMotorsNum = 20;

            if (currentTime >= 80)
                maxMotorsNum = 30;

            if (currentTime >= 100)
                maxMotorsNum = 35;
        }



        //if (currentTime >= 60 && carController.speed != 0)
        //    maxMotorsNum = 30;

        //if (currentTime >= 80 && carController.speed != 0)
        //    maxMotorsNum = 40;

        //if (currentTime >= 100 && carController.speed != 0)
        //    maxMotorsNum = 50;

        //if (currentTime >= 120 && carController.speed != 0)
        //    maxMotorsNum = 70;

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
    
    private void SpawnMotorcycle(float addDistance)
    {
        //Vector3 spawnPosition = GetNavMeshPosition(carTransform.position, carTransform.forward, spawnDistance + addDistance);

        
        PathCreator pathCreator = carController.curPathCreator;
        float spawnPointDistance = carController.distanceTravelled + spawnDistance + addDistance;
        if(spawnPointDistance > pathCreator.path.length)
        {
            spawnPointDistance = carController.distanceTravelled - addDistance - 10; // TODO: - ganjiaqi this number can change later!!!
        }

        Vector3 spawnPosition = pathCreator.path.GetPointAtDistance(spawnPointDistance);
        Instantiate(MotoPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetNavMeshPosition(Vector3 origin, Vector3 direction, float distance)
    {
        Vector3 destination = origin + direction.normalized * distance;
        NavMeshHit navHit;

        if (NavMesh.SamplePosition(destination, out navHit, distance, 2))
        {
            return navHit.position;
        }
        return destination;
    }

    // FOR FLORA AT END OF GAME
    // TODO
    public void SpawnMotorcycleAtEnd(bool fromFront = true)
    {
        Debug.Log("plan to spawn motors!!!");
        Vector3 direction = carTransform.forward;
        if (!fromFront)
        {
            direction = (-1) * direction;
        }

        //Vector3 spawnPosition = GetNavMeshPosition(carTransform.position, direction, 40);
        for (int i = 0; i < maxMotorsNum; i++)
        {
            //Instantiate(MotoPrefab, spawnPosition, Quaternion.identity);

            SpawnMotorcycle(addDistance);
            addDistance += 4;
            isSmaller = true;
            
        }
        hasSpawned = true;
    }
}
