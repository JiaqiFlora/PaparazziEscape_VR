using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChampagneController : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private bool isGrabbing;
    private GameObject grabbedHand;
    private XRBaseInteractor selectingInteractor;

    private Vector3 myLastPosition;
    private float myLastTime;

    public float speed = 10f;
    public float searchRadius = 10f;
    private Transform target;
    private bool flyToTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener((interactor) => OnGrabStart());
        grabInteractable.selectExited.AddListener((interactor) => OnGrabEnd());
    }

    private void OnGrabStart()
    {
        selectingInteractor = (XRBaseInteractor)grabInteractable.interactorsSelecting[0];
        grabbedHand = selectingInteractor.gameObject;

        if (selectingInteractor.gameObject.tag == "Player")
        {
            isGrabbing = true;
        }
    }

    private void Update()
    {
        if(flyToTarget)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 360f);

            Vector3 movement = direction * speed * Time.deltaTime;
            transform.position += movement;

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                flyToTarget = false;
                target = null;
                DisappearChampagne();
            }
        }
    }

    private void OnGrabEnd()
    {
        isGrabbing = false;

        if(FindTarget())
        {
            flyToTarget = true;
        } else
        {
            Vector3 velocityForHand = (transform.position - myLastPosition) / (Time.time - myLastTime);
            Debug.Log($"my hand velocity is: {velocityForHand}");

            Vector3 throwDirection = selectingInteractor.transform.forward;
            Debug.Log($"select interactor direction: {selectingInteractor.transform.forward}, magnitude{velocityForHand.magnitude}");

            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().AddForce(velocityForHand.magnitude * throwDirection * 600f, ForceMode.Impulse);
            //GetComponent<Rigidbody>().AddForce(throwDirection * 1000f, ForceMode.Impulse);

            Invoke("DisappearChampagne", 5f);
        }

        // at this moment, throttle's parent has changed to car
        if (grabbedHand != null)
        {
            //transform.position = new Vector3(grabbedHand.transform.position.x, grabbedHand.transform.position.y, grabbedHand.transform.position.z);
            grabbedHand = null;
            selectingInteractor = null;
        }
    }

    private void LateUpdate()
    {
        if (isGrabbing)
        {
            if (grabInteractable.interactorsSelecting.Count > 0)
            {
                XRBaseInteractor selectingInteractor = (XRBaseInteractor)grabInteractable.interactorsSelecting[0];
                // when grabbing, update throttle object position to be the same world position with hand(interactor)
                transform.position = new Vector3(selectingInteractor.transform.position.x, selectingInteractor.transform.position.y, selectingInteractor.transform.position.z);

                myLastPosition = transform.position;
                myLastTime = Time.time;
            }
        }
    }

    private void DisappearChampagne()
    {
        Debug.Log("champagne disappear!!");
        this.gameObject.SetActive(false);
        Destroy(this);
    }

    private bool FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
        float closestDistance = Mathf.Infinity;

        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("motor"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    target = collider.gameObject.transform;
                    Debug.Log($"find more closer motor can hit!!! {collider.gameObject}");
                }
            }
        }

        if(target != null)
        {
            Debug.Log($"find closest motor can hit!!! ");
            return true;
        }

        return false;
    }
}
