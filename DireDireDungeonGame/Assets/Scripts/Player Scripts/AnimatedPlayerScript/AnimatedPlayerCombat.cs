using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedPlayerCombat : MonoBehaviour
{
    //public CinemachineVirtualCamera vcam;
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
            canSwing = false;
            swingTimer = 0;
        }
    }
}
