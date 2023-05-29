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
    public AudioSource turnAudio;
    public float collisionForce;

    // for component animation
    [Header("for model animation")]
    public GameObject oldSteeringWheel;
    public GameObject newSteeringWheel;
    public GameObject oldGear1;
    public GameObject oldGear2;
    public GameObject newGear;
    public GameObject oldRadio;
    public GameObject newRadio;
    public GameObject brakeObject;

    private float totalTime;
    private bool driving = false;
    private bool firstIntersection = true;
    private bool driveFastTip = false;
    private bool tipIsOn = false;
    private float previousSpeed = 0f;
    public TrackTreeNode curPathTreeNode;
    private bool deadEndPlayedAudio = false;
    public PathCreator curPathCreator
    {
        get
        {
            return curPathTreeNode.pathCreator;
        }
    }
  
    
    private bool turnLeft = true; // true for left, false for right
    private bool planToEnd = false; 

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
        // first driving moment trigger tip for other accessories;
        if(speed > 0 && !driving)
        {
            driving = true;
            StartCoroutine(FirstDrivePlayAudio());
        }

        if(curPathTreeNode == null)
        {
            return;
        }

        if (isBrake)
        {
            speed = 0f;
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
        if(distanceTravelled >= curPathCreator.path.length - 80f && curPathTreeNode.left != null && curPathTreeNode.right != null)
        {
            TurnTipOn();
        } else
        {
            canvasForSteerWheel.SetActive(false);
            tipIsOn = false;
        }
       

        // reach to the end!
        if (distanceTravelled >= curPathCreator.path.length)
        {
            // here to deal with the end of game thing!
            if (planToEnd)
            {
                Debug.Log("plan to the end!!!");
                HitEndBillboard();
                return;
            }

            if ((curPathTreeNode.left == null && curPathTreeNode.right == null))
            {
                Debug.Log("no way");
                speed = 0;
                distanceTravelled = curPathCreator.path.length;
                if(!deadEndPlayedAudio)
                {
                    VirtualAudioHelper.instance.PlayVirtualAudio(3);
                    deadEndPlayedAudio = true;
                }
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

        deadEndPlayedAudio = false;
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

        totalTime += Time.deltaTime;
        TriggerDirveFastAudio();
    }

    public void TriggerDirveFastAudio()
    {
        if (!driveFastTip && totalTime > 120 && speed < 50)
        {
            Debug.Log("exceed 2 mins! speed not fast enough, trigger drive fast tip!");
            VirtualAudioHelper.instance.PlayVirtualAudio(5);
            driveFastTip = true;
        }
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
        Debug.Log("turn right!!!");
        turnLeft = false;

    }

    public void TurnLeft()
    {
        Debug.Log("turn left!!!");
        turnLeft = true;
    }

    private void TurnTipOn()
    {
        if(tipIsOn)
        {
            return;
        }

        tipIsOn = true;
        turnAudio.Play();
        canvasForSteerWheel.SetActive(true);

        if(firstIntersection)
        {
            VirtualAudioHelper.instance.PlayVirtualAudio(2);
            firstIntersection = false;
        }
    }

    public void HitEndBillboard()
    {
        // v1: for fly away
        StopTheCar();
        //speed = 0;
        //StartCoroutine(CrashRoutine());
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;

        //Vector3 flyDirection = (-transform.forward + transform.up).normalized;
        Vector3 flyDirection = -transform.forward;
        Vector3 force = flyDirection * collisionForce;
        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;

        FadeScreen.instance.EndingScreen(3f);

        // v2: just stop
        // TODO: - ganjiaqi. add effect to show it is a car crash????
        //StopTheCar();
        //MotoManager.instance.SpawnMotorcycleAtEnd(false);
    }


    public void PlanToHitBillBoard()
    {
        // stop till the end of the road.
        planToEnd = true;

        // here to change the speed and disable thottle and brake
        speed = 60;
        newGear.SetActive(false);
        brakeObject.SetActive(false);
    }

    private IEnumerator CrashRoutine()
    {
        float timer = 0;
        while (timer <= FadeScreen.instance.fadeDuration)
        {
            speed -= 1;
            yield return null;
        }
    }

    IEnumerator FirstDrivePlayAudio()
    {
        yield return new WaitForSeconds(3f);
        VirtualAudioHelper.instance.PlayVirtualAudio(1, true);
    }
}