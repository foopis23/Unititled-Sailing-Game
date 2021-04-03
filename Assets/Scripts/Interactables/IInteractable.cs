using UnityEngine;

public interface IInteractable
{
    public void Interact(GameObject player);
    public void LookAt(GameObject player);
    public void LookAway(GameObject player);
}