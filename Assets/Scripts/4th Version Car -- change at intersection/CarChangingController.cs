using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarChangingController : MonoBehaviour
{
    // for car driving
    public float distanceTravelled;
    public float speed = 0f; 
    public bool isBrake = false;
    public GameObject canvasForSteerWheel;

    // for component animation
    [Header("for model animation")]
    public GameObject oldSteeringWheel;
    public GameObject newSteeringWheel;
    public GameObject oldGear1;
    public GameObject oldGear2;
    public GameObject newGear;
    public GameObject oldRadio;
    public GameObject newRadio;


    private float previousSpeed = 0f;
    private TrackTreeNode curPathTreeNode;
    private PathCreator curPathCreator
    {
        get
        {
            return curPathTreeNode.pathCreator;
        }
    }
  
    
    private bool turnLeft = true; // true for left, false for right

    // Start is called before the first frame update
    void Start()
    {
        Invoke("GetCurTreeRoot", 1f);
    }

    private void GetCurTreeRoot()
    {
        curPathTreeNode = TrackManager.instance.pathTreeRoot;
    }

    // Update is called once per frame
    void Update()
    {
        if(curPathTreeNode == null)
        {
            return;
        }

        if (isBrake)
        {
            speed = 0f;
            Debug.Log("is brake, return!!!");
            return;
        }

        distanceTravelled += speed * Time.deltaTime;

        // user reverse to parent road!
        if (distanceTravelled < 0)
        {
            distanceTravelled = 0;

            // reverse to its parent road
            if(curPathTreeNode.parent != null)
            {
                curPathTreeNode = curPathTreeNode.parent;
                distanceTravelled = curPathCreator.path.length;
                return;
            }
        }

        // if user gonna reach to the end of the road, and there are two child roads of current road, show ui tip!!!!
        if(distanceTravelled >= curPathCreator.path.length * 0.8 && curPathTreeNode.left != null && curPathTreeNode.right != null)
        {
            canvasForSteerWheel.SetActive(true);
        } else
        {
            canvasForSteerWheel.SetActive(false);
        }
       

        // reach to the end!
        if (distanceTravelled >= curPathCreator.path.length)
        {
            if((curPathTreeNode.left == null && curPathTreeNode.right == null))
            {
                Debug.Log("no way");
                distanceTravelled = curPathCreator.path.length;
                return;
            } else if((curPathTreeNode.left != null && curPathTreeNode.right == null))
            {
                Debug.Log("can only turn to left");
                curPathTreeNode = curPathTreeNode.left;
            } else if ((curPathTreeNode.left == null && curPathTreeNode.right != null))
            {
                Debug.Log("can only turn to right");
                curPathTreeNode = curPathTreeNode.right;
            } else
            {
                Debug.Log($"turn left? : {turnLeft}");
                if (turnLeft)
                {
                    curPathTreeNode = curPathTreeNode.left;
                }
                else if (!turnLeft)
                {
                    curPathTreeNode = curPathTreeNode.right;
                }
            }

            distanceTravelled = 0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TurnLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TurnRight();
        }

        transform.position = curPathCreator.path.GetPointAtDistance(distanceTravelled);

        Quaternion rotationForPath = curPathCreator.path.GetRotationAtDistance(distanceTravelled);

        Vector3 euler = rotationForPath.eulerAngles;
        euler.z = 0;

        Quaternion rotationForCar = Quaternion.Euler(euler);
        transform.rotation = rotationForCar;
    }

    public void StopTheCar()
    {
        Debug.Log("stop the car");
        isBrake = true;
        previousSpeed = speed;
        speed = 0f;
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
    }

    public void StartCarModelChange()
    {
        oldSteeringWheel.SetActive(false);
        newSteeringWheel.SetActive(true);
        oldGear1.SetActive(false);
        oldGear2.SetActive(false);
        newGear.SetActive(true);
        oldRadio.SetActive(false);
        newRadio.SetActive(true);
    }

    public void TurnRight()
    {
        //Debug.Log("turn right!!!");
        turnLeft = false;

    }

    public void TurnLeft()
    {
        //Debug.Log("turn left!!!");
        turnLeft = true;
    }
}