using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPanelController : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnInteraction(GameObject player)
    {
        _animator.Play("ActivateButton");
    }
}
