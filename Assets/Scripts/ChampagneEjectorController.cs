using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChampagneEjectorController : MonoBehaviour
{
    public GameObject champagneModelObj;
    public GameObject chanpagnePrefab;
    public Transform spawnPoint;
    public Transform parentTransform;
    public GameObject triggerCollider;
    public BoxCollider detectCollider;
    public AudioSource audioSource;

    private Animator chamAnimator;
    private XRSimpleInteractable simpleInteractable;
    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        chamAnimator = GetComponent<Animator>();
        simpleInteractable = GetComponent<XRSimpleInteractable>();
        simpleInteractable.selectEntered.AddListener((interactor) => OnSelectStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            Collider[] allColliders = Physics.OverlapBox(detectCollider.bounds.center, detectCollider.bounds.extents, Quaternion.identity);
            if (allColliders.Length <= 1)
            {
                Debug.Log("now ejector is open and nothing here!");

                CloseEjector();
            } else
            {
                foreach(Collider collider in allColliders)
                {
                    if(collider.tag == "champagne")
                    {
                        return;
                    }
                }

                Debug.Log("now have other colliders inside, but no chamgpane, so can close!");
                CloseEjector();
            }

        }
    }

    private void OnSelectStart()
    {
        Debug.Log("select!");

        OpenEjector();
    }

    public void SpawnNewChampagne()
    {
        champagneModelObj.SetActive(false);
        GameObject newObj = Instantiate(chanpagnePrefab, parentTransform);
        newObj.transform.position = spawnPoint.position;

        isOpen = true;
    }

    private void OpenEjector()
    {
        champagneModelObj.SetActive(true);
        chamAnimator.SetTrigger("eject");
        triggerCollider.SetActive(false);

        audioSource.PlayDelayed(1.3f);
    }

    private void CloseEjector()
    {
        isOpen = false;
        champagneModelObj.SetActive(false);
        chamAnimator.SetTrigger("down");

        triggerCollider.SetActive(true);
    }
}
