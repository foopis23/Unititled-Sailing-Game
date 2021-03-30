using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class PlayerGravity : MonoBehaviour
{
    public LayerMask ground;
    
    public float gravityMultiplier = 1f;
    public float jumpVelocity = 10f;

    public bool isSwimming;
    
    private CharacterController _characterController;
    private Vector3 _velocity;

    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _velocity = new Vector3();
    }

    private void Update()
    {
        _characterController.Move(_velocity * Time.deltaTime); 
        var moveGravity = Physics.gravity * gravityMultiplier;
        //Vector3 feetPos = (transform.position - transform.up * (_characterController.height / 2));
        //RaycastHit test;
        //var isGrounded = Physics.SphereCast(feetPos, 0.2f, transform.up * -1, out test, 0.2f, ground.value);
        isGrounded = _characterController.isGrounded;

        if (isSwimming || isGrounded)
        {
            _velocity.Set(0, 0,0);

            if (Input.GetButton ("Jump")) {
                
                _velocity = transform.up * jumpVelocity;
            }
        }
        else
        {
            _velocity += Physics.gravity * (gravityMultiplier * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {

    }
}
