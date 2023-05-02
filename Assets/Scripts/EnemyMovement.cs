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

    public Transform playerTarget;

    private bool hasCollided;

    private double zRotation;

    public float rotationSpeed = 10f;
    public float maxRotationAngle = 30f;

    public CarChangingController carChangingController;

    //TestForeward testForeward = new TestForeward(); // create an instance of TestForeward
    //float carSpeed = testForeward.speed; // access the speed property using the instance



    // Start is called before the first frame update
    void Start()
    {
        float randSpeed = Random.Range(3, 6);

        enemy = GetComponentInChildren<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshCollider = GetComponentInChildren<MeshCollider>();

        enemy.speed = randSpeed + carChangingController.speed;
        enemy.acceleration = randSpeed + carChangingController.speed;
        //enemy.acceleration = 1;
        hasCollided = false;

        zRotation = this.transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCollided)
        {
            enemy.SetDestination(playerTarget.position);
            transform.LookAt(playerTarget);

            // for rotation of the cycle -> real effect
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


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            hasCollided = true;
            enemy.enabled = false;
            boxCollider.enabled = false;
            meshCollider.enabled = true;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            Destroy(gameObject, 7);
        }
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
