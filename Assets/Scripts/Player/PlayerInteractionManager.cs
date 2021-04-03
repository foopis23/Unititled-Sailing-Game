using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    public Camera playerCamera;
    public LayerMask interactables;
    public float interactionDistance = 5.0f;

    private IInteractable _lookingAt;

    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit;
        if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit,
            interactionDistance,
            interactables.value))
        {
            if (_lookingAt != null)
            {
                _lookingAt.LookAway(gameObject);
                _lookingAt = null; 
            }
            return;
        }

        
        var interactable = hit.collider.gameObject.GetComponent<IInteractable>();

        if (_lookingAt != interactable)
        {
            _lookingAt?.LookAway(gameObject);

            _lookingAt = interactable;
            interactable.LookAt(gameObject);
        }

        if (!Input.GetButtonDown("Interact")) return;

        interactable.Interact(gameObject);
    }
}
