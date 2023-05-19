using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSkeletonCombat : MonoBehaviour
{
    public GameObject arrow;
    public Vector3 arrowOffset;
    public float arrowSpeed;
    public float shootCoolDown;
    private float shootTimeCounter;
    private bool canShoot;
    public AudioSource Shoot;


    private void Update()
    {
        if(canShoot == false)
        {
            shootTimeCounter += Time.deltaTime;

            if(shootTimeCounter >= shootCoolDown)
            {
                canShoot = true;
            }
        }
    }

    public void ShootBow(Vector2 direction, Vector3 arrowRotation)
    {
        if(canShoot)
        {
            Shoot.Play();
            GameObject ArrowShot;
            ArrowShot = Instantiate
                (arrow, gameObject.transform.position + arrowOffset, Quaternion.Euler(arrowRotation));

            ArrowShot.GetComponent<Rigidbody2D>().velocity = direction.normalized * arrowSpeed;

            canShoot = false;
            shootTimeCounter = 0;
        }
    }
}
