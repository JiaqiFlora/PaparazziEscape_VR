using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DeadEndMotoCyclesBehavior : MonoBehaviour
{
    public GameObject car;

    private NavMeshAgent enemyNavMesh;
    private ParticleSystem flash;

    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        enemyNavMesh = GetComponent<NavMeshAgent>();
        flash = GetComponentInChildren<ParticleSystem>();

        // no particle playing
        flash.Stop();
        
        enemyNavMesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, car.transform.position);

        if (distance < 100)
        {
            flash.Play();
            enemyNavMesh.enabled = true;
        }
    }
}



