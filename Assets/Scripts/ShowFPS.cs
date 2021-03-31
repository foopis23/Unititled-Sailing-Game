using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
    public Text fpsText;
    public float deltaTime;

    public float fpsUpdatetime;
    private float _lastFPSChange = 0;

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        if (Time.time - _lastFPSChange >= fpsUpdatetime)
        {
            fpsText.text = Mathf.Ceil (fps).ToString ();
            _lastFPSChange = Time.time;
        }
    }
}
