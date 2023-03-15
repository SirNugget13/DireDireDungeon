using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;

    // Update is called once per frame
    void Update()
    {
        if (vcam.m_Lens.OrthographicSize > 7)
        {
            Debug.Log("Cam called");
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, 7, 1 * Time.deltaTime);
        }
    }
}
