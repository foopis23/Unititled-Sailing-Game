using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private bool _isMapOpen;
    private Animator _animator;
    
    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _isMapOpen = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetKeyDown("m")) return;

        _animator.Play(_isMapOpen ? "NoteClose" : "NoteOpen");

        _isMapOpen = !_isMapOpen;
    }
}
