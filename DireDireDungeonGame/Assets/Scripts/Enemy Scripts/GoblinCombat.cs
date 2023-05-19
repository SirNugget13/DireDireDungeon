using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinCombat : MonoBehaviour
{
    public GameObject goblinSword;
    public float SwingDelay;
    public Goblin goblin;


    private float swingTimer;
    private bool canSwing = true;
    private CapsuleCollider2D swordPath;
    public AudioSource GoblinSwing;



    // Start is called before the first frame update
    void Start()
    {
        swordPath = gameObject.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(goblin.playerDistance < 2)
        {
            Attack();
        }

        swingTimer += Time.deltaTime;

        if (swingTimer >= SwingDelay)
        {
            canSwing = true;
        }
    }

    void Attack()
    {
        if (canSwing)
        {
            GoblinSwing.Play();
            swordPath.enabled = true;

            goblinSword.transform.SetLocalPositionAndRotation
                (Vector3.zero, Quaternion.Euler(0, 0, -125));
            this.Wait(0.2f, () =>
            {
                goblinSword.transform.SetLocalPositionAndRotation
                    (Vector3.zero, Quaternion.Euler(0, 0, -50));
                swordPath.enabled = false;
                

            });

            canSwing = false;
            swingTimer = 0;
        }
    }
}
