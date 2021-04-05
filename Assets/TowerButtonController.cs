using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TowerButtonController : MonoBehaviour
{
    public ParticleSystem towerParticleSystem;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnInteract(GameObject player)
    {
        _animator.Play("PressButton");
        towerParticleSystem.gameObject.SetActive(true);
        PlayerData.Instance.activatedTower1 = true;
    }
}
