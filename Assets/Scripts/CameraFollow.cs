using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform follow;
    public Vector3 offset;
    public float rotationDamping;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = follow.position + follow.forward * offset.z + transform.up * offset.y + transform.right * offset.x;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = follow.position;
        var newPos = (pos + follow.forward * offset.z + transform.right * offset.x + Vector3.up);
        newPos.y = transform.position.y;
        transform.position = newPos;
        var lookPos = pos - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;
    }
}
