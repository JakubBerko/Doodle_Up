using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform doodler;
    private void LateUpdate()
    {
        if (doodler.position.y > transform.position.y)
        {
            Vector3 newCamPos = new Vector3(transform.position.x, doodler.position.y, transform.position.z);
            transform.position = newCamPos;
        }
    }
}
