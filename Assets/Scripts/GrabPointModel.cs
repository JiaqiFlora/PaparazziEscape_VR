using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPointModel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetFloat("Grip", 1f);
    }

}
