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

    private float _lastPatternTime;
    private float _lastButtonTime;
    private int patternIndex;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _lastPatternTime = Time.time;
        _lastButtonTime = Time.time;
        patternIndex = 0;
    }

    // Update is called once per frame
    private void Update()
    {
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
}
