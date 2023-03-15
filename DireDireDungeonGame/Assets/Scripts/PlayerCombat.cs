using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCombat : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public GameObject Sword;
    public PlayerController pc;
    


    // Update is called once per frame
    void Update()
    {
       if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        Sword.transform.SetLocalPositionAndRotation
                (Vector3.zero, Quaternion.Euler(0, 0, -125));
        this.Wait(0.2f, () =>
        {
            Sword.transform.SetLocalPositionAndRotation
                (Vector3.zero, Quaternion.Euler(0, 0, -50));
        });
    }

}
