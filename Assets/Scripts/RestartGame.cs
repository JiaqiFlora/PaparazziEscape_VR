using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private bool restratButtonPressed = false;
    private bool isRestarting = false;

    public InputActionReference yButton;
    
    // Start is called before the first frame update
    void Start()
    {

        yButton.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (yButton.action.triggered) {
            var currentScene = SceneManager.GetActiveScene();
            //SceneManager.LoadScene(currentScene.name);
            SceneManager.LoadScene(0);
        }
    }
}
