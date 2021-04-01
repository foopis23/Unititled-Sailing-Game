using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightSystem : MonoBehaviour
{
    public ParticleSystem stars;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, speed * Time.deltaTime);
        transform.LookAt(Vector3.zero);
        Debug.Log(transform.rotation.eulerAngles.x);
        if (Mathf.Abs(transform.rotation.eulerAngles.x - 350) < 1 && !stars.isPlaying)
        {
            stars.Play();
        }

    }
}
