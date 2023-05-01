using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Haptic
{
    [Range(0, 1)]
    public float intensity;
    public float duration;

    public void TriggerHaptic(BaseInteractionEventArgs eventArgs)
    {
        if(eventArgs.interactorObject is XRBaseControllerInteractor controllerInteractor)
        {
            TriggerHaptic(controllerInteractor.xrController);
        }
    }

    public void TriggerHaptic(XRBaseController controller)
    {
        if (intensity > 0)
        {
            controller.SendHapticImpulse(intensity, duration);
        }
    }
}

public class HapticInteractable : MonoBehaviour
{
    public Haptic hapticActivated;
    public Haptic hapticSelectEntered;
    public Haptic hapticCollide;

    private XRBaseInteractable interactable;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();

        if(interactable != null)
        {
            interactable.activated.AddListener(hapticActivated.TriggerHaptic);
            interactable.selectEntered.AddListener(hapticSelectEntered.TriggerHaptic);
        }
    }

    public void TriggerHaptic(Haptic haptic)
    {
        TriggerHapticBase(haptic);
    }

    public void TriggerHaptic(float intensity, float duration)
    {
        Haptic haptic = new Haptic();
        haptic.intensity = intensity;
        haptic.duration = duration;

        TriggerHapticBase(haptic);
    }

    private void TriggerHapticBase(Haptic haptic)
    {
        if (interactable.isSelected && interactable.interactorsSelecting.Count > 0)
        {
            XRBaseInteractor selectingInteractor = (XRBaseInteractor)interactable.interactorsSelecting[0];
            if (selectingInteractor is XRBaseControllerInteractor controllerInteractor)
            {
                haptic.TriggerHaptic(controllerInteractor.xrController);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && hapticCollide.intensity > 0)
        {
            XRBaseInteractor collideInteractor = other.GetComponent<XRBaseInteractor>();
            if (collideInteractor is XRBaseControllerInteractor controllerInteractor)
            {
                hapticCollide.TriggerHaptic(controllerInteractor.xrController);
            }
        }
    }
}
