using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    private GameManager gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(gm.keyGotten)
            {
                gm.floor++;
                gm.Save();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                Debug.Log("Door open!");
            }
        }
    }
}
