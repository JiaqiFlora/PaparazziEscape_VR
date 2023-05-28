using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool isShoot = false;
    private Transform spawnPoint;
    private float fireSpeed;
    private CarChangingController carChangingController;

    private Vector3 carVelocity;
    private Vector3 bulletVelocity;
    private Vector3 startPoint;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShoot)
        {
            startTime = Time.time;
            return;
        }

        gameObject.transform.position = startPoint + ((Time.time - startTime) * (carVelocity + bulletVelocity)) + carVelocity * Time.deltaTime;
    }


    public void TriggerBulletShoot(Transform spawnPoint, float fireSpeed, CarChangingController carChangingController)
    {
        this.spawnPoint = spawnPoint;
        this.fireSpeed = fireSpeed;
        this.carChangingController = carChangingController;

        startPoint = spawnPoint.transform.position;
        carVelocity = carChangingController.gameObject.transform.forward * carChangingController.speed;
        bulletVelocity = spawnPoint.forward * fireSpeed;
        startTime = Time.time;

        isShoot = true;

        Invoke("DestroyBullet", 5);
    }

    private void DestroyBullet()
    {
        isShoot = false;
        DestroyObject(gameObject);
    }
}
