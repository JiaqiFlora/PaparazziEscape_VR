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
        Debug.Log($"detect, usual: {Physics.OverlapBox(detectCollider.bounds.center, detectCollider.bounds.extents, Quaternion.identity).Length}");
        if (isOpen)
        {
            Debug.Log($"detect, open: {Physics.OverlapBox(detectCollider.bounds.center, detectCollider.bounds.extents, Quaternion.identity)[0].gameObject}");
            if(Physics.OverlapBox(detectCollider.bounds.center, detectCollider.bounds.extents, Quaternion.identity).Length <= 1)
            {
                Debug.Log("now ejector is open and nothing here!");

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
    }

    private void CloseEjector()
    {
        isOpen = false;
        champagneModelObj.SetActive(false);
        chamAnimator.SetTrigger("down");

        triggerCollider.SetActive(true);
    }
}
