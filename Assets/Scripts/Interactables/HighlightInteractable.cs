using UnityEngine;

public class HighlightInteractable : Interactable
{
    public Outline outline;

    public void Start()
    {
        outline.enabled = false;
    }

    public override void LookAt(GameObject player)
    {
        outline.enabled = true;
    }

    public override void LookAway(GameObject player)
    {
        outline.enabled = false;
    }
}