using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedPlayerCombat : MonoBehaviour
{
    //public CinemachineVirtualCamera vcam;
    public GameObject Sword;
    public PlayerController pc;

    public float SwingDelay;
    private float swingTimer;
    private bool canSwing = true;
    private CapsuleCollider2D swordPath;

    private void Start()
    {
        swordPath = gameObject.GetComponent<CapsuleCollider2D>();
        swordPath.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.state != PlayerController.State.Dead)
        {
            if (Input.GetButtonDown("Attack") && !GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().IsPaused && pc.state != PlayerController.State.Dead)
            {
                Attack();
            }

            swingTimer += Time.deltaTime;

            if (swingTimer >= SwingDelay)
            {
                canSwing = true;
            }
        }
    }

    void Attack()
    {
        if (canSwing)
        {
            swordPath.enabled = true;

            Sword.transform.SetLocalPositionAndRotation
                (Vector3.zero, Quaternion.Euler(0, 0, -125));
            this.Wait(0.2f, () =>
            {
                Sword.transform.SetLocalPositionAndRotation
                    (Vector3.zero, Quaternion.Euler(0, 0, -50));
                swordPath.enabled = false;
            });

            canSwing = false;
            swingTimer = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

        }
    }

}
