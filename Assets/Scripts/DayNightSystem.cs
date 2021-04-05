using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Serialization;
using RenderSettings = UnityEngine.RenderSettings;

public class DayNightSystem : MonoBehaviour
{
    public float startTime;
    public float speed;
    
    [ColorUsage(false, true)]
    public Color dayAmbientColor;
    [ColorUsage(false, true)]
    public Color nightAmbientColor;
    [ColorUsage(true, false)]
    public Color dayFogColor;
    [ColorUsage(true, false)]
    public Color nightFogColor;
    public float fadeTime;
    private Color _currentAmbientColor;
    private Color _currentFogColor;

    public float dayEnvRefInt;
    public float nightEnvRefInt;
    private float _currentEnvRefInt;
    
    public ParticleSystem stars;
    private float _time; //! This probably isn't the best way to keep track of what time of dat it is, but it works

    private Light _light;
    
    private void Start()
    {
        _time = startTime;
        transform.RotateAround(Vector3.zero, Vector3.right, startTime);
        transform.LookAt(Vector3.zero);
        
        _currentAmbientColor = dayAmbientColor;
        _currentFogColor = dayFogColor;
        _currentEnvRefInt = dayEnvRefInt;

        _light = GetComponent<Light>();
    }
    
    private void Update()
    {
        UpdateTime();
        RotateSun();
        HandleTransitions();
        HandleStars();
    }

    private void HandleStars()
    {
        if (Math.Abs(_time - 190) < 1 && !stars.isPlaying)
        {
            var inverseSpeed = (1 / speed);
            var starsMain = stars.main;
            var starsMainStartLifetime = starsMain.startLifetime;
            
            starsMain.duration = 10 * inverseSpeed;
            starsMainStartLifetime.constantMin = 155 * inverseSpeed;
            starsMainStartLifetime.constantMax = 165 * inverseSpeed;
            
            stars.gameObject.SetActive(true);
            stars.Play();
        }else if (Math.Abs(_time - 350) < 1)
        {
            stars.gameObject.SetActive(false);
        }
    }

    private void HandleTransitions()
    {
        if (_time > (180 - fadeTime) && _time < (360 - fadeTime))
        {
            var intpoint = (_time - (180 - fadeTime)) / (fadeTime * 2);
            
            Debug.Log(_time - (180 - fadeTime));

            if (Math.Abs(_currentEnvRefInt - nightEnvRefInt) > 0.01)
                _currentEnvRefInt = Mathf.Lerp(dayEnvRefInt, nightEnvRefInt, intpoint);
            
            if (_currentFogColor != nightFogColor)
                _currentFogColor = Color.Lerp(dayFogColor, nightFogColor, intpoint);
            
            if (_currentAmbientColor != nightAmbientColor)
                _currentAmbientColor = Color.Lerp(dayAmbientColor, nightAmbientColor, intpoint);

            if (_light.intensity > 0.01)
                _light.intensity = Mathf.Lerp(_light.intensity, 0, intpoint);

        }else if (_time > 360 - fadeTime || _time < 180 - fadeTime)
        {

            var intpoint = ((_time > 360 - fadeTime) ? (_time - 360 - fadeTime) : _time + fadeTime) / (fadeTime * 2);

            if (Math.Abs(_currentEnvRefInt - dayEnvRefInt) > 0.01)
                _currentEnvRefInt = Mathf.Lerp(nightEnvRefInt, dayEnvRefInt, intpoint);
            
            if (_currentFogColor != dayFogColor)
                _currentFogColor = Color.Lerp(nightFogColor, dayFogColor, intpoint);
            
            if (_currentAmbientColor != dayAmbientColor)
                _currentAmbientColor = Color.Lerp(nightAmbientColor, dayAmbientColor, intpoint);
            
            if (_light.intensity < 0.99)
                _light.intensity = Mathf.Lerp(_light.intensity, 1.0f, intpoint);
        }
        
        RenderSettings.reflectionIntensity = _currentEnvRefInt;
        RenderSettings.fogColor = _currentFogColor;
        RenderSettings.ambientLight = _currentAmbientColor;
    }

    private void UpdateTime()
    {
        _time += speed * Time.deltaTime;
        _time %= 360;
    }

    private void RotateSun()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, speed * Time.deltaTime);
        transform.LookAt(Vector3.zero);
    }
}
