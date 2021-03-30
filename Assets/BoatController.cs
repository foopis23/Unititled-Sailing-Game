using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [FormerlySerializedAs("maxSpeed")] public float maxVelocity;
    public float acceleration;
    private float _velocity;
    
    public float turnSpeed;
    public bool isPlayerDriving = false;
    public GameObject playerDriving;
    [FormerlySerializedAs("camera")] public Camera boatCamera;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _velocity = 0;
    }

    private void FixedUpdate()
    {
        if (!isPlayerDriving) return;

        if (Input.GetButtonDown("Interact"))
        {
            isPlayerDriving = false;
            playerDriving.transform.position = transform.position + 5 * transform.right;
            boatCamera.gameObject.SetActive(false);
            playerDriving.SetActive(true);
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            _velocity = Mathf.Min(_velocity + acceleration * Time.fixedDeltaTime, maxVelocity);
        }
        else
        {
            _velocity = Mathf.Lerp(_velocity, 0, 0.02f);
        }

        var forwardNotVertical = transform.forward;
        forwardNotVertical.y = 0;
        forwardNotVertical.Normalize();
        var newPos = transform.position + forwardNotVertical * (_velocity * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPos);
        
        if (Input.GetKey(KeyCode.A))
        {
            var newRot = transform.rotation.eulerAngles - transform.up * (Time.fixedDeltaTime * turnSpeed);
            _rigidbody.MoveRotation(Quaternion.Euler(newRot));
        }

        if (Input.GetKey(KeyCode.D))
        {
            var newRot = transform.rotation.eulerAngles + transform.up * (Time.fixedDeltaTime * turnSpeed);
            _rigidbody.MoveRotation(Quaternion.Euler(newRot));
        }
    }

    public void GetInBoat(GameObject player)
    {
        playerDriving = player;
        player.SetActive(false);
        boatCamera.gameObject.SetActive(true);
        isPlayerDriving = true;
    }
}
