using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paparazziAnimationTest : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            animator.SetBool("TurnRight", true);

        if (Input.GetKeyDown(KeyCode.A))
            animator.SetBool("TurnRight", false);

        if (Input.GetKeyDown(KeyCode.Tab))
            animator.SetBool("TurnLeft", true);

        if (Input.GetKeyDown(KeyCode.B))
            animator.SetBool("TurnLeft", false);
    }
}
