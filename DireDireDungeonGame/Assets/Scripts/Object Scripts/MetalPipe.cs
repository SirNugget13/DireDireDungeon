using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalPipe : MonoBehaviour
{
    public AudioSource pipeAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerSword"))
        {
            pipeAudio.Play();
        }
    }
}
