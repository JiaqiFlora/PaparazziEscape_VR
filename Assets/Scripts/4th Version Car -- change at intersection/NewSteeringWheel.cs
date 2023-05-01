//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.XR.Interaction.Toolkit;

//public class NewSteeringWheel : XRBaseInteractable
//{
//    [SerializeField] private Transform wheelTransform;

//    public UnityEvent<float> OnWheelRotated;

//    private float currentAngle = 0.0f;

//    protected override void OnSelectEntered(SelectEnterEventArgs args)
//    {
//        Debug.Log("select new steering wheel!");
//        base.OnSelectEntered(args);
//        currentAngle = FindWheelAngle();
//    }

//    protected override void OnSelectExited(SelectExitEventArgs args)
//    {
//        base.OnSelectExited(args);
//        currentAngle = FindWheelAngle();
//    }

//    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
//    {
//        base.ProcessInteractable(updatePhase);

//        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
//        {
//            if (isSelected)
//                RotateWheel();
//        }
//    }

//    private void RotateWheel()
//    {
//        // Convert that direction to an angle, then rotation
//        float totalAngle = FindWheelAngle();

//        // Apply difference in angle to wheel
//        float angleDifference = currentAngle - totalAngle;
//        Debug.Log($"angle difference: {angleDifference}");
//        wheelTransform.Rotate(transform.up, angleDifference, Space.World);

//        // Store angle for next process
//        currentAngle = totalAngle;
//        OnWheelRotated?.Invoke(angleDifference);
//    }

//    private float FindWheelAngle()
//    {
//        float totalAngle = 0;

//        // Combine directions of current interactors
//        foreach (IXRSelectInteractor interactor in interactorsSelecting)
//        {
//            Vector3 direction = FindLocalPoint(interactor.transform.position);
//            totalAngle += ConvertToAngle(direction) * FindRotationSensitivity();
//        }

//        return totalAngle;
//    }

//    private Vector3 FindLocalPoint(Vector3 position)
//    {
//        // Convert the hand positions to local, so we can find the angle easier
//        return transform.InverseTransformPoint(position).normalized;
//    }

//    private float ConvertToAngle(Vector2 direction)
//    {
//        // Use a consistent up direction to find the angle
//        return Vector2.SignedAngle(transform.forward, direction);
//    }

//    private float FindRotationSensitivity()
//    {
//        // Use a smaller rotation sensitivity with two hands
//        return 1.0f / interactorsSelecting.Count;
//    }
//}



using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class NewSteeringWheel : XRBaseInteractable
{
    [SerializeField] private Transform wheelTransform;

    public UnityEvent<float> OnWheelRotated;
    public CarChangingController carChangingController;

    private float currentAngle = 0.0f;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("select new steering wheel!");
        base.OnSelectEntered(args);
        currentAngle = FindWheelAngle();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        currentAngle = FindWheelAngle();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
                RotateWheel();
        }
    }

    private void RotateWheel()
    {
        // Convert that direction to an angle, then rotation
        float totalAngle = FindWheelAngle();

        // Apply difference in angle to wheel
        float angleDifference = currentAngle - totalAngle;
        //Debug.Log($"total angle: {totalAngle}");
        //Debug.Log($"cur angle: {currentAngle}");
        //Debug.Log($"difference: {angleDifference}");

        // restrict it to -160-160, but when speed up, buggy!
        //if(totalAngle >= 160 && currentAngle <= 160 && currentAngle > 0)
        //{
        //    currentAngle = 160;
        //    return;
        //}

        //if(totalAngle <= -160 && currentAngle >= -160 && currentAngle < 0)
        //{
        //    currentAngle = -160;
        //    return;
        //}

        if(!(angleDifference < -30 || angleDifference > 30))
        {
            wheelTransform.Rotate(transform.up, angleDifference, Space.World);

        }

        // Store angle for next process
        currentAngle = totalAngle;
        OnWheelRotated?.Invoke(angleDifference);

        if(currentAngle > 0)
        {
            Debug.Log("turn left!");
            carChangingController.TurnLeft();
        } else
        {
            Debug.Log("turn right");
            carChangingController.TurnRight();
        }
    }

    private float FindWheelAngle()
    {
        float totalAngle = 0;

        // Combine directions of current interactors
        foreach (IXRSelectInteractor interactor in interactorsSelecting)
        {
            Vector2 direction = FindLocalPoint(interactor.transform.position);
            totalAngle += ConvertToAngle(direction) * FindRotationSensitivity();
        }

        return totalAngle;
    }

    private Vector2 FindLocalPoint(Vector3 position)
    {
        // Convert the hand positions to local, so we can find the angle easier
        Vector3 normalizdPoition = transform.InverseTransformPoint(position).normalized;
        return new Vector2(normalizdPoition.x, normalizdPoition.z);
    }

    private float ConvertToAngle(Vector2 direction)
    {
        // Use a consistent up direction to find the angle
        return Vector2.SignedAngle(transform.forward, direction);
    }

    private float FindRotationSensitivity()
    {
        // Use a smaller rotation sensitivity with two hands
        return 1.0f / interactorsSelecting.Count;
    }
}