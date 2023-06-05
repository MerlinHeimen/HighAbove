using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTarget;

    public float pLerp = 0.02f;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, cameraTarget.position, pLerp);
    }
}
