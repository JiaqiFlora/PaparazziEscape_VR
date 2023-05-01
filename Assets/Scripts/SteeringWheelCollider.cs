using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class SteeringWheelCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("enter into steering wheel collider");
            Debug.Log(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("exit from steering wheel collider");
            Debug.Log(other);

            XRBaseInteractor xRBaseInteractor = (XRBaseInteractor)other.GetComponent<XRDirectInteractor>();

            Debug.Log(xRBaseInteractor == null);

            //xRBaseInteractor.ForceDeselect();

            XRBaseInteractable xRBaseInteractable = (XRBaseInteractable)xRBaseInteractor.firstInteractableSelected;
            xRBaseInteractable.ForceDeselect();

            //SelectExitEventArgs args = new SelectExitEventArgs();
            //args.interactorObject = xRBaseInteractor;
            //Debug.Log(xRBaseInteractor.firstInteractableSelected);
            //xRBaseInteractor.firstInteractableSelected.OnSelectExiting(args);
            //xRBaseInteractor.firstInteractableSelected.OnSelectExited(args);

            //xRBaseInteractor.allowSelect = false; // working, but too slow to react

            //xRBaseInteractor.enabled = false; // working, but too slow to react

            //xRBaseInteractor.enableInteractions = false; // working, but too slow to react

            Debug.Log("force exit!!");
        }
    }


}


public static class XRBaseInteractableExtension
{
    /// <summary>
    /// Force deselect the selected interactable.
    ///
    /// This is an extension method for <c>XRBaseInteractable</c>.
    /// </summary>
    /// <param name="interactable">Interactable that has been selected by some interactor</param>
    public static void ForceDeselect(this XRBaseInteractable interactable)
    {
        Debug.Log($"enter into!!!  {interactable.isSelected}");
        //interactable.interactionManager.CancelInteractableSelection(interactable);
        interactable.interactionManager.CancelInteractableSelection((IXRSelectInteractable)interactable); // not working, assert is good, isSeleted changed, but in headset, still failed!
        Assert.IsFalse(interactable.isSelected);

        Debug.Log($"after assert: {interactable.isSelected}");
    }
}

public static class XRBaseInteractorExtension
{
    /// <summary>
    /// Force deselect any selected interactable for given interactor.
    ///
    /// This is an extension method for <c>XRBaseInteractor</c>.
    /// </summary>
    /// <param name="interactor">Interactor that has some interactable selected</param>
    public static void ForceDeselect(this XRBaseInteractor interactor)
    {
        //interactor.interactionManager.CancelInteractorSelection(interactor);
        interactor.interactionManager.CancelInteractorSelection((IXRSelectInteractor)interactor); // not working!! assert failed!
        Assert.IsFalse(interactor.isSelectActive);
    }
}
