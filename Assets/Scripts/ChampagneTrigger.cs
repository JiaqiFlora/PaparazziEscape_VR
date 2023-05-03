using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChampagneTrigger : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform parentTransform;
    public GameObject chanpagnePrefab;

    private XRSimpleInteractable simpleInteractable;

    // Start is called before the first frame update
    void Start()
    {
        simpleInteractable = GetComponent<XRSimpleInteractable>();
        simpleInteractable.activated.AddListener((interactor) => OnTriggerStart());
        simpleInteractable.selectEntered.AddListener((interactor) => OnSelectStart());
    }

    private void OnTriggerStart()
    {
        Debug.Log("trigger!");
        SpawnNewChampagne();
    }

    private void OnSelectStart()
    {
        Debug.Log("select!");
        SpawnNewChampagne();
    }

    public void SpawnNewChampagne()
    {
        GameObject newObj = Instantiate(chanpagnePrefab, parentTransform);
        newObj.transform.position = spawnPoint.position;
        
    }
}
