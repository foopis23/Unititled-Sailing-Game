using UnityEngine;

public class NoteController : MonoBehaviour
{
    private bool _firstFrame = true;
    private bool _isCardOpen = false;
    private Animator _animator;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_isCardOpen) return;
        
        if (_firstFrame)
        {
            _firstFrame = false;
            return;
        }

        if (!Input.GetButtonDown("Interact")) return;
        _animator.Play("NoteClose");
        _isCardOpen = false;
    }

    public void ActivateNote()
    {
        _isCardOpen = true;
        _firstFrame = true;
        _animator.Play("NoteOpen");
    }
}
