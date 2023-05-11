using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    private GameManager gm;
    private SceneLoader sl;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sl = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(gm.keyGotten)
            {
                gm.floor++;
                
                gm.Save();
                
                if (gm.floor % 5 == 0)
                {
                    sl.LoadScenes(2);
                }
                else
                {
                    sl.ReloadScene();
                }
                Debug.Log("Door open!");
            }
        }
    }
}
