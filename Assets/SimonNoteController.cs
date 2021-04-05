using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SimonNoteController : MonoBehaviour
{
    public Sprite note;
    public Image noteDisplay;
    private NoteController _noteController;

    public void Start()
    {
        _noteController = noteDisplay.GetComponent<NoteController>();
    }

    public void OnInteract(GameObject player)
    {
        noteDisplay.sprite = note;
        _noteController.ActivateNote();
    }
}
