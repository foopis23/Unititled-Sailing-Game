using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBank : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FMODUnity.RuntimeManager.HasBankLoaded("Master Bank"))
        {
            Debug.Log("Master Bank Loaded");
        }
    }
}
