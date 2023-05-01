using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarMoveController : MonoBehaviour
{

    public PathCreator pathCreator0;
    public PathCreator pathCreator1;

    public float speed = 5f;
    public float distanceTravelled;

    private PathCreator curPathCreator;
    private int carPathIndex = 0;
    private int pathTotal = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: - ganjiaqi will change strategy later, now just simple test!!!
        if(carPathIndex == 0)
        {
            curPathCreator = pathCreator0;
        } else
        {
            curPathCreator = pathCreator1;
        }

        distanceTravelled += speed * Time.deltaTime;
        transform.position = curPathCreator.path.GetPointAtDistance(distanceTravelled);

        Quaternion rotationForPath = curPathCreator.path.GetRotationAtDistance(distanceTravelled);
        Vector3 euler = rotationForPath.eulerAngles;
        euler.z += 90;
        Quaternion rotationForCar = Quaternion.Euler(euler);
        transform.rotation = rotationForCar;


        // switch path
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            carPathIndex -= 1;
            carPathIndex %= pathTotal;
        } else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            carPathIndex += 1;
            carPathIndex %= pathTotal;
        }
    }
}
