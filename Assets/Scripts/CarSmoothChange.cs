using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarSmoothChange : MonoBehaviour
{
    public List<PathCreator> pathCreators;
    public float distanceTravelled;

    public float speed = 0f;
    public bool isBrake = false;

    private PathCreator curPathCreator;
    private float previousSpeed = 0f;
    private int pathNums
    {
        get
        {
            return pathCreators.Count;
        }
    }
    private int curPathIndex = 0; // TODO: - ganjiaqi. consider change to a array to manage!

    public GameObject oldSteeringWheel;
    public GameObject newSteeringWheel;
    public GameObject oldGear1;
    public GameObject oldGear2;
    public GameObject newGear;

    private float startTime = 0;
    private bool turnRight = false;
    private bool turnLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        if (pathCreators.Count > 0)
        {
            curPathIndex = 0;
            curPathCreator = pathCreators[curPathIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isBrake)
        {
            speed = 0f;
            return;
        }
        
        distanceTravelled += speed * Time.deltaTime;
        // in case user go back at the beginning
        if (distanceTravelled < 0)
        {
            distanceTravelled = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || turnLeft)
        {
            TurnLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || turnRight)
        {
            TurnRight();
        } else
        {
            transform.position = curPathCreator.path.GetPointAtDistance(distanceTravelled);
        }

        Quaternion rotationForPath = curPathCreator.path.GetRotationAtDistance(distanceTravelled);
        //Debug.Log(rotationForPath);

        Vector3 euler = rotationForPath.eulerAngles;
        euler.z += 90;


        // TODO: - ganjiaqi. try to find a better way
        // this condition check is for the new path on cube guiding, so wired....
        //if(euler.z > 250 && euler.z < 340)
        //{
        //    euler.z += 90;
        //}

        // TODO: - ganjiaqi. just make it be 0, find a better way!!!
        //euler.z = 0;

        Quaternion rotationForCar = Quaternion.Euler(euler);
        transform.rotation = rotationForCar;
    }

    public void StopTheCar()
    {
        Debug.Log("stop the car");
        isBrake = true;
        previousSpeed = speed;
        speed = 0f;

        Debug.Log("set the speed to 0");
        Debug.Log($"now the speed is: {speed}, is brake: {isBrake}");
    }

    public void RestartCar()
    {
        Debug.Log("restart the car");
        if (!isBrake)
        {
            return;
        }

        speed = previousSpeed;
        isBrake = false;

        Debug.Log("set the speed to previous one");
    }

    public void StartCarModelChange()
    {
        oldSteeringWheel.SetActive(false);
        newSteeringWheel.SetActive(true);
        oldGear1.SetActive(false);
        oldGear2.SetActive(false);
        newGear.SetActive(true);
    }

    public void TurnRight()
    {
        if(curPathIndex == 1)
        {
            return;
        }

        //Debug.Log("turn right!!!");
        turnRight = true;

        Vector3 positionOnPath = curPathCreator.path.GetPointAtDistance(distanceTravelled);
        Vector3 directionOnPath = curPathCreator.path.GetDirectionAtDistance(distanceTravelled);
        Vector3 rightAngle = Vector3.Cross(directionOnPath, Vector3.down);

        if (startTime != 0)
        {
            transform.position = positionOnPath + rightAngle * speed * (Time.time - startTime);
        }
        else
        {
            startTime = Time.time;
        }


        // check for the end!
        PathCreator nextLane = pathCreators[1];
        Vector3 closestPointAtAnotherlane = nextLane.path.GetClosestPointOnPath(transform.position);
        float distanceToNextLane = Vector3.Distance(transform.position, closestPointAtAnotherlane);

        if(distanceToNextLane < 2f)
        {
            Debug.Log("reach to next lane! ==== right one");

            curPathIndex = 1;
            curPathCreator = nextLane;
            turnRight = false;
            startTime = 0f;

            float distanceNow = curPathCreator.path.GetClosestDistanceAlongPath(transform.position);
            distanceTravelled = distanceNow;
        }
    }

    public void TurnLeft()
    {
        if (curPathIndex == 0)
        {
            return;
        }

        //Debug.Log("turn right!!!");
        turnLeft = true;

        Vector3 positionOnPath = curPathCreator.path.GetPointAtDistance(distanceTravelled);
        Vector3 directionOnPath = curPathCreator.path.GetDirectionAtDistance(distanceTravelled);
        Vector3 leftAngle = Vector3.Cross(directionOnPath, Vector3.up);

        if (startTime != 0)
        {
            transform.position = positionOnPath + leftAngle * speed * (Time.time - startTime);
        }
        else
        {
            startTime = Time.time;
        }


        // check for the end!
        PathCreator nextLane = pathCreators[0];
        Vector3 closestPointAtAnotherlane = nextLane.path.GetClosestPointOnPath(transform.position);
        float distanceToNextLane = Vector3.Distance(transform.position, closestPointAtAnotherlane);

        if (distanceToNextLane < 2f)
        {
            Debug.Log("reach to next lane! ---- left one");

            curPathIndex = 0;
            curPathCreator = nextLane;
            turnLeft = false;
            startTime = 0f;

            float distanceNow = curPathCreator.path.GetClosestDistanceAlongPath(transform.position);
            distanceTravelled = distanceNow;
        }
    }
}
