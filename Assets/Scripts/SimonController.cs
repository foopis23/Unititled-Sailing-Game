using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class SimonController : MonoBehaviour
{
    public Animator simonAnimator;
    public string[] pattern;
    public float timeBetweenPatterns;
    public float timeBetweenButtons;
    public float timeToExpireAttempt = 60;
    
    [FMODUnity.EventRef] public string redSoundEffect = "";
    [FMODUnity.EventRef] public string blueSoundEffect = "";
    [FMODUnity.EventRef] public string yellowSoundEffect = "";
    [FMODUnity.EventRef] public string greenSoundEffect = "";
    [FMODUnity.EventRef] public string failSoundEffect = "";
    [FMODUnity.EventRef] public string winSoundEffect = "";

    private float _lastAttemptTime;
    private int _attemptIndex;
    private int _level;
    private float _lastPatternTime;
    private float _lastButtonTime;
    private int _patternIndex;
    

    private bool _displayCurrentLevel = false;
    
    private void Start()
    {
        _lastPatternTime = Time.time;
        _lastButtonTime = Time.time;
        _patternIndex = 0;
        _lastAttemptTime = -1;
        _attemptIndex = 0;
        _level = 1;
        _displayCurrentLevel = true;
    }
    
    private void Update()
    {
        if (_lastAttemptTime > 0)
        {
            // Attempt Expired
            if (Time.time - _lastAttemptTime >= timeToExpireAttempt)
            {
                Fail();
            }
        }

        if (!_displayCurrentLevel) return;
        if (!(Time.time - _lastPatternTime > timeBetweenPatterns)) return;
        if (!(Time.time - _lastButtonTime > timeBetweenButtons)) return;
            
        _lastButtonTime = Time.time;
        
        if (_patternIndex >= _level || _patternIndex >= pattern.Length)
        {
            _patternIndex = 0;
            _lastPatternTime = Time.time;
            
            if (_lastAttemptTime > 0)
                _displayCurrentLevel = false;
            
            return;
        }
        
        simonAnimator.Play($"Activate{pattern[_patternIndex]}");
        FMODUnity.RuntimeManager.PlayOneShot(GetColorEffect(pattern[_patternIndex]), transform.position);
        _patternIndex++;
    }

    private string GetColorEffect(string color)
    {
        switch (color)
        {
            case "Red":
                return redSoundEffect;
            case "Blue":
                return blueSoundEffect;
            case "Yellow":
                return yellowSoundEffect;
            case "Green":
                return greenSoundEffect;
        }

        return redSoundEffect;
    }

    private void Fail()
    {
        //TODO: Play Fail Sound Effect
        
        _lastAttemptTime = -1;
        _level = 1;
        _attemptIndex = 0;

        // Reset Pattern Timer
        _lastPatternTime = Time.time;
        _lastButtonTime = Time.time;
        _displayCurrentLevel = true;
    }

    private void CompleteLevel()
    {
        //TODO: Play Success Sound
        _level++;
        _attemptIndex = 0;
        
        // reset pattern timers
        _displayCurrentLevel = true;
        _lastPatternTime = Time.time;
        _lastButtonTime = Time.time;
    }

    private void CompletePuzzle()
    {
        // TODO: Play Complete Puzzle Sound
        // TODO: Activate Beacon
        
        _displayCurrentLevel = false;
        _attemptIndex = 0;
        _lastAttemptTime = -1;
    }

    private void HitButton(string color)
    {
        _displayCurrentLevel = false;
        _lastAttemptTime = Time.time;
        simonAnimator.Play($"Activate{color}");
        
        if (!pattern[_attemptIndex].Equals(color))
        {
            Fail();
        }
        else
        {
            // Success
            _attemptIndex++;
        }
        
        if (_attemptIndex >= _level)
        {
            // Level Complete
            CompleteLevel();

        }
        
        if (_level < pattern.Length) return;
        
        //puzzle complete
        CompletePuzzle();
    }

    public void PressedYellow(GameObject player)
    {
        HitButton("Yellow");
    }

    public void PressedBlue(GameObject player)
    {
        HitButton("Blue");
    }

    public void PressedRed(GameObject player)
    {
        HitButton("Red");
    }

    public void PressedGreen(GameObject player)
    {
        HitButton("Green");
    }
}
