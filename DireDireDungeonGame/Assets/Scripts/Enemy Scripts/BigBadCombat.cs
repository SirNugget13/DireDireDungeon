using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBadCombat : MonoBehaviour
{
    private CapsuleCollider2D cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = gameObject.GetComponent<CapsuleCollider2D>();
        cc.enabled = false;
    }

    public void Swing()
    {
        cc.enabled = true;
    }

    public void DoneSwinging()
    {
        cc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
