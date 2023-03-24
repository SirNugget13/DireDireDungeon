using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSkeletonCombat : MonoBehaviour
{
    public GameObject arrow;
    public Vector3 arrowOffset;
    public float arrowSpeed;

    public void ShootBow(Vector2 direction, Vector3 arrowRotation)
    {
        GameObject ArrowShot;
        ArrowShot = Instantiate
            (arrow, gameObject.transform.position + arrowOffset, Quaternion.Euler(arrowRotation));

        ArrowShot.GetComponent<Rigidbody2D>().velocity = direction.normalized * arrowSpeed;
    }
}
