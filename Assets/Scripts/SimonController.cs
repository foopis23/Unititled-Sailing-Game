using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonController : MonoBehaviour
{
    public Animator simonAnimator;
    public string[] pattern;
    public float timeBetweenPatterns;
    public float timeBetweenButtons;

    public float timeToExpireAttempt = 60;
    private float lastAttemptTime;
    private int attemptIndex;
    private string[] attempt;

    private float _lastPatternTime;
    private float _lastButtonTime;
    private int patternIndex;


    // Start is called before the first frame update
    private void Start()
    {
        _lastPatternTime = Time.time;
        _lastButtonTime = Time.time;
        patternIndex = 0;
        lastAttemptTime = -1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (lastAttemptTime > 0)
        {
            // don't play pattern if currently attempting
            if (Time.time - lastAttemptTime < timeToExpireAttempt)
            {
                return;
            }

            lastAttemptTime = -1;
            return;
        }

        
        if (!(Time.time - _lastPatternTime > timeBetweenPatterns)) return;

        if (!(Time.time - _lastButtonTime > timeBetweenButtons)) return;
        _lastButtonTime = Time.time;
        
        if (patternIndex >= pattern.Length)
        {
            patternIndex = 0;
            _lastPatternTime = Time.time;
            return;
        }
        
        simonAnimator.Play($"Activate{pattern[patternIndex]}");
        patternIndex++;
    }

    public void PressedYellow(GameObject player)
    {
        lastAttemptTime = Time.time;
    }

    public void PressedBlue(GameObject player)
    {
        lastAttemptTime = Time.time;
    }

    public void PressedRed(GameObject player)
    {
        lastAttemptTime = Time.time;
    }

    public void PressedGreen(GameObject player)
    {
        lastAttemptTime = Time.time;
    }
}
