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
        timeBetweenFlash = Random.Range(2f, 4f);


        StartCoroutine(takePicture());

        flash.Play();

    }

    // Update is called once per frame
    void Update()
    {
        //if (timer >= 5)
        //{
        //    //ParticleSystem.MainModule main = flash.main;
        //    //main.simulationSpeed = timeBetweenFlash;

        //    StartCoroutine(takePicture());

        //    //if (isFlashing)
        //    //    audioSource.Play();

        //}
        //else
        //{
        //    timer += 1 * Time.deltaTime;
        //}




    }

    private IEnumerator takePicture()
    {
        while (true)
        {
            ParticleSystem.MainModule main = flash.main;
            main.simulationSpeed = timeBetweenFlash;
            isFlashing = true;

            

            //Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++");
            //Debug.Log(timeBetweenFlash);
            audioSource.Play();

            //flash.Play();
            isFlashing = false;
            //yield return new WaitForSeconds(timeBetweenFlash);
            yield return new WaitForSeconds(timeBetweenFlash);


        }



    }
}

//using System.Collections;
//using UnityEngine;

//public class ParticleFlash : MonoBehaviour
//{
//    private ParticleSystem flash;
//    private AudioSource audioSource;

//    private bool isFlashing = false;
//    private float randomTime;


//    private void Start()
//    {
//        audioSource = GetComponent<AudioSource>();
//        flash = GetComponent<ParticleSystem>();
//        StartCoroutine(TakePictures());
//        randomTime = Random.Range(0.5f, 3.5f);
//        flash.Play();
        
//    }

//    private IEnumerator TakePictures()
//    {
//        while (true)
//        {

//            Debug.Log("++++++++++++++++++++++++++");
//            Debug.Log(randomTime);
//            flash.Play();
//            audioSource.Play();

//            float duration = flash.main.duration;
//            yield return new WaitForSeconds(duration + randomTime);
//        }
//    }
//}
