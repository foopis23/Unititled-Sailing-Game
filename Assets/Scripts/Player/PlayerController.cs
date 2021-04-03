using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float swimSpeed;
    public float gravityMultiplier = 1f;
    public float jumpVelocity = 10f;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    private CharacterController _characterController;
    private Vector3 _velocity;
    private bool _isSwimming;
    private bool _isGrounded;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _velocity = new Vector3();
    }

    private void Update()
    {
        DoWalking();
        DoGravity();

    }

    private void LateUpdate()
    {
        DoSwimming();
    }

    private void DoSwimming()
    {
        var playerPos = transform.position;
        var bottomY = playerPos.y - (_characterController.height / 2);
        var waveHeight = WaveManager.Instance.GetWaveHeight(playerPos);
        
        _isSwimming = waveHeight > bottomY;

        if (!(transform.position.y < waveHeight)) return;
        
        playerPos = new Vector3(playerPos.x, waveHeight, playerPos.z);
        transform.position = playerPos;
    }

    private void DoWalking()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        var move = transform.forward * z + transform.right * x;
        var currentSpeed = (_isSwimming) ? swimSpeed : speed;
        _characterController.Move(move * (Time.deltaTime * currentSpeed));
    }

    private void DoGravity()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded)
        {
            _velocity.Set(0, 0,0);

            if (Input.GetButton ("Jump") && !_isSwimming) {
                
                _velocity = transform.up * jumpVelocity;
            }
        }else if (_isSwimming)
        {
            _velocity = Physics.gravity;
        }
        else
        {
            _velocity += Physics.gravity * (gravityMultiplier * Time.deltaTime);
        }
        
        _characterController.Move(_velocity * Time.deltaTime);
    }
}
