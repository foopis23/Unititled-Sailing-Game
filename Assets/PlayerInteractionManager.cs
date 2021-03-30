using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    public Camera playerCamera;
    public LayerMask interactables;
    public float interactionDistance = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetButtonDown("Interact")) return;
        RaycastHit hit;
        if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit,
            interactionDistance,
            interactables.value)) return;
        
        var interactable = hit.collider.gameObject.GetComponent<Interactable>();
        interactable.Interact(gameObject);
    }
}
