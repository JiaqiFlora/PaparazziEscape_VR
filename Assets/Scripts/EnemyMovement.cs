// hadi's original version
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent enemy;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private MeshCollider meshCollider;
    private ParticleSystem flash;
    private float randSpeed;
    private Animator animator;
    private bool hasCollided;
    private double zRotation;
    private CarChangingController carChangingController;

    // sorry Hadi, change here to public, I want to try collisionForce outside.
    public float collisionForce = 20f;
    public Transform playerTarget;
    public float rotationSpeed = 10f;
    public float maxRotationAngle = 30f;
    public GameObject frontCarCollider;
    public AudioSource bumpAudio;


    // Start is called before the first frame update
    void Start()
    {
        int randomToFollow = Random.Range(0, MotoManager.instance.toFollow.Length);
        carChangingController = MotoManager.instance.carController;
        frontCarCollider = MotoManager.instance.frontCollider;
        playerTarget = MotoManager.instance.toFollow[randomToFollow];
        
        randSpeed = Random.Range(8, 15);

        enemy = GetComponentInChildren<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshCollider = GetComponent<MeshCollider>();
        animator = GetComponentInChildren<Animator>();
        flash = GetComponentInChildren<ParticleSystem>();

        hasCollided = false;
        zRotation = this.transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        // for making the cycle stop when the car stops
        float distance = Vector3.Distance(transform.position, playerTarget.transform.position);

        if (!hasCollided)
        {
            enemy.SetDestination(playerTarget.position);
            transform.LookAt(playerTarget);

            // for speed 
            enemy.speed = randSpeed + carChangingController.speed;
            enemy.acceleration = randSpeed + carChangingController.speed;

            // rotaion of the motorcycke ==> SWITCHED OFF - messing with animation
            float velocityMagnitude = rb.velocity.magnitude;
            float rotationAmount = Mathf.Clamp(velocityMagnitude / 10f, 0f, 1f) * maxRotationAngle;
            float turnDirection = Mathf.Sign(Vector3.Cross(transform.forward, rb.velocity.normalized).y);
            Quaternion rotation = Quaternion.AngleAxis(turnDirection * rotationAmount, transform.forward);
            transform.rotation = rotation * transform.rotation;
            
            if (distance < 2 && carChangingController.speed == 0)
            {
                enemy.speed = 0;
            }
        }

        // simultae car collision from front in case of car moving too fast
        float distanceToFrontCollider = Vector3.Distance(transform.position, frontCarCollider.transform.position);
        if (distanceToFrontCollider < 3f)
        {
            hasCollided = true;
            enemy.enabled = false;

            Vector3 direction = transform.position - frontCarCollider.transform.position;
            direction = direction.normalized;
            Vector3 force = direction * collisionForce;
            rb.AddForce(force, ForceMode.Impulse);

            flash.Stop();

            rb.constraints = RigidbodyConstraints.FreezePositionY;

            Destroy(gameObject, 3);
        }

        // make the papparazi animate based on the object it is following
        if (playerTarget.tag == "RightSideFollow" && distance < 10)
        {
            animator.SetBool("TurnLeft", true);
        }
        else animator.SetBool("TurnLeft", false);
        if (playerTarget.tag == "LeftSideFollow" && distance < 10)
        {
            animator.SetBool("TurnRight", true); 
        }
        else animator.SetBool("TurnRight", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            hasCollided = true;
            enemy.enabled = false;
            flash.Stop();
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            MotoManager.currentMotos--;
            Debug.Log(MotoManager.currentMotos);
            Debug.Log("------------------");
            Destroy(gameObject, 3);
        }
    }

    public void HitByBottle(Vector3 direction)
    {
        Debug.Log("collide with champagne");
        hasCollided = true;
        //enemy.enabled = false;
        GetComponentInChildren<NavMeshAgent>().enabled = false;

        // for collision applied force in opposite direction
        //Vector3 direction = transform.position - other.transform.forward;

        Vector3 force = direction * collisionForce;
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

        GetComponentInChildren<ParticleSystem>().Stop();

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        Destroy(gameObject, 7);

        Debug.Log("bump audio play!!!!");
        bumpAudio.Play();
    }
}
