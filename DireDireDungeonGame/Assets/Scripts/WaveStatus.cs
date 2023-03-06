using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStatus : MonoBehaviour
{
    public int waveNum = 1;

    private bool canUpdate;
    private float updateTimer;

    private void Update()
    {
        if (!canUpdate)
        {
            updateTimer += Time.deltaTime;
            if (updateTimer >= 0.21f)
            {
                canUpdate = true;
            }
        }

        if(canUpdate)
        {
            waveNum++;
            canUpdate = false;
            updateTimer = 0;
        }

    }
}
