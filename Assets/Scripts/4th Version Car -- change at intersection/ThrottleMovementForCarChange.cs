using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ThrottleMovementForCarChange : MonoBehaviour
{
    public CarChangingController carChangingController;
    public AudioSource audioSource;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "gear0":
                carChangingController.speed = -25.0f;
                audioSource.Play();
                Debug.Log("reach to gear0");
                break;
            case "gear1":
                carChangingController.speed = 10f;
                audioSource.Play();
                Debug.Log("reach to gear1");
                break;
            case "gear2":
                carChangingController.speed = 20f;
                audioSource.Play();
                Debug.Log("reach to gear2");
                break;
            case "gear3":
                carChangingController.speed = 40f;
                audioSource.Play();
                Debug.Log("reach to gear3");
                break;
            case "gear4":
                carChangingController.speed = 90f;
                audioSource.Play();
                Debug.Log("reach to gear4");
                break;
            case "gear5":
                carChangingController.speed = 60f;
                audioSource.Play();
                Debug.Log("reach to gear5");
                break;
            default:
                Debug.Log("not reach any gear");
                Debug.Log(other);
                break;
        }
    }
}
