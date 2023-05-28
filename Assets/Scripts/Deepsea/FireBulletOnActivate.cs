using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20.0f;
    public AudioSource shootAudio;

    public CarChangingController carChangingController;

    private bool isShoot = false;


    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void FireBullet(ActivateEventArgs arg = null)
    {
        //isShoot = true;

        //shootAudio.Play();
        //GameObject spawnedBullet = Instantiate(bullet);
        //spawnedBullet.transform.position = spawnPoint.position;
        //Vector3 bulletVelocity = spawnPoint.forward * fireSpeed;
        //Vector3 carVelocity = carChangingController.curPathCreator.path.GetDirectionAtDistance(carChangingController.distanceTravelled) * carChangingController.speed;

        ////spawnedBullet.transform.position = spawnPoint.position + carVelocity * Time.deltaTime;
        //spawnedBullet.GetComponent<Rigidbody>().velocity = bulletVelocity + carVelocity;

        //if (spawnedBullet != null)
        //{
        //    Destroy(spawnedBullet, 5);
        //}

        shootAudio.Play();
        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.GetComponent<Bullet>().TriggerBulletShoot(spawnPoint, fireSpeed, carChangingController);
    }

}
