using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Interactable : MonoBehaviour, IInteractable
{
    public UnityEvent<GameObject> onInteraction;

    public virtual void Interact(GameObject player)
    {
        onInteraction.Invoke(player);
    }

    public virtual void LookAt(GameObject player) {}

    public virtual void LookAway(GameObject player) {}
}