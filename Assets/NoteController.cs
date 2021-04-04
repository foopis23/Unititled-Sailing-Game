using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    private bool _firstFrame = true;
    private Animator _animator;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_firstFrame)
        {
            _firstFrame = false;
            return;
        }

        if (Input.GetButtonDown("Interact"))
        {
            _animator.Play("NoteClose");
            // gameObject.SetActive(false);
        }       
    }

    public void ActivateNote()
    {
        _firstFrame = true;
        
        _animator.Play("NoteOpen");
    }
}
