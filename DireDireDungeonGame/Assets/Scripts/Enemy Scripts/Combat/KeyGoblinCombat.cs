using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGoblinCombat : MonoBehaviour
{
    public GameObject goblinSword;
    public float SwingDelay;
    public KeyGoblin goblin;
    public float swingTriggerDistance;
    
    private float swingTimer;
    private bool canSwing = true;
    private CapsuleCollider2D swordPath;
    private Color orignalColor;


    // Start is called before the first frame update
    void Start()
    {
        swordPath = gameObject.GetComponent<CapsuleCollider2D>();
        orignalColor = goblin.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (goblin.isDead)
        {
            Destroy(swordPath);
            goblin.GetComponent<SpriteRenderer>().color = orignalColor;
            Destroy(gameObject);
        }

        if (goblin.playerDistance < swingTriggerDistance && canSwing)
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
        goblin.doMove = false;
        goblin.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        goblin.GetComponent<SpriteRenderer>().color = Color.red;

        canSwing = false;
        swingTimer = 0;

        this.Wait(0.3f, () =>
        {
            goblinSword.transform.SetLocalPositionAndRotation
                (Vector3.zero, Quaternion.Euler(0, 0, -125));

            if (swordPath != null) { swordPath.enabled = true; }
           

            this.Wait(0.2f, () =>
            {
                goblinSword.transform.SetLocalPositionAndRotation
                    (Vector3.zero, Quaternion.Euler(0, 0, -50));
                if (swordPath != null) { swordPath.enabled = false; }


                this.Wait(0.5f, () =>
                {
                    goblin.doMove = true;
                    goblin.GetComponent<SpriteRenderer>().color = orignalColor;
                });
            });
        });
    }
}
