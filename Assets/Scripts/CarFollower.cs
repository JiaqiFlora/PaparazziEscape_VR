using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarFollower : MonoBehaviour
{
    public List<PathCreator> pathCreators;
    public float distanceTravelled;

    public float speed = 10f;
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

    // Start is called before the first frame update
    void Start()
    {
        if(pathCreators.Count > 0)
        {
            curPathIndex = 0;
            curPathCreator = pathCreators[curPathIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TurnLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TurnRight();
        }


        distanceTravelled += speed * Time.deltaTime;
        // in case user go back at the beginning
        if(distanceTravelled < 0)
        {
            distanceTravelled = 0;
        }

        transform.position = curPathCreator.path.GetPointAtDistance(distanceTravelled);

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
        Debug.Log($"====speed is: {speed}");
    }

    public void RestartCar()
    {
        Debug.Log("restart the car");
        if(!isBrake)
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
        curPathIndex += 1;

        if(curPathIndex >= pathNums)
        {
            curPathIndex -= 1;
        }

        curPathCreator = pathCreators[curPathIndex];
    }

    public void TurnLeft()
    {
        curPathIndex -= 1;
        curPathIndex %= pathNums;

        if (curPathIndex < 0)
        {
            curPathIndex = 0;
        }

        curPathCreator = pathCreators[curPathIndex];
    }
}
