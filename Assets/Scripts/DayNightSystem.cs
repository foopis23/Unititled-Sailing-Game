using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightSystem : MonoBehaviour
{
    public float dayLength;
    public float startTime;
    private float _currentTime;

    public Color dayFogColor;
    public Color nightFogColor;
    [ColorUsage(false, true)]
    public Color dayAmbientColor;
    [ColorUsage(false, true)]
    public Color nightAmbientColor;

    // Start is called before the first frame update
    void Start()
    {
        _currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        var test = (int) (_currentTime / dayLength);
        
        Quaternion newRotation;
        Quaternion rotation;
        switch (test)
        {
            case 2:
                _currentTime -= dayLength * 2;
                break;
            case 1: // night time
                RenderSettings.fogColor = nightFogColor;
                RenderSettings.ambientLight = nightAmbientColor;
                rotation = transform.rotation;
                newRotation = Quaternion.Euler(180 + ((_currentTime - dayLength) / dayLength) * 180.0f, rotation.eulerAngles.y, rotation.eulerAngles.z);
                transform.rotation = newRotation;
                break;
            default:
            {
                RenderSettings.fogColor = dayFogColor;
                RenderSettings.ambientLight = dayAmbientColor;
                // day time
                rotation = transform.rotation;
                newRotation = Quaternion.Euler((_currentTime / dayLength) * 180.0f, rotation.eulerAngles.y, rotation.eulerAngles.z);
                transform.rotation = newRotation;
                break;
            }
        }
        
        _currentTime += Time.deltaTime;
    }
}
