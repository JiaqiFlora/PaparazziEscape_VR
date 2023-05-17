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


    public Transform playerTarget;

    private bool hasCollided;

    private double zRotation;

    public float rotationSpeed = 10f;
    public float maxRotationAngle = 30f;
    public float collisionForce;

    public CarChangingController carChangingController;
    public GameObject frontCarCollider;
    public AudioSource bumpAudio;

    private float randSpeed;
    //TestForeward testForeward = new TestForeward(); // create an instance of TestForeward
    //float carSpeed = testForeward.speed; // access the speed property using the instance



    // Start is called before the first frame update
    void Start()
    {
        randSpeed = Random.Range(5, 12);

        enemy = GetComponentInChildren<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        //meshCollider = GetComponentInChildren<MeshCollider>();
        meshCollider = GetComponent<MeshCollider>();

        //enemy.speed = randSpeed + carChangingController.speed;
        //enemy.acceleration = randSpeed + carChangingController.speed;
        //enemy.acceleration = 1;
        hasCollided = false;
        flash = GetComponentInChildren<ParticleSystem>();

        zRotation = this.transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCollided)
        {
            enemy.SetDestination(playerTarget.position);
            transform.LookAt(playerTarget);

            // for speed 
            enemy.speed = randSpeed + carChangingController.speed;
            enemy.acceleration = randSpeed + carChangingController.speed;

            // for rotation of the cycle -> more realistic effect
            float velocityMagnitude = rb.velocity.magnitude;
            float rotationAmount = Mathf.Clamp(velocityMagnitude / 10f, 0f, 1f) * maxRotationAngle;

            // Determine direction of turn
            float turnDirection = Mathf.Sign(Vector3.Cross(transform.forward, rb.velocity.normalized).y);

            // Rotate motorcycle around local forward axis
            Quaternion rotation = Quaternion.AngleAxis(turnDirection * rotationAmount, transform.forward);
            transform.rotation = rotation * transform.rotation;

            // for making the cycle stop when the car stops
            float distance = Vector3.Distance(transform.position, playerTarget.transform.position);
            if (distance < 2 && carChangingController.speed == 0)
            {
                enemy.speed = 0;
            }
        }

        // simultae car collision from front in case of car moving too fast
        float distanceToFrontCollider = Vector3.Distance(transform.position, frontCarCollider.transform.position);
        if (distanceToFrontCollider < 3)
        {
            hasCollided = true;
            enemy.enabled = false;
            //boxCollider.enabled = false;
            //meshCollider.enabled = true;

            // for collision applied force in opposite direction

            Vector3 direction = transform.position - frontCarCollider.transform.position;
            direction = direction.normalized;
            Vector3 force = direction * collisionForce;
            rb.AddForce(force, ForceMode.Impulse);

            flash.Stop();

            rb.constraints = RigidbodyConstraints.FreezePositionY;
            Destroy(gameObject, 7);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            hasCollided = true;
            enemy.enabled = false;
            //boxCollider.enabled = false;
            //meshCollider.enabled = true;

            // for collision applied force in opposite direction
            //Vector3 direction = transform.position - collision.gameObject.transform.position;
            //direction = direction.normalized;
            //Vector3 force = direction * collisionForce;
            //rb.AddForce(force, ForceMode.Impulse);

            flash.Stop();

            rb.constraints = RigidbodyConstraints.FreezePositionY;
            Destroy(gameObject, 7);
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






//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class EnemyMovement : MonoBehaviour
//{
//    private NavMeshAgent enemy;
//    private Rigidbody rb;
//    private BoxCollider boxCollider;
//    private MeshCollider meshCollider;

//    public Transform playerTarget;
//    public CarFollower carFollower;

//    private bool hasCollided;

//    private double zRotation;

//    public float rotationSpeed = 10f;
//    public float maxRotationAngle = 30f;


//    // Start is called before the first frame update
//    void Start()
//    {
//        float carSpeed = carFollower.speed;
//        float mySpeed = Random.Range(carSpeed * 4, carSpeed * 8);

//        enemy = GetComponentInChildren<NavMeshAgent>();
//        rb = GetComponent<Rigidbody>();
//        boxCollider = GetComponent<BoxCollider>();
//        meshCollider = GetComponentInChildren<MeshCollider>();

//        enemy.speed = mySpeed;
//        enemy.acceleration = mySpeed;
//        hasCollided = false;

//        zRotation = this.transform.eulerAngles.z;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (!hasCollided)
//        {
//            float carSpeed = carFollower.speed;
//            float mySpeed = Random.Range(carSpeed * 1, carSpeed * 2f);
//            enemy.speed = mySpeed;
//            enemy.acceleration = mySpeed;

//            enemy.SetDestination(playerTarget.position);
//            transform.LookAt(playerTarget);

//            //float yRotation = this.transform.eulerAngles.y;
//            //Quaternion newRotation = Quaternion.Euler(this.transform.eulerAngles.x, 
//            //    this.transform.eulerAngles.y, (float)(this.transform.eulerAngles.y * -1.50));

//            //transform.rotation = newRotation;


//            float velocityMagnitude = rb.velocity.magnitude;
//            float rotationAmount = Mathf.Clamp(velocityMagnitude / 10f, 0f, 1f) * maxRotationAngle;

//            // Determine direction of turn
//            float turnDirection = Mathf.Sign(Vector3.Cross(transform.forward, rb.velocity.normalized).y);

//            // Rotate motorcycle around local forward axis
//            Quaternion rotation = Quaternion.AngleAxis(turnDirection * rotationAmount, transform.forward);
//            transform.rotation = rotation * transform.rotation;

//        }


//    }


//    private void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.tag == "Car")
//        {
//            hasCollided = true;
//            enemy.enabled = false;
//            boxCollider.enabled = false;
//            meshCollider.enabled = true;
//            rb.constraints = RigidbodyConstraints.FreezePositionY;
//            Destroy(gameObject, 7);
//        }
//    }
//}
