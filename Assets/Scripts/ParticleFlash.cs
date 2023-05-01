using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFlash : MonoBehaviour
{
    private ParticleSystem flash;
    private float timeBetweenFlash;

    private float timer = 0;

    private AudioSource audioSource;

    private bool isFlashing = false;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(WaitBefore());

        audioSource = GetComponent<AudioSource>();
        flash = GetComponent<ParticleSystem>();
        timeBetweenFlash = Random.Range(0.5f, 3.5f);

        

        flash.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 5)
        {
            //ParticleSystem.MainModule main = flash.main;
            //main.simulationSpeed = timeBetweenFlash;

            StartCoroutine(takePicture());

            if (isFlashing)
                audioSource.Play();
        }
        else
        {
            timer += 1 * Time.deltaTime;
        }

        
        
        
    }

    private IEnumerator takePicture()
    {
        ParticleSystem.MainModule main = flash.main;
        main.simulationSpeed = timeBetweenFlash;
        isFlashing = true;
        yield return new WaitForSeconds(timeBetweenFlash);
        //flash.Play();
        isFlashing = false;

    }
}
