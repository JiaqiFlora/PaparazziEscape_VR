using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoadCubeOrder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ReverseOrderOfChildren();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReverseOrderOfChildren()
    {
        for(int i = 0; i < transform.childCount - 1; ++i)
        {
            transform.GetChild(0).SetSiblingIndex(transform.childCount - 1 - i);
        }
    }
}
