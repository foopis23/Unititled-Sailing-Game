using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBox : MonoBehaviour
{
    public Sprite note;
    public Image noteDisplay;
    public GameObject particleSystem;
    public GameObject noteObject;
    public GameObject powerCoilObject;

    private bool _hasSeenNote;
    private bool _towerIsActive;
    private NoteController _noteController;

    private void Start()
    {
        _noteController = noteDisplay.GetComponent<NoteController>();
    }

    public void OnInteract(GameObject player)
    {
        if (_towerIsActive) return;
        
        if (PlayerData.Instance.hasPowerCoil && _hasSeenNote)
        {
            //Activate Beacon
            noteObject.SetActive(false);
            powerCoilObject.SetActive(true);
            particleSystem.SetActive(true);
            _towerIsActive = true;
        }
        else
        {
            // show note
            noteDisplay.sprite = note;
            _noteController.ActivateNote();
            _hasSeenNote = true;
        }
    }
}
