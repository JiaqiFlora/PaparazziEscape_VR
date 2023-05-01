using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GearStickController : MonoBehaviour
{
    public Transform gearPositionsParent;
    public float snapDistance = 0.1f;
    public float snapSpeed = 5.0f;

    private Vector3 initialPosition;
    private Rigidbody rb;
    private List<Transform> gearPositions;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;

        // Get all gear positions
        gearPositions = new List<Transform>();
        foreach (Transform child in gearPositionsParent)
        {
            gearPositions.Add(child);
        }

        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void FixedUpdate()
    {
        // Check if the gear stick is close enough to any gear position and snap to it, but only when being grabbed
        if (grabInteractable.isSelected)
        {
            foreach (Transform gearPosition in gearPositions)
            {
                if (Vector3.Distance(transform.position, gearPosition.position) < snapDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, gearPosition.position, snapSpeed * Time.fixedDeltaTime);
                }
            }
        }
    }
}
