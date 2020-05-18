using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Transform toFollow;
    Vector3 camPosBefore;

    private void FixedUpdate()
    {
        camPosBefore = transform.position;
        Vector3 camPosNew = (toFollow.position + camPosBefore) / 2;
        transform.position = new Vector3(camPosNew.x, camPosNew.y, -10);
    }
}
