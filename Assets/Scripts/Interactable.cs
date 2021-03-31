using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent<GameObject> onInteraction;

    public void Interact(GameObject player)
    {
        onInteraction.Invoke(player);
    }
}
