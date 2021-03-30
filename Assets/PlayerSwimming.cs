using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerGravity))]
[RequireComponent(typeof(CharacterController))]
public class PlayerSwimming : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerGravity _playerGravity;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerGravity = GetComponent<PlayerGravity>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        var bottomY = transform.position.y - (_characterController.height / 2);
        _playerGravity.isSwimming = WaveManager.Instance.GetWaveHeight(transform.position) > bottomY;
        
        var waveHeight = WaveManager.Instance.GetWaveHeight(transform.position);
        var centerY = transform.position.y;
        if (waveHeight > centerY)
        {
            _characterController.Move((waveHeight - centerY) * Vector3.up);
        }
    }
}
