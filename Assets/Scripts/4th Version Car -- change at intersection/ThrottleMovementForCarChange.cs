using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ThrottleMovementForCarChange : MonoBehaviour
{
    public CarChangingController carChangingController;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "gear0":
                carChangingController.speed = -100.0f;
                Debug.Log("reach to gear0");
                break;
            case "gear1":
                carChangingController.speed = 10f;
                Debug.Log("reach to gear1");
                break;
            case "gear2":
                carChangingController.speed = 50f;
                Debug.Log("reach to gear2");
                break;
            case "gear3":
                carChangingController.speed = 100f;
                Debug.Log("reach to gear3");
                break;
            case "gear4":
                carChangingController.speed = 200f;
                Debug.Log("reach to gear4");
                break;
            case "gear5":
                carChangingController.speed = 500f;
                Debug.Log("reach to gear5");
                break;
            default:
                Debug.Log("not reach any gear");
                Debug.Log(other);
                break;
        }
    }
}
