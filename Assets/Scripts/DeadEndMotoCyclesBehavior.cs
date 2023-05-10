using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DeadEndMotoCyclesBehavior : MonoBehaviour
{
    public GameObject car;
    public float distanceToMove;

    private NavMeshAgent enemyNavMesh;
    private ParticleSystem flash;

    private EnemyMovement script;
    private float distance;

    private bool isDistance;

    // Start is called before the first frame update
    void Start()
    {
        enemyNavMesh = GetComponent<NavMeshAgent>();
        flash = GetComponentInChildren<ParticleSystem>();

        script = GetComponent<EnemyMovement>();
        script.enabled = false;
        // no particle playing
        flash.Stop();
        
        //enemyNavMesh.enabled = false;

        isDistance = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDistance == true)
        {
            script.enabled = true;
            flash.Play();
            //enemyNavMesh.enabled = true;

        }

        distance = Vector3.Distance(transform.position, car.transform.position);

        if (distance < distanceToMove)
        {
            //flash.Play();
            //enemyNavMesh.enabled = true;
            isDistance = true;
        }
    }
}



