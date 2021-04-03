using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideNote : MonoBehaviour
{
    private bool _firstFrame = true;

    private void Update()
    {
        if (_firstFrame)
        {
            _firstFrame = false;
            return;
        }

        if (Input.GetButtonDown("Interact"))
        {
            gameObject.SetActive(false);
        }       
    }
}
